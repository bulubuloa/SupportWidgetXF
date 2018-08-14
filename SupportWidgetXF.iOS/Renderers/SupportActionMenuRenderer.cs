using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.DropCombo;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using SupportWidgetXF.Widgets.Interface;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportActionMenu), typeof(SupportActionMenuRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportActionMenuRenderer : ButtonRenderer,IDropItemSelected
    {
        private SupportActionMenu supportButton;
        private int MinWidth = 130;
        private int ExtendWidth = 10;
        private int HeightOfRow = 40;

        private UIView converView;
        private UITableView tableView;
        private DropItemSource dropSource;
        private bool IsShowDropList = false;

        private List<IAutoDropItem> SupportItemList = new List<IAutoDropItem>();
        private void NotifyAdapterChanged()
        {
            SupportItemList.Clear();
            if (supportButton.MenuItemsSource != null)
            {
                SupportItemList.AddRange(supportButton.MenuItemsSource.ToList());
            }
            tableView.ReloadData();
        }

        async void SupportButton_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(100);
            TextFieldTapHandle();
        }

        public SupportActionMenuRenderer()
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

                    Control.ClipsToBounds = true;
                    Control.Layer.CornerRadius = supportButton.CornerRadius;

                    supportButton.Clicked += SupportButton_Clicked;
                    tableView = new UITableView();
                    tableView.AutoresizingMask = UIViewAutoresizing.All;
                    tableView.Frame = Control.Frame;
                    tableView.SeparatorColor = UIColor.Clear;
                    var configStyle = new SupportAutoComplete();
                    configStyle.DropMode = SupportAutoCompleteDropMode.IconAndTitle;

                    dropSource = new DropItemSource(SupportItemList,configStyle, HeightOfRow, this);
                    tableView.Source = dropSource;
                    NotifyAdapterChanged();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportActionMenu.MenuItemsSource)))
            {
                NotifyAdapterChanged();
            }
        }

        private void TextFieldTapHandle()
        {
            IsShowDropList = !IsShowDropList;

            if (IsShowDropList)
            {
                if (SupportItemList.Count > 0)
                {
                    var rectOfControl = Control.ConvertRectToView(Control.Frame, Window);
                    nfloat height = Window.Bounds.Height - rectOfControl.Y - 10;
                    CGRect rectOfRoot = new CGRect(rectOfControl.X, rectOfControl.Y, rectOfControl.Width, height);

                    ShowSubviewAt(rectOfRoot, tableView, () =>
                    {
                        tableView.Layer.MasksToBounds = false;
                    });
                }
            }
            else
            {
                RemoveDropCover();
            }
        }

        private void RemoveDropCover()
        {
            if (converView != null)
                converView.RemoveFromSuperview();
            if (tableView != null)
                tableView.RemoveFromSuperview();
        }

        private void ShowSubviewAt(CGRect rectOfRoot, UIView subView, Action didFinishAnimation)
        {
            converView = new UIView();
            converView.Frame = new CGRect(0, 0, Window.Bounds.Width, Window.Bounds.Height);
            converView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.25f);
            converView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                RemoveDropCover();
                IsShowDropList = false;
            }));

            Window.AddSubview(converView);
            subView.Frame = new CGRect(rectOfRoot.X, rectOfRoot.Y, rectOfRoot.Width, 0);

            UIView.Animate(0.2, () =>
            {
                subView.Frame = new CGRect(rectOfRoot.X, rectOfRoot.Y, rectOfRoot.Width, HeightOfRow * SupportItemList.Count).ResyncViewPosition(Window, MinWidth, ExtendWidth);
                subView.SetShadow(2f, 2, 0.8f);
                Window.AddSubview(subView);
            }, didFinishAnimation);
        }

        public void IF_ItemSelectd(int position)
        {
            if (SupportItemList[position].IF_GetAction() != null)
            {
                SupportItemList[position].IF_GetAction()();
            }
            TextFieldTapHandle();
        }
    }
}