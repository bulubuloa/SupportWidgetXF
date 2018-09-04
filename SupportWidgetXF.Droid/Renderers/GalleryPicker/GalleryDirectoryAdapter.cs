using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class GalleryDirectoryAdapter : ArrayAdapter
    {
        private class ViewHolder : Java.Lang.Object
        {
            public TextView txtTitle;
            public ImageView sortDown;
        }

        private Context context;
        private ViewHolder viewHolder;
        private List<GalleryDirectory> galleryDirectories;

        public GalleryDirectoryAdapter(Context context, List<GalleryDirectory> galleries) : base(context, Resource.Layout.adapter_photosfolder)
        {
            this.galleryDirectories = galleries;
            this.context = context;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override int Count => galleryDirectories.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {

                viewHolder = new ViewHolder();
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.layout_single_title_normal, parent, false);

                viewHolder.txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
                viewHolder.sortDown = convertView.FindViewById<ImageView>(Resource.Id.sortDown);
                convertView.Tag = (viewHolder);
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            convertView.SetBackgroundResource(Resource.Drawable.border_background);
            viewHolder.txtTitle.Text = galleryDirectories[position].Name;
            viewHolder.sortDown.Visibility = ViewStates.Visible;
            return convertView;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {

                viewHolder = new ViewHolder();
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.layout_single_title_normal, parent, false);

                viewHolder.txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
                convertView.Tag = (viewHolder);
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            viewHolder.txtTitle.Text = galleryDirectories[position].Name;
            return convertView;
        }
    }
}