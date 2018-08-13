using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using SupportWidgetXF.Widgets.Interface;
using Xamarin.Forms.Platform.Android;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class DropItemAdapterAsync : ArrayAdapter
    {
        public List<IAutoDropItem> originalData, items;
        private Context mContext;
        private SupportAutoComplete ConfigStyle;
        private IDropItemSelected IDropItemSelected;

        public DropItemAdapterAsync(Context context, List<IAutoDropItem> storeDataLst, SupportAutoComplete _ConfigStyle, IDropItemSelected dropItemSelected) : base(context,0)
        {
            originalData = storeDataLst;
            items = storeDataLst;
            mContext = context;
            ConfigStyle = _ConfigStyle;
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

            bttClick.Click += (sender, e) => 
            {
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