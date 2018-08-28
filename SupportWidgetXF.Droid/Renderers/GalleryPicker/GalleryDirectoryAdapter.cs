using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class GalleryDirectoryAdapter : ArrayAdapter<GalleryDirectory>
    {
        private class ViewHolder : Java.Lang.Object
        {
            public TextView tv_foldern, tv_foldersize;
            public ImageView iv_image;
        }

        Context context;
        ViewHolder viewHolder;
        List<GalleryDirectory> galleryDirectories;

        public GalleryDirectoryAdapter(Context context, List<GalleryDirectory> galleries) : base(context, Resource.Layout.adapter_photosfolder)
        {
            this.galleryDirectories = galleries;
            this.context = context;
        }

        public override int Count => galleryDirectories.Count;

        public override int GetItemViewType(int position)
        {
            return position;
        }

        public override int ViewTypeCount => galleryDirectories.Count > 0 ? galleryDirectories.Count : 1;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {

                viewHolder = new ViewHolder();
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.adapter_photosfolder, parent, false);
                viewHolder.tv_foldern = convertView.FindViewById<TextView>(Resource.Id.tv_folder);
                viewHolder.tv_foldersize = convertView.FindViewById<TextView>(Resource.Id.tv_folder2);
                viewHolder.iv_image = convertView.FindViewById<ImageView>(Resource.Id.iv_image);

                convertView.Tag = (viewHolder);
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            viewHolder.tv_foldern.Text = galleryDirectories[position].Name;
            viewHolder.tv_foldersize.Text = galleryDirectories[position].Images.Count + "";


            Glide.With(context).Load(galleryDirectories[position].Images[0]).Into(viewHolder.iv_image);

            return convertView;
        }
    }
}