using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using SupportWidgetXF.Widgets.Interface;
using Xamarin.Forms.Platform.Android;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class SpinnerMultiSelectAdapter : ArrayAdapter
    {
        private class ViewHolder : Java.Lang.Object
        {
            public TextView txtTitle = null, txtDescription = null, txtSeperator = null;
            public ImageView imgIcon = null, sort_down;
            public Button bttClick;
            public CheckBox checkBox = null;
        }

        private List<IAutoDropItem> items;
        private Context mContext;
        private SupportDropList ConfigStyle;
        private IDropItemSelected IDropItemSelected;
        private GradientDrawable gradientDrawable;

        public SpinnerMultiSelectAdapter(Context context, List<IAutoDropItem> storeDataLst, SupportDropList _ConfigStyle, IDropItemSelected _IDropItemSelected) : base(context, 0)
        {
            items = storeDataLst;
            mContext = context;
            ConfigStyle = _ConfigStyle;
            IDropItemSelected = _IDropItemSelected;

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

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder = null;

            if (convertView == null)
            {
                holder = new ViewHolder();

                if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.TitleWithDescription)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_description, parent, false);
                    //holder.txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
                }
                else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.IconAndTitle)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_icon, parent, false);
                    //holder.imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                }
                else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.FullTextAndIcon)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_full_text_and_icon, parent, false);
                    //holder.txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
                    //holder.imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                }
                else
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_single_title, parent, false);
                }

                holder.sort_down = convertView.FindViewById<ImageView>(Resource.Id.sortDown);
                holder.txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
                holder.txtSeperator = convertView.FindViewById<TextView>(Resource.Id.txtSeperator);
                holder.bttClick = convertView.FindViewById<Button>(Resource.Id.bttClick);
                holder.checkBox = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);

                convertView.Tag = (holder);
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            holder.txtTitle.Text = string.Join(", ", items.Where(Xamarin => Xamarin.IF_GetChecked()).Select(ita => ita.IF_GetTitle()));

            holder.checkBox.Tag = (position);
            holder.sort_down.Visibility = ViewStates.Visible;
            holder.checkBox.Visibility = ViewStates.Gone;
            //holder.imgIcon.Visibility = ViewStates.Gone;
            holder.bttClick.Visibility = ViewStates.Gone;

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

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            IAutoDropItem item = items[position];
            ViewHolder holder = null;

            if(convertView==null)
            {
                holder = new ViewHolder();
                if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.TitleWithDescription)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_description, parent, false);
                    holder.txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
                }
                else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.IconAndTitle)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_title_and_icon, parent, false);
                    holder.imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                }
                else if (ConfigStyle.DropMode == SupportAutoCompleteDropMode.FullTextAndIcon)
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_full_text_and_icon, parent, false);
                    holder.txtDescription = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
                    holder.imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                }
                else
                {
                    convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_single_title, parent, false);
                }

                holder.txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
                holder.txtSeperator = convertView.FindViewById<TextView>(Resource.Id.txtSeperator);
                holder.bttClick = convertView.FindViewById<Button>(Resource.Id.bttClick);
                holder.checkBox = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);
                holder.bttClick.Click += (sender, e) => {
                    IDropItemSelected.IF_ItemSelectd(position);
                };

                convertView.Tag = (holder);
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            holder.txtTitle.Text = item.IF_GetTitle();
            if (holder.txtDescription != null)
            {
                holder.txtDescription.Text = item.IF_GetDescription();
                holder.txtDescription.SetTextColor(ConfigStyle.DescriptionTextColor.ToAndroid());
            }
            holder.txtSeperator.SetBackgroundColor(ConfigStyle.SeperatorColor.ToAndroid());
            holder.checkBox.Visibility = ConfigStyle.IsAllowMultiSelect ? ViewStates.Visible : ViewStates.Gone;
            holder.checkBox.Tag = (position);
            holder.checkBox.Checked = item.IF_GetChecked();

            try
            {
                if (holder.imgIcon != null)
                {
                    if (item.IF_GetIcon() != null)
                    {
                        var image = Context.Resources.GetIdentifier(item.IF_GetIcon(), "drawable", Context.PackageName);
                        holder.imgIcon.SetImageResource(image);
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
