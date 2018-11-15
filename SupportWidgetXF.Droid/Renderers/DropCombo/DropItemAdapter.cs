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
using SupportWidgetXF.Widgets.Interface;
using Xamarin.Forms.Platform.Android;
using Object = Java.Lang.Object;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class ChemicalFilter : Filter
    {
       private DropItemAdapter dropItemAdapter;

        public ChemicalFilter(DropItemAdapter dropItemAdapter)
        {
            this.dropItemAdapter = dropItemAdapter;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var returnObj = new FilterResults();
            var results = new List<IAutoDropItem>();

            if (dropItemAdapter.originalData == null)
                dropItemAdapter.originalData = dropItemAdapter.items;

            if (constraint == null) 
                return returnObj;

            if (dropItemAdapter.originalData != null && dropItemAdapter.originalData.Any())
            {
                var key = constraint.ToString().ToLower();
                results.AddRange(dropItemAdapter.originalData.Where(drop => drop.IF_GetTitle().ToLower().Contains(key)));
            }

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

    public class DropItemAdapter : ArrayAdapter, IFilterable
    {
        public List<IAutoDropItem> originalData, items;
        private Context mContext;
        private SupportAutoComplete ConfigStyle;
        public Filter Filter { get; private set; }
        private IDropItemSelected IDropItemSelected;

        public DropItemAdapter(Context context, List<IAutoDropItem> storeDataLst, SupportAutoComplete _ConfigStyle,IDropItemSelected dropItemSelected) : base(context,0)
        {
            originalData = storeDataLst;
            mContext = context;
            ConfigStyle = _ConfigStyle;
            Filter = new ChemicalFilter(this);
            items = new List<IAutoDropItem>();
            IDropItemSelected = dropItemSelected;
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
            Button bttClick;
            CheckBox checkBox = null;

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
            bttClick = convertView.FindViewById<Button>(Resource.Id.bttClick);
                       

            txtTitle.Text = item.IF_GetTitle();
            if(txtDescription!=null)
            {
                txtDescription.Text = item.IF_GetDescription();
                txtDescription.SetTextColor(ConfigStyle.DescriptionTextColor.ToAndroid());
            }
            txtSeperator.SetBackgroundColor(ConfigStyle.SeperatorColor.ToAndroid());

            bttClick.Click += (sender, e) => {
                IDropItemSelected.IF_ItemSelectd(position);
            };

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