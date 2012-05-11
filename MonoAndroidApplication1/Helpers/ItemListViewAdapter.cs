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

    public class ItemListViewAdapter : ArrayAdapter<ItemSingle>, IFilterable
    {

        public JavaList<ItemSingle> _itemList;
        public JavaList<ItemSingle> _originalList = new JavaList<ItemSingle>();
        private Activity _activity;
        private ItemListViewAdapterFilter filter = null;

        public ItemListViewAdapter(Activity activity, int resourceId, int textsomething, JavaList<ItemSingle> items)
            : base(activity, resourceId, textsomething, items)
        {
            this._activity = activity;
            this._itemList = items;
            CloneItems(items);
        }

        public JavaList<ItemSingle> getItemList()
        {
            return _itemList;
        }

        public void CloneItems(JavaList<ItemSingle> items)
        {
            foreach (var item in items)
            {
                _originalList.Add(item);
            }
        }

        public void Add(ItemSingle item)
        {
            _itemList.Add(item);
            _originalList.Add(item);
        }

        public override int Count
        {
            get { return _itemList.Count; }
        }

        public override long GetItemId(int position)
        {
            return _itemList[position].id;
        }

        public ItemSingle getItem(String no)
        {
            foreach (var item in _itemList)
            {
                if (no.Equals(item.no))
                {
                    return item;
                }
            }
            foreach (var item in _originalList)
            {
                if (no.Equals(item.no))
                {
                    return item;
                }
            }
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.item_list_element, parent, false);
            var description = view.FindViewById<TextView>(Resource.Id.DescItem);
            var no = view.FindViewById<TextView>(Resource.Id.NoItem);
            var price = view.FindViewById<TextView>(Resource.Id.PriceItem);
            var amount = view.FindViewById<TextView>(Resource.Id.AmountItem);
            description.Text = _itemList[position].description;
            no.Text = _itemList[position].no;
            price.Text = _itemList[position].endprice;
            amount.Text = _itemList[position].quantity;

            return view;
        }

        public override Filter Filter
        {
            get
            {
                if (filter == null)
                {
                    filter = new ItemListViewAdapterFilter(this);
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

    //public class ItemSingle : Java.Lang.Object
    //{
    //    public string no { get; set; }
    //    public string description { get; set; }
    //    public string listprice { get; set; }
    //    public string unitprice { get; set; }
    //    public string quantity { get; set; }
    //    public string discount { get; set; }
    //    public string endprice { get; set; }
    //    public int id { get; set; }

    //    public ItemSingle(string no, string description, string listprice, string unitprice, int id)
    //    {
    //        this.description = description;
    //        this.listprice = listprice;
    //        this.unitprice = unitprice;
    //        this.no = no;
    //        this.id = id;
    //        this.quantity = "0";
    //        this.discount = "0";
    //    }

    //    public override string ToString()
    //    {
    //        return no + " " + description;
    //    }
    //}

    public class ItemListViewAdapterFilter : Filter
    {
        ItemListViewAdapter adapter;

        public ItemListViewAdapterFilter(ItemListViewAdapter _adapter)
        {
            adapter = _adapter;
        }

        protected override void PublishResults(Java.Lang.ICharSequence constraint, Filter.FilterResults results)
        {

            var getItemThem = results.Values as JavaList<ItemSingle>;

            // Clone items so they are local.
            var localItems = new JavaList<ItemSingle>();
            foreach (var item in getItemThem)
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
                JavaList<ItemSingle> result = new JavaList<ItemSingle>();
                foreach (var item in adapter._itemList)
                {
                    if (item.description.ToLowerInvariant().StartsWith(prefix.ToString().ToLowerInvariant()))
                        result.Add(item);
                    else
                    {
                        String value = item.description.ToLowerInvariant();
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