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

namespace iNAV.Helpers
{

    public class CustomerAdapter : ArrayAdapter<CustomerSingle>, IFilterable
    {
        
        public JavaList<CustomerSingle> _contactList;
        public JavaList<CustomerSingle> _originalList = new JavaList<CustomerSingle>();
        private Activity _activity;
        private CustomerAdapterFilter filter = null;

        public CustomerAdapter(Activity activity, int resourceId, int textsomething, JavaList<CustomerSingle> items)
            : base(activity, resourceId, textsomething, items)
        {
            _activity = activity;
            _contactList = items;
            CloneItems(items);
        }

        public void CloneItems(JavaList<CustomerSingle> items)
        {
            foreach (var item in items)
            {
                _originalList.Add(item);
            }
        }

        public JavaList<CustomerSingle> getCustomerList()
        {
            return _contactList;
        }

        public void Add(CustomerSingle item)
        {
 	         _contactList.Add(item);
             _originalList.Add(item);
        }

        public override int Count
        {
            get { return _contactList.Count; }
        }

        public override long GetItemId(int position)
        {
            return _contactList[position].id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.details, parent, false);
            var contactName = view.FindViewById<TextView>(Resource.Id.Name);
            var contactAddress = view.FindViewById<TextView>(Resource.Id.Address);
            contactName.Text = _contactList[position].name;
            contactAddress.Text = _contactList[position].address;

            return view;
        }

        public override Filter Filter
        {
            get
            {
                if (filter == null)
                {
                    filter = new CustomerAdapterFilter(this);
                    return filter;
                }
                else
                    return filter;
            }
        }

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
        }

        public override void NotifyDataSetInvalidated()
        {
            base.NotifyDataSetInvalidated();
        }
    }

    public class CustomerSingle:Java.Lang.Object
    {
        public string no { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int id { get; set; }

        public CustomerSingle(string no, string name, string address, int id):base()
        {
            this.address = address;
            this.name = name;
            this.no = no;
            this.id = id;
        }

        public override String ToString()
        {
            return no + " " + name + "\n" + address;
        }
    }

    public class CustomerAdapterFilter : Filter
    {
        CustomerAdapter adapter;

        public CustomerAdapterFilter(CustomerAdapter _adapter)
        {
            adapter = _adapter;
        }

        protected override void PublishResults(Java.Lang.ICharSequence constraint, Filter.FilterResults results)
        {
            var getThem = results.Values as JavaList<CustomerSingle>;

            // Clone items so they are local.
            var localItems = new JavaList<CustomerSingle>();
            foreach (var item in getThem)
            {
                localItems.Add(item);
            }

            adapter.NotifyDataSetChanged();
            adapter.Clear();
            foreach (var item in localItems)
            {
                adapter.Add(item);
            }
        }

        protected override FilterResults PerformFiltering(Java.Lang.ICharSequence prefix)
        {
            JavaList filtered;
            if (prefix != null)
            {
                JavaList<CustomerSingle> result = new JavaList<CustomerSingle>();
                foreach (var item in adapter._contactList)
                {
                    String searchable = item.no + " " + item.name;
                    if (searchable.ToLowerInvariant().StartsWith(prefix.ToString().ToLowerInvariant()))
                        result.Add(item);
                    else
                    {
                        String value = searchable.ToLowerInvariant();
                        String[] valueSplit = value.Split(' ');
                        foreach (var word in valueSplit)
                        {
                            if (word.StartsWith(prefix.ToString().ToLowerInvariant()))
                                result.Add(item);
                        }
                    }
                }
                filtered = result;
            }
            else
            {
                filtered = adapter._originalList;
            }

            FilterResults results = new FilterResults();
            results.Values = filtered;
            results.Count = filtered.Count;
            return results;
        }
    }
}