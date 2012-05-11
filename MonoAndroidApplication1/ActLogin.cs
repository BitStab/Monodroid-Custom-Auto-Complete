using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace iNAV
{
    [Activity(Label = "iNAV Login", Icon = "@drawable/icon")]
    public class ActLogin : Activity
    {
        private Logic service = null;
        private EditText tfLogin =null;
		private EditText tfPwd=null;
        private ProgressDialog progress;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            this.service = new Logic();

            //GUI-Elemente werden an Variablennamen gebunden
			//und entsprechende Events zugewiesen.
			this.tfLogin = FindViewById<EditText>(Resource.Id.tfLogin);
            this.tfPwd = FindViewById<EditText>(Resource.Id.tfPwd);
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += delegate
            {                
                ThreadPool.QueueUserWorkItem(o => btnLoginOnClick(btnLogin, null,this.tfLogin.Text, this.tfPwd.Text));
            };
        }

        public void btnLoginOnClick(Object sender, EventArgs e, String uname, String pwd)
        {
            //Wenn die Logik zugreift und Navision/Windows den Login genehmigt, gehe weiter zur ActOverview
            RunOnUiThread(() => {
                progress = ProgressDialog.Show(this, "Processing...", "Trying to connect and logging into Server", true);
                service._Service.AuthenticationCompleted += delegate { progress.Hide(); };
            });
			try{
	            if (service.proofUser(tfLogin.Text,tfPwd.Text)){
                    
                    //Übertrage erarbeitete Daten an die GUI
		            RunOnUiThread(() =>
                    {
                        var overview = new Intent(this, typeof(Dashboard));
                        overview.PutExtra("username", this.tfLogin.Text);
                        overview.PutExtra("password", this.tfPwd.Text); 
                        
                        //Starte die neue Oberfläche
                        StartActivity(overview);
                        progress.Dismiss();
                    });
	            }
	            else{
	            	RunOnUiThread(() =>
                    {
                        progress.Hide();
                        Toast.MakeText(this,"Invalid Credentials",ToastLength.Short).Show();                        
                    });
				}
			}
			catch(Exception err){
				RunOnUiThread(() =>
                {
                    progress.Hide();
                    Toast.MakeText(this, err.Message, ToastLength.Short).Show();
                });
			}
        }
    }
}

