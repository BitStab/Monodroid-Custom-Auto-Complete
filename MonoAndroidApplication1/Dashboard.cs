using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using iNAV.Helpers;


namespace iNAV
{
    
    public class Dashboard : Fragment
    {
        private String user = null;
        private String pwd = null;
        private ProgressDialog progress;

        private View mContentView;

        private Dialog dialog = null;
        private Logic service = null;
        private String[] customerList = null;
        private AutoCompleteTextView tv_customer_search = null;
        private TextView txtSelectedCustomer = null;
        private AutoCompleteTextView actv_Item = null;
        private ListView items_list = null;
        private ItemAdapter item_list_adapter = null;
        private ItemListViewAdapter item_list_view_adapter = null;
        private String customer_id = null;
        private LayoutInflater inflater = null;
        private ViewGroup container = null;
        private Bundle savedInstanceState = null;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            
            base.OnActivityCreated(savedInstanceState);
            
            //if(customer_id==null)
            //    this.customerDialog();
            //getCustomers(FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete));           
            //actv_Item.AfterTextChanged += delegate
            //{
            //    AfterTextChanged_Item(this.actv_Item, null);
            //};

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            mContentView = inflater.Inflate(Resource.Layout.Dashboard, null);
            items_list = mContentView.FindViewById<ListView>(Resource.Id.dashboardList);
            txtSelectedCustomer = mContentView.FindViewById<TextView>(Resource.Id.txtSelectedCustomer);
            actv_Item = mContentView.FindViewById<AutoCompleteTextView>(Resource.Id.actv_Item);
            actv_Item.Threshold = 1;
            Button btnAdd = mContentView.FindViewById<Button>(Resource.Id.btnAdd_to_Cart);
            btnAdd.Enabled = true;
            btnAdd.Clickable = true;
            btnAdd.Click += new EventHandler(btnAddOnClick);
            Activity activity = Activity;
            mContentView.SystemUiVisibilityChange += delegate(object sender, View.SystemUiVisibilityChangeEventArgs e)
            {
                ActionBar actionBar = Activity.ActionBar;
                if (actionBar != null)
                {
                    mContentView.SystemUiVisibility = e.Visibility;
                    if (e.Visibility == StatusBarVisibility.Visible)
                    {
                        actionBar.Show();
                    }
                    else
                    {
                        actionBar.Hide();
                    }
                }
            };

            // Show/hide the system status bar when single-clicking a photo. This is also called
            // 'lights out mode.' Activating and deactivating this mode also invokes the listener
            // defined above, which will show or hide the action bar accordingly.

            mContentView.Click += delegate
            {
                if (mContentView.SystemUiVisibility == StatusBarVisibility.Visible)
                {
                    mContentView.SystemUiVisibility = StatusBarVisibility.Hidden;
                }
                else
                {
                    mContentView.SystemUiVisibility = StatusBarVisibility.Visible;
                }
            };
            return mContentView;
        }

        private void ShowLoginPopup(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View loginPopup = inflater.Inflate(Resource.Layout.Login, container);
            dialog = new Dialog(container.Context);
            dialog.SetContentView(loginPopup);
            dialog.SetTitle("Login");
            tv_customer_search = (AutoCompleteTextView)dialog.FindViewById(Resource.Id.autocomplete);
            tv_customer_search.Threshold = 1;
            EditText tfLogin =null;
		    EditText tfPwd=null;
            //GUI-Elemente werden an Variablennamen gebunden
			//und entsprechende Events zugewiesen.
            tfLogin = loginPopup.FindViewById<EditText>(Resource.Id.tfLogin);
            tfPwd = loginPopup.FindViewById<EditText>(Resource.Id.tfPwd);
            Button btnLogin = loginPopup.FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += delegate
            {                
                ThreadPool.QueueUserWorkItem(o => btnLoginOnClick(btnLogin, null,tfLogin.Text, tfPwd.Text));
            };
        }

        public void btnLoginOnClick(Object sender, EventArgs e, String uname, String passwd)
        {
            //Wenn die Logik zugreift und Navision/Windows den Login genehmigt, gehe weiter zur ActOverview
            Activity.RunOnUiThread(() =>
            {
                progress = ProgressDialog.Show(Activity, "Processing...", "Trying to connect and logging into Server", true);
                service._Service.AuthenticationCompleted += delegate { progress.Hide(); };
            });
            try
            {
                if (service.proofUser(uname, passwd))
                {

                    //Übertrage erarbeitete Daten an die GUI
                    Activity.RunOnUiThread(() =>
                    {
                        user = uname;
                        pwd = passwd;
                        progress.Dismiss();
                    });
                }
                else
                {
                    Activity.RunOnUiThread(() =>
                    {
                        progress.Hide();
                        Toast.MakeText(Activity, "Invalid Credentials", ToastLength.Short).Show();
                    });
                }
            }
            catch (Exception err)
            {
                Activity.RunOnUiThread(() =>
                {
                    progress.Hide();
                    Toast.MakeText(Activity, err.Message, ToastLength.Short).Show();
                });
            }
        }

        private void getCustomers(AutoCompleteTextView tv, String search)
        {

            try
            {
                service.getCustomer("*");
            }
            catch (Exception e)
            {
                Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(Activity, e.Message, ToastLength.Short).Show();
                });
            }
            while (service.customersLock == true)
            {
            }
            Activity.RunOnUiThread(() =>
            {
                var adapter = service.PopulateCustomers(Activity, Resource.Layout.details, Resource.Id.Name);
                tv.Adapter = adapter;
            });

        }

        // Dialog zur Anzeige der Customer-Auswahl
        private void customerDialog(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {


            View setCustomer = inflater.Inflate(Resource.Layout.SetCustomer, container);
            dialog = new Dialog(container.Context);
            dialog.SetContentView(setCustomer);
            dialog.SetTitle("Search & select customer");
            tv_customer_search = (AutoCompleteTextView)dialog.FindViewById(Resource.Id.autocomplete);
            tv_customer_search.Threshold = 1;

            ThreadPool.QueueUserWorkItem(o =>
            {
                getCustomers(tv_customer_search, null);
            });

            dialog.Show();

            //tv_customer_search.AfterTextChanged += delegate
            //{
            //    AfterTextChanged_Customer(this.tv_customer_search, null);
            //};
            Button btnSelect = (Button)dialog.FindViewById(Resource.Id.btnSelect);
            Button btnCancel = (Button)dialog.FindViewById(Resource.Id.btnCancel);

            btnCancel.Click += delegate
            {
                btnCancelOnClick(this, null);
            };

            btnSelect.Click += delegate
            {
                btnSelectOnClick(this, null);
            };
        }

        private void getItems(AutoCompleteTextView tv, String search)
        {
            try
            {
                service.getItems("*");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("400"))
                    getItems(tv,search);
                else
                    Activity.RunOnUiThread(() =>
                    {
                        Toast.MakeText(Activity, e.Message, ToastLength.Short).Show();
                    });
            }
            while (service.itemLock == true)
            {
            }
            Activity.RunOnUiThread(() =>
            {
                var adapter = service.PopulateItems(Activity, Resource.Layout.autocomplete_item, Resource.Id.Desc1);
                tv.Adapter = adapter;
            });
        }

        private void AfterTextChanged_Customer(Object sender, EventArgs e)
        {
            AutoCompleteTextView actv = (AutoCompleteTextView)sender;
            if (actv.Text.Length < 3)
            {
                getCustomers(actv, actv.Text);
            }
            else
            {
                if (actv.Text.Length == 1)
                {
                    getCustomers(actv, actv.Text);
                }
            }
        }

        private void AfterTextChanged_Item(Object sender, EventArgs e)
        {
            AutoCompleteTextView actv = (AutoCompleteTextView)sender;
            if (actv.Text.Length < 3)
            {
                getItems(actv, actv.Text);
            }
        }
        
        private void ListClicked(object sender, DialogClickEventArgs e)
        {
            var items = customerList;

            var builder = new AlertDialog.Builder(Activity);
            builder.SetMessage(string.Format("You selected: {0} , {1}", (int)e.Which, customerList[(int)e.Which]));
            builder.Show();
        }

        private void btnCancelOnClick(Object sender, EventArgs e)
        {
            
        }

        private void btnSelectOnClick(Object sender, EventArgs e)
        {

            if (tv_customer_search.Text.Length > 5)
            {
                // Suche nach dem Ausgewählten Unternehmen
                customer_id = tv_customer_search.Text.Substring(0, tv_customer_search.Text.IndexOf(" "));
                txtSelectedCustomer.Text = tv_customer_search.Text;
                ThreadPool.QueueUserWorkItem(o =>
                {
                    getItems(actv_Item, null);
                });
                dialog.Dismiss();
            }
        }

        private void btnAddOnClick(Object sender, EventArgs e)
        {
            if (actv_Item.Text.Contains(" "))
            {
                item_list_adapter = (ItemAdapter)actv_Item.Adapter;
                Item_Order_Popup(actv_Item.Text.Substring(0, actv_Item.Text.IndexOf(" ")),inflater,container,savedInstanceState);
            }
        }

        private void Item_Order_Popup(String itemID, LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View itemOrderPopup = inflater.Inflate(Resource.Layout.Item_Order_Popup, null);
            dialog = new Dialog(container.Context);
            dialog.SetContentView(itemOrderPopup);
            dialog.SetTitle("Order selected Item");
            String itemDesc = null;
            ItemSingle item = item_list_adapter.getItem(itemID);
            EditText itemName = (EditText) dialog.FindViewById(Resource.Id.itemName);
            EditText amount = (EditText) dialog.FindViewById(Resource.Id.Amount);
            EditText basePrice = (EditText) dialog.FindViewById(Resource.Id.BasePrice);
            EditText discount = (EditText) dialog.FindViewById(Resource.Id.Discount);
            TextView endprice = (TextView) dialog.FindViewById(Resource.Id.Endprice);
            Button btnOrder = (Button)dialog.FindViewById(Resource.Id.btnOrder);
            Button btnCancel = (Button)dialog.FindViewById(Resource.Id.btnCancelOrder);
            itemDesc = item.description;
            itemName.Text = item.no;
            amount.Text = "";
            amount.Hint = "Amount...";
            basePrice.Text = item.unitprice;
            discount.Text = "";
            discount.Hint = "Discount...";
            endprice.Text = "0.00";
            dialog.Show();
            amount.AfterTextChanged += delegate{
                ThreadPool.QueueUserWorkItem(o =>
                {
                    amount_TextChanged(this, null);
                });
            };
            btnOrder.Click += delegate{
                btnOrder_Click(this,null);
            };
        }

        private void amount_TextChanged(Object sender, EventArgs e)
        {
            EditText amount = (EditText)dialog.FindViewById(Resource.Id.Amount);
            EditText itemName = (EditText)dialog.FindViewById(Resource.Id.itemName);
            EditText basePrice = (EditText)dialog.FindViewById(Resource.Id.BasePrice);
            EditText discount = (EditText)dialog.FindViewById(Resource.Id.Discount);
            TextView endprice = (TextView)dialog.FindViewById(Resource.Id.Endprice);
            Thread.Sleep(500);
            if (!amount.Text.Equals(""))
            {
                String price = service.getPrice(itemName.Text, amount.Text);
                Activity.RunOnUiThread(() =>
                {
                    endprice.Text = price;
                });
            }
        }

        private void btnOrder_Click(Object sender, EventArgs e)
        {
            if (item_list_view_adapter == null)
                item_list_view_adapter = new ItemListViewAdapter(Activity, Resource.Layout.item_list_element, Resource.Id.DescItem, new JavaList<ItemSingle>());
            
            EditText amount = (EditText)dialog.FindViewById(Resource.Id.Amount);
            EditText itemName = (EditText)dialog.FindViewById(Resource.Id.itemName);
            EditText basePrice = (EditText)dialog.FindViewById(Resource.Id.BasePrice);
            EditText discount = (EditText)dialog.FindViewById(Resource.Id.Discount);
            TextView endprice = (TextView)dialog.FindViewById(Resource.Id.Endprice);
            if (!amount.Text.Equals(""))
            {
                String id = itemName.Text;
                ItemAdapter temp_adapter = (ItemAdapter)actv_Item.Adapter;
                ItemSingle item = (ItemSingle)temp_adapter.getItem(id);
                item.quantity = amount.Text;
                item.endprice = endprice.Text;
                item_list_view_adapter.Add(item);
                items_list.Adapter = item_list_view_adapter;
                item_list_view_adapter.NotifyDataSetChanged();
                actv_Item.Text = "";
                dialog.Dismiss();
            }
            else
            {
                Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(Activity, "Please enter the Amount!", ToastLength.Short).Show();
                });
            }
        }

        private void btnCancelOrder_Click(Object sender, EventArgs e)
        {
            actv_Item.Text = "";
            dialog.Dismiss();
        }
    }
}