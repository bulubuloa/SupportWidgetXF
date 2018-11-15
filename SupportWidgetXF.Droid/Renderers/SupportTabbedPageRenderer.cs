using System;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Widget;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(SupportTabbedPage), typeof(SupportTabbedPageRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportTabbedPageRenderer : TabbedPageRenderer
    {
        protected SupportTabbedPage supportTabbedPage;

        /*
         * NavigationBottom
         */
        protected TabLayout tabLayout;
        protected ViewPager viewPager;
        protected BottomNavigationView bottomNavigationView;
        protected Android.Widget.RelativeLayout relativeLayout;

        public SupportTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportTabbedPage)
                {
                    supportTabbedPage = Element as SupportTabbedPage;

                    OnInitializeNavigationBottomControl();
                    InitializeModify();
                }
            }
        }

        protected virtual void OnInitializeNavigationBottomControl()
        {
            for (var i = 0; i < ChildCount; i++)
            {
                var view = GetChildAt(i);
                if (view is Android.Views.ViewGroup)
                {
                    if (view is Android.Widget.RelativeLayout)
                    {
                        relativeLayout = view as Android.Widget.RelativeLayout;
                        for (int j = 0; j < relativeLayout.ChildCount; j++)
                        {
                            var child = relativeLayout.GetChildAt(j);
                            if (child is TabLayout)
                                tabLayout = (TabLayout)child;
                            if (child is ViewPager)
                                viewPager = (ViewPager)child;
                            if (child is BottomNavigationView)
                                bottomNavigationView = (BottomNavigationView)child;
                        }
                    }
                }
                if (view is TabLayout)
                    tabLayout = (TabLayout)view;
                if (view is ViewPager)
                    viewPager = (ViewPager)view;
            }
        }

        protected virtual void InitializeModify()
        {
            try
            {
                if (bottomNavigationView != null && supportTabbedPage.IsShadow)
                {
                    if(supportTabbedPage.IsShadow)
                    {
                        if (relativeLayout != null)
                        {
                            var background = new LinearLayout(SupportWidgetXFSetup.Activity);
                            background.SetBackgroundResource(Resource.Drawable.shadowclonenavigation_top);
                            background.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent)
                            {
                                Height = 7
                            };
                            relativeLayout.AddView(background);
                            var param = background.LayoutParameters as Android.Widget.RelativeLayout.LayoutParams;
                            param.AddRule(LayoutRules.Above, bottomNavigationView.Id);
                        }
                    }
                    if(supportTabbedPage.TitleAndIconLayout == TabbedIconTitleArrange.OnlyIcon)
                    {
                        var scale = Resources.DisplayMetrics.Density;
                        var paddingDp = 9;
                        var dpAsPixels = (int)(paddingDp * scale + 0.5f);
                        bottomNavigationView.SetPadding(bottomNavigationView.PaddingLeft, dpAsPixels, bottomNavigationView.PaddingRight, bottomNavigationView.PaddingBottom);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}