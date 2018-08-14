using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Java.Lang.Reflect;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportActionMenu), typeof(SupportActionMenuRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
	public class SupportActionMenuRenderer : ButtonRenderer
    {
        private SupportActionMenu supportButton;
        private PopupMenu popupMenu;

        private List<IAutoDropItem> SupportItemList = new List<IAutoDropItem>();
        private void NotifyAdapterChanged()
        {
            SupportItemList.Clear();
            if (supportButton.MenuItemsSource != null)
            {
                SupportItemList.AddRange(supportButton.MenuItemsSource.ToList());
            }
        }

        public SupportActionMenuRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportActionMenu)
                {
                    supportButton = Element as SupportActionMenu;
                    if (Control != null)
                    {
                        Control.SetAllCaps(false);
                        Control.SetPadding(0, 0, 0, 0);
                        Control.TextAlignment = Android.Views.TextAlignment.Center;
                        NotifyAdapterChanged();
                        try
                        {
                            Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, null, null);

                            if (supportButton.Image != null)
                            {
                                var image = Context.GetDrawable(supportButton.Image);
                                if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
                                {
                                    Control.SetBackgroundDrawable(image);
                                }
                                else
                                {
                                    Control.SetBackground(image);
                                }
                                Control.SetPadding(3, 3, 3, 3);
                                //Control.SetCompoundDrawablesWithIntrinsicBounds(null,null, image, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                        supportButton.Clicked += SupportButton_Clicked;
                    }
                }
            }
        }

        void SupportButton_Clicked(object sender, EventArgs e)
        {
            InitilizeMenu(Control);
        }

        private void InitilizeMenu(Android.Widget.Button button)
        {
            popupMenu = new PopupMenu(SupportWidgetXFSetup.Activity, button);

            Field field = popupMenu.Class.GetDeclaredField("mPopup");
            field.Accessible = true;
            Java.Lang.Object menuPopupHelper = field.Get(popupMenu);
            Method setForceIcons = menuPopupHelper.Class.GetDeclaredMethod("setForceShowIcon", Java.Lang.Boolean.Type);
            setForceIcons.Invoke(menuPopupHelper, true);

            int max = SupportItemList.Count;
            for (int i = 0; i < max; i++)
            {
                var item = SupportItemList[i];
                popupMenu.Menu.Add(Android.Views.Menu.None, i + 1, i + 1, new Java.Lang.String(item.IF_GetTitle()));
                var itemDone = popupMenu.Menu.GetItem(i);
                var image = Context.GetDrawable(item.IF_GetIcon());
                itemDone.SetIcon(image);
            }

            popupMenu.MenuItemClick += PopupMenu_MenuItemClick;
            popupMenu.Show();
        }

        void PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            if(SupportItemList[e.Item.Order - 1].IF_GetAction()!=null)
            {
                SupportItemList[e.Item.Order - 1].IF_GetAction()();
            }
        }
    }
}