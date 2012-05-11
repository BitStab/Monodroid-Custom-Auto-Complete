using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;

namespace iNAV.Helpers
{

    /// <summary>
    /// Database to store all the synchronized data from DynamicsNAV to speed up the working velocity
    /// of the device.
    /// </summary>
    
    
    

    class NAVDatabase
    {
        // Globals
        private static const String TAG = "NAV Database";
        public static const String KEY_WORD = SearchManager.SuggestColumnText1;
        public static const String KEY_DEFINITION = SearchManager.SuggestColumnText2;

        private static const String DATABASE_NAME = "iNAV_DB";
        private static const String FTS_VIRTUAL_TABLE = "FTSdictionary";
        private static const int DATABASE_VERSION = 2;

        private const DictionaryOpenHelper mDatabaseOpenHelper;
        private static const HashMap<String,String> mColumnMap = buildColumnMap();
    }

    private static class DictionaryOpenHelper : SQLiteOpenHelper {

        private Context mHelperContext;
        private SQLiteDatabase mDatabase;

        /* Note that FTS3 does not support column constraints and thus, you cannot
         * declare a primary key. However, "rowid" is automatically used as a unique
         * identifier, so when making requests, we will use "_id" as an alias for "rowid"
         */
        private static const String FTS_TABLE_CREATE =
                    "CREATE VIRTUAL TABLE " + FTS_VIRTUAL_TABLE +
                    " USING fts3 (" +
                    KEY_WORD + ", " +
                    KEY_DEFINITION + ");";

        public DictionaryOpenHelper(Context context) {
            super(context, DATABASE_NAME, null, DATABASE_VERSION);
            mHelperContext = context;
        }

        
        public override void onCreate(SQLiteDatabase db) {
            mDatabase = db;
            mDatabase.ExecSQL(FTS_TABLE_CREATE);
            loadDictionary();
        }

        /**
         * Starts a thread to load the database table with words
         */
        private void loadDictionary() {
            new System.Threading.Thread(new Runnable() {
                public void run() {
                    try {
                        loadWords();
                    } catch (IOException e) {
                        throw new RuntimeException(e);
                    }
                }
            }).start();
        }

        private void loadWords() throws IOException {
            Log.d(TAG, "Loading words...");
            final Resources resources = mHelperContext.getResources();
            InputStream inputStream = resources.openRawResource(R.raw.definitions);
            BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));

            try {
                String line;
                while ((line = reader.readLine()) != null) {
                    String[] strings = TextUtils.split(line, "-");
                    if (strings.length < 2) continue;
                    long id = addWord(strings[0].trim(), strings[1].trim());
                    if (id < 0) {
                        Log.e(TAG, "unable to add word: " + strings[0].trim());
                    }
                }
            } finally {
                reader.close();
            }
            Log.d(TAG, "DONE loading words.");
        }

        /**
         * Add a word to the dictionary.
         * @return rowId or -1 if failed
         */
        public long addWord(String word, String definition) {
            ContentValues initialValues = new ContentValues();
            initialValues.put(KEY_WORD, word);
            initialValues.put(KEY_DEFINITION, definition);

            return mDatabase.insert(FTS_VIRTUAL_TABLE, null, initialValues);
        }

        @Override
        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
            Log.w(TAG, "Upgrading database from version " + oldVersion + " to "
                    + newVersion + ", which will destroy all old data");
            db.execSQL("DROP TABLE IF EXISTS " + FTS_VIRTUAL_TABLE);
            onCreate(db);
        }
}