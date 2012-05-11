using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Android.Net;
using iNAV.ws_test;
using iNAV.Helpers;
using System.Web.Services;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace iNAV
{
	
    class Logic
    {
		
		public ws_test.Test _Service=null;
        protected rootCustomer root = null;
        protected Customer[] customers = null;
        public bool customersLock = true;
        protected Item[] item = null;
        protected rootItem itemroot=null;
        public bool itemLock = true;

        public Logic()
        {

            this._Service = new ws_test.Test();
            CookieContainer cooks = new CookieContainer();
            _Service.CookieContainer = cooks;
            CredentialCache cache = new CredentialCache();
            cache.Add(new System.Uri(_Service.Url), "NTLM", new NetworkCredential("***", "***", "***.**"));
			_Service.UseDefaultCredentials=false;
            _Service.Credentials = cache;
			_Service.PreAuthenticate=true;
        }

        public bool proofUser(string user, string pass)
        {
			try{                
                return _Service.Authentication(user, pass);
			}
			catch(Exception e)
			{
				throw new System.Exception(e.Message);
			}
        }
		
		public void getCustomer(string search)
		{
		   // Create an instance of the WebService
		   
			   
		   // Make an Asynchronous Call by calling 
		   // the Begin method of the proxy class
            object mlock = new object();
            lock (mlock)
            {
                this.root = new rootCustomer();
                customersLock = true;
                try
                {
                    _Service.GetCustomers(search, ref this.root);
                    customers = (root.Customer);
                }
                catch (Exception er)
                {

                    throw er;
                }
                customersLock = false;
            }
		   // Do some process while the web 
		   // service is processing the request
           
		}

        public void getItems(string search)
        {
            // Create an instance of the WebService


            // Make an Asynchronous Call by calling 
            // the Begin method of the proxy class
            object mlock = new object();
            lock (mlock)
            {
                this.itemroot = new rootItem();
                itemLock = true;
                try
                {
                    //_Service.GetItems(search, ref this.root);
                    _Service.GetItems(search, ref this.itemroot);
                    item = (itemroot.Item);
                }
                catch (Exception er)
                {
                    throw er;
                }
                itemLock = false;
            }
            // Do some process while the web 
            // service is processing the request

        }

        public CustomerAdapter PopulateCustomers(Activity activity, int resourceId, int textsomething)
        {
            if (customersLock == false)
            {

                CustomerAdapter custs = null;//,new List<CustomerSingle>());
                JavaList<CustomerSingle> custsList = new JavaList<CustomerSingle>();
                if (customers != null)
                {
                    System.Collections.IEnumerator custEnum = customers.GetEnumerator();
                    custEnum.Reset();
                    int i = 0;
                    while (custEnum.MoveNext())
                    {
                        Customer active = (Customer)custEnum.Current;
                        custsList.Add(new CustomerSingle(active.No_, active.Name, active.Address, i));
                        i++;
                    }
                }
                custs = new CustomerAdapter(activity, resourceId, textsomething, custsList);
                return custs;
            }
            else
            {
                return null;
            }
        }

        public ItemAdapter PopulateItems(Activity activity, int resourceId, int textsomething)
        {
            if (itemLock == false)
            {
                JavaList<ItemSingle> activeItems = new JavaList<ItemSingle>();
                if (item != null)
                {
                    
                    System.Collections.IEnumerator itemEnum = item.GetEnumerator();
                    itemEnum.Reset();
                    int i = 0;
                    while (itemEnum.MoveNext())
                    {
                        Item active = (Item)itemEnum.Current;
                        activeItems.Add(new ItemSingle(active.No_,active.Description,active.Unit_List_Price,active.Unit_Price,active.Sales_Unit_of_Measure,i));
                        i++;
                    }
                }
                ItemAdapter items = new ItemAdapter(activity, resourceId, textsomething, activeItems);
                return items;
            }
            else
            {
                return null;
            }
        }

        public String WriteOrder(JavaList<ItemSingle> items, CustomerSingle customer)
        {
            SalesHeader header = new SalesHeader();
            header.Sell_To_Customer_No = customer.no;
            RootSalesHeader root = new RootSalesHeader();
            List<Lines> lines = new List<Lines>();
            foreach (var item in items)
            {
                Lines line = new Lines();
                line.No = item.no;
                line.Quantity = Convert.ToDecimal(item.quantity);
                line.Line_Discount_Pct = Convert.ToDecimal(item.discount);
                lines.Add(line);
            }
            header.Lines = lines.ToArray();
            root.SalesHeader.Lines.SetValue(header,0);
            return _Service.CreateSalesOrder(ref root);
        }

        public String getPrice(String itemNo, String amount)
        {
            SalesHeader head = new SalesHeader();
            Lines line= new Lines();
            
            line.No = itemNo;
            line.Quantity = Convert.ToDecimal(amount);
            List<Lines> lines = new List<Lines>();
            object lineObj = line;
            lines.Add(line);
            head.Lines = lines.ToArray();
            RootSalesHeader root = new RootSalesHeader();
            root.SalesHeader = head;
            double price=Convert.ToDouble(_Service.CreateFakeSalesOrder(ref root));
            return price.ToString();
        }
    
    }
    //public class Lines:Java.Lang.Object
    //{
    //    public String No { get; set; }
    //    public Decimal Quantity { get; set; }
    //    public Decimal UnitOfMeasure { get; set; }
    //    public Decimal Line_Discount_Pct { get; set; }
    //    public String Type { get; set; }

    //    public Lines()
    //    {

    //    }
    //}
}
