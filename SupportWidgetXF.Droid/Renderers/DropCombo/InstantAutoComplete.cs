using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SupportWidgetXF.Droid.Renderers.DropCombo
{
    public class InstantAutoComplete : AutoCompleteTextView
    {
        public InstantAutoComplete(Context context) : base(context)
        {
        }

        public InstantAutoComplete(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public InstantAutoComplete(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public InstantAutoComplete(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public InstantAutoComplete(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes, Resources.Theme popupTheme) : base(context, attrs, defStyleAttr, defStyleRes, popupTheme)
        {
        }

        protected InstantAutoComplete(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }


        public override bool EnoughToFilter()
        {
            return true;
        }

        protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, Rect previouslyFocusedRect)
        {
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);
            if (gainFocus && Adapter != null)
            {
                PerformFiltering(Text, 0);
            }
        }
    }
}