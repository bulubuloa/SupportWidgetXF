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
using SupportWidgetXF.Widgets.Interface;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class GalleryImageAdapter : ArrayAdapter<GalleryDirectory>
    {
        private class ViewHolder : Java.Lang.Object
        {
            public ImageView imageView;
            public CheckBox checkBox;
            public Button buttonCheckbox, buttonClick;
            public LinearLayout cover;
        }

        private Context context;
        private ViewHolder viewHolder;
        private List<GalleryDirectory> galleryDirectories;
        private int Position;
        private IGalleryPickerSelected IGalleryPickerSelected;

        public GalleryImageAdapter(Context context, List<GalleryDirectory> galleries, int position, IGalleryPickerSelected galleryPickerSelected) : base(context, Resource.Layout.adapter_photosfolder)
        {
            this.galleryDirectories = galleries;
            this.context = context;
            this.Position = position;
            this.IGalleryPickerSelected = galleryPickerSelected;
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
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.adapter_photosfolder, null);
                viewHolder.imageView = convertView.FindViewById<ImageView>(Resource.Id.iv_image);
                viewHolder.checkBox = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);
                viewHolder.buttonCheckbox = convertView.FindViewById<Button>(Resource.Id.buttoCheckbox);
                viewHolder.buttonClick = convertView.FindViewById<Button>(Resource.Id.buttonClick);
                viewHolder.cover = convertView.FindViewById<LinearLayout>(Resource.Id.cover);

                viewHolder.buttonCheckbox.Click += (object sender, EventArgs e) => {
                    IGalleryPickerSelected.IF_ImageSelected(Position, position,null,null);
                };
                viewHolder.buttonClick.Click += (object sender, EventArgs e) => {
                    IGalleryPickerSelected.IF_CameraSelected(position);
                };
                convertView.Tag = (viewHolder);
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            var item = galleryDirectories[Position].Images[position];

            viewHolder.buttonClick.SetBackgroundColor(Android.Graphics.Color.Transparent);
            viewHolder.buttonCheckbox.SetBackgroundColor(Android.Graphics.Color.Transparent);

            if (string.IsNullOrEmpty(item.OriginalPath))
            {
                viewHolder.imageView.SetImageResource(Resource.Drawable.camera);
                viewHolder.checkBox.Visibility = ViewStates.Gone;
                viewHolder.buttonCheckbox.Visibility = ViewStates.Gone;
                viewHolder.buttonClick.Visibility = ViewStates.Visible;
            }
            else
            {
                viewHolder.checkBox.Visibility = ViewStates.Visible;
                viewHolder.buttonCheckbox.Visibility = ViewStates.Visible;
                viewHolder.buttonClick.Visibility = ViewStates.Gone;

                Glide.With(context).Load(item.OriginalPath)
                 .Apply(RequestOptions
                        .DiskCacheStrategyOf(DiskCacheStrategy.All)
                        .SkipMemoryCache(false)
                        .Format(DecodeFormat.PreferRgb565)
                        .OptionalCenterCrop())
                 .Thumbnail(0.1f)
                 .Into(viewHolder.imageView);
               
                viewHolder.checkBox.Checked = item.Checked;

                if (item.Checked)
                    viewHolder.cover.SetBackgroundResource(Resource.Color.colorWi);
                else
                    viewHolder.cover.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }

            return convertView;
        }
    }
}