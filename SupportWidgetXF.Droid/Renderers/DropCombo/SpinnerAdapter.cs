using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using Xamarin.Forms.Platform.Android;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class SpinnerAdapter : ArrayAdapter
    {
        public List<IAutoDropItem> originalData, items;
        private Context mContext;
        private SupportDropList ConfigStyle;
        private GradientDrawable gradientDrawable;

        public SpinnerAdapter(Context context, List<IAutoDropItem> storeDataLst, SupportDropList _ConfigStyle) : base(context, 0)
        {
            originalData = storeDataLst;
            items = storeDataLst;
            mContext = context;
            ConfigStyle = _ConfigStyle;

            gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { Android.Graphics.Color.White, Android.Graphics.Color.White });
            gradientDrawable.SetStroke((int)ConfigStyle.CornerWidth, ConfigStyle.CornerColor.ToAndroid());
            gradientDrawable.SetShape(ShapeType.Rectangle);
            gradientDrawable.SetCornerRadius((float)ConfigStyle.CornerRadius);
        }

        public override int Count => items.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            TextView txtTitle = null, txtDescription = null, txtSeperator = null;
            ImageView imgIcon = null;
            Button bttClick;
            IAutoDropItem item = items[position];
            CheckBox checkBox = null;

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
            checkBox = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);

            txtTitle.Text = item.IF_GetTitle();
            if (txtDescription != null)
            {
                txtDescription.Text = item.IF_GetDescription();
                txtDescription.SetTextColor(ConfigStyle.DescriptionTextColor.ToAndroid());
            }
            txtSeperator.SetBackgroundColor(ConfigStyle.SeperatorColor.ToAndroid());
            bttClick.Visibility = ViewStates.Gone;

            checkBox.Visibility = ConfigStyle.IsAllowMultiSelect ? ViewStates.Visible : ViewStates.Gone;

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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView txtTitle = null, txtDescription = null, txtSeperator = null;
            ImageView imgIcon = null, sort_down = null;
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
            sort_down = convertView.FindViewById<ImageView>(Resource.Id.sortDown);

            txtTitle.Text = item.IF_GetTitle();
            if (txtDescription != null)
            {
                txtDescription.Text = item.IF_GetDescription();
                txtDescription.SetTextColor(ConfigStyle.DescriptionTextColor.ToAndroid());
            }
            txtSeperator.SetBackgroundColor(Android.Graphics.Color.Transparent);

            bttClick.Visibility = ViewStates.Gone;
            sort_down.Visibility = ViewStates.Visible;

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
            if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
            {
                convertView.SetBackgroundDrawable(gradientDrawable);
            }
            else
            {
                convertView.SetBackground(gradientDrawable);
            }
            return convertView;
        }
    }
}
