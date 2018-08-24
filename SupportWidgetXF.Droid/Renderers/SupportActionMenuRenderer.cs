using System.ComponentModel;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Lang.Reflect;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportActionMenu), typeof(SupportActionMenuRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportActionMenuRenderer : SupportDropRenderer<SupportActionMenu, Android.Widget.Button>
    {
        private PopupMenu popupMenu;

        public SupportActionMenuRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SupportActionMenu> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportActionMenu)
                {
                    SupportView = Element as SupportActionMenu;
                    SyncItemSource();
                    InitilizeMenu();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportActionMenu.ItemsSource)))
            {
                SyncItemSource();
                InitilizeMenu();
            }
        }

        protected override void OnInitializeOriginalView()
        {
            base.OnInitializeOriginalView();
            OriginalView = new Android.Widget.Button(Context);
            OriginalView.SetPadding(0, 0, 0, 0);
            OriginalView.Click += (sender, e) => {
                OnShowActionMenu();
            };
        }

        protected virtual void InitilizeMenu()
        {
            if(Control!=null)
            {
                popupMenu = new PopupMenu(SupportWidgetXFSetup.Activity, Control);

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
            }
        }

        protected virtual void OnShowActionMenu()
        {
            if(popupMenu!=null)
                popupMenu.Show();
        }

        void PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            if (SupportItemList[e.Item.Order - 1].IF_GetAction()!=null)
                SupportItemList[e.Item.Order - 1].IF_GetAction()();
        }
    }
}