using System;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class GalleryItemSquare : LinearLayout
    {
        public GalleryItemSquare(Context context) : base(context)
        {
        }

        public GalleryItemSquare(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public GalleryItemSquare(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
        }
    }
}