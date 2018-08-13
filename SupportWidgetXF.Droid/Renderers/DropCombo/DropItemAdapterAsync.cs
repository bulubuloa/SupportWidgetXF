using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using Xamarin.Forms.Platform.Android;
using Object = Java.Lang.Object;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class ChemicalFilterAsync : Filter
    {
        private DropItemAdapterAsync dropItemAdapter;

        public ChemicalFilterAsync(DropItemAdapterAsync dropItemAdapter)
        {
            this.dropItemAdapter = dropItemAdapter;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var returnObj = new FilterResults();
            var results = new List<IAutoDropItem>();
            results.AddRange(dropItemAdapter.originalData);

            returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
            returnObj.Count = results.Count;
            constraint.Dispose();
            return returnObj;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            if(results.Values!=null)
            {
                using (var values = results.Values)
                    dropItemAdapter.items = values.ToArray<Object>().Select(r => r.ToNetObject<IAutoDropItem>()).ToList();

                dropItemAdapter.NotifyDataSetChanged();
                constraint.Dispose();
                results.Dispose();
            }
        }
    }

    public class DropItemAdapterAsync : ArrayAdapter
    {
        public List<IAutoDropItem> originalData, items;
        private Context mContext;
        private SupportAutoComplete ConfigStyle;
        public Filter Filter { get; private set; }

        public DropItemAdapterAsync(Context context, List<IAutoDropItem> storeDataLst, SupportAutoComplete _ConfigStyle) : base(context,0)
        {
            originalData = storeDataLst;
            items = storeDataLst;
            mContext = context;
            ConfigStyle = _ConfigStyle;
            Filter = new ChemicalFilterAsync(this);
            items = new List<IAutoDropItem>();
        }

        public override int Count => items.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView txtTitle = null, txtDescription = null, txtSeperator = null;
            ImageView imgIcon = null;
            IAutoDropItem item = items[position];

            if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.TitleWithDescription)
            {
                convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_description, parent, false);
                txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
            }
            else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.IconAndTitle)
            {
                convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_icon, parent, false);
                imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
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

            txtTitle.Text = item.IF_GetTitle();
            if(txtDescription!=null)
            {
                txtDescription.Text = item.IF_GetDescription();
                txtDescription.SetTextColor(ConfigStyle.DescriptionTextColor.ToAndroid());
            }
            txtSeperator.SetBackgroundColor(ConfigStyle.SeperatorColor.ToAndroid());

            try
            {
                if (imgIcon != null)
                {
                    if (item.IF_GetIcon() != null)
                    {
                        var image = Context.Resources.GetIdentifier(item.IF_GetIcon(), "drawable", Context.PackageName);
                        imgIcon.SetImageResource(image);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return convertView;
        }
    }
}