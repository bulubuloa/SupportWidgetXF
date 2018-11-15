using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Request;
using SupportWidgetXF.Models;
using static Android.Support.V7.Widget.RecyclerView;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class GalleryDirectoryNewAdapter : ArrayAdapter
    {
        private Context context;
        private ViewHolder viewHolder;
        private List<GalleryDirectory> galleryDirectories;

        private class ViewHolder : Java.Lang.Object
        {
            public TextView txtTitle, txtCount;
            public ImageView imgIcon;
        }

        public GalleryDirectoryNewAdapter(Context context, List<GalleryDirectory> galleries) : base(context, Resource.Layout.layout_directory)
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
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.layout_directory, parent, false);

                viewHolder.txtTitle = convertView.FindViewById<TextView>(Resource.Id.albumName);
                viewHolder.txtCount = convertView.FindViewById<TextView>(Resource.Id.albumCount);
                viewHolder.imgIcon = convertView.FindViewById<ImageView>(Resource.Id.imgIcon);
                convertView.Tag = (viewHolder);
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            var data = galleryDirectories[position];

            viewHolder.txtTitle.Text = data.IF_GetTitle();
            viewHolder.txtCount.Text = "(" + data.Images.Count + ")";

            var imgPath = data.Images[1].OriginalPath;
            Glide.With(context).Load(imgPath)
                .Apply(RequestOptions
                       .DiskCacheStrategyOf(DiskCacheStrategy.All)
                       .SkipMemoryCache(false)
                       .Format(DecodeFormat.PreferRgb565)
                       .OptionalCenterCrop())
                .Thumbnail(0.1f)
                .Into(viewHolder.imgIcon);

            return convertView;
        }
    }
}
