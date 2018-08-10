using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class ListFilter : Filter
    {
        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            throw new NotImplementedException();
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            throw new NotImplementedException();
        }
    }

    public class DropItemAdapter : ArrayAdapter
    {
        private List<IAutoDropItem> dataList;
        private Context mContext;
        private SupportAutoComplete ConfigStyle;

        private ListFilter listFilter = new ListFilter();
        private List<String> dataListAllItems;

        public DropItemAdapter(Context context, List<IAutoDropItem> storeDataLst, SupportAutoComplete _ConfigStyle) : base(context,0)
        {
            dataList = storeDataLst;
            mContext = context;
            ConfigStyle = _ConfigStyle;
        }

        public override int Count => dataList.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView txtTitle = null, txtDescription = null, txtSeperator = null;
            ImageView imgIcon = null;
            IAutoDropItem item = dataList[position];

            if (convertView == null)
            {
                if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.TitleWithDescription)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_description, parent, false);
                    txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
                    imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                }
                else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.IconAndTitle)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_icon, parent, false);
                }
                else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.FullTextAndIcon)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_full_text_and_icon, parent, false);
                    txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
                    imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                }
                else
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_single_title, parent, false);
                }
                txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
                txtSeperator = convertView.FindViewById<TextView>(Resource.Id.txtSeperator);
            }

            txtTitle.Text = item.IF_GetTitle();
            if(txtDescription!=null)
                txtDescription.Text = item.IF_GetDescription();

            return convertView;
        }
    }
}
