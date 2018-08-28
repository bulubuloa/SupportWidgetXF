using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class GalleryImageAdapter : ArrayAdapter<GalleryDirectory>
    {
        private class ViewHolder : Java.Lang.Object
        {
            public TextView tv_foldern, tv_foldersize;
            public ImageView iv_image;
        }

        Context context;
        ViewHolder viewHolder;
        List<GalleryDirectory> galleryDirectories;
        int Position;

        public GalleryImageAdapter(Context context, List<GalleryDirectory> galleries, int position) : base(context, Resource.Layout.adapter_photosfolder)
        {
            this.galleryDirectories = galleries;
            this.context = context;
            this.Position = position;
        }

        public override int Count => galleryDirectories[Position].Images.Count;

        public override int GetItemViewType(int position)
        {
            return position;
        }

        public override int ViewTypeCount => galleryDirectories[Position].Images.Count > 0 ? galleryDirectories[Position].Images.Count : 1;

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

            viewHolder.tv_foldern.Visibility = ViewStates.Gone;
            viewHolder.tv_foldersize.Visibility = ViewStates.Gone;


            Glide.With(context).Load(galleryDirectories[Position].Images[position]).Into(viewHolder.iv_image);

            return convertView;
        }
    }
}