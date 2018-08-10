using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.DropCombo;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportAutoCompleteAsync), typeof(SupportAutoCompleteAsyncRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportAutoCompleteAsyncRenderer : ViewRenderer<SupportAutoCompleteAsync, UIView>
    {
        private SupportAutoCompleteAsync supportAutoComplete;
        private UITableView tableView;
        private UITextField textField;
        private int HeightOfRow = 40;
        private bool IsShowDropList = false;
        private DropItemSource dropSource;

        private List<IAutoDropItem> SupportItemList = new List<IAutoDropItem>();
        private void NotifyAdapterChanged()
        {
            SupportItemList.Clear();
            if (supportAutoComplete.ItemsSource != null)
            {
                SupportItemList.AddRange(supportAutoComplete.ItemsSource.ToList());
            }
            tableView.ReloadData();
        }

        public SupportAutoCompleteAsyncRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SupportAutoCompleteAsync> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.NewElement is SupportAutoCompleteAsync)
            {
                supportAutoComplete = e.NewElement as SupportAutoCompleteAsync;
                if (Control == null)
                {
                    textField = new UITextField();
                    textField.Layer.CornerRadius = (float)supportAutoComplete.CornerRadius;
                    textField.Layer.BorderWidth = (float)supportAutoComplete.CornerWidth;
                    textField.Layer.BorderColor = supportAutoComplete.CornerColor.ToCGColor();
                    //textField.AttributedPlaceholder = new NSAttributedString(supportAutoComplete.Placeholder, font: UIFont.FromName(supportAutoComplete.FontFamily, size: (float)supportAutoComplete.FontSize));
                    //textField.Font = UIFont.FromName(supportAutoComplete.FontFamily, (float)supportAutoComplete.FontSize);
                    textField.Frame = this.Frame;
                    textField.Placeholder = supportAutoComplete.Placeholder;
                    textField.LeftView = new UIView(new CGRect(0, 0, supportAutoComplete.PaddingInside, 0));
                    textField.LeftViewMode = UITextFieldViewMode.Always;
                    textField.Text = supportAutoComplete.Text;
                    textField.EditingChanged += Wrapper_EditingChanged; ;
                    textField.ShouldEndEditing += Wrapper_ShouldEndEditing;
                    textField.ShouldBeginEditing += Wrapper_ShouldBeginEditing;
                    textField.ShouldReturn += (textField) =>
                    {
                        supportAutoComplete.RunReturnAction();
                        return true;
                    };
                    textField.InitlizeReturnKey(supportAutoComplete.ReturnType);

                    tableView = new UITableView();
                    tableView.AutoresizingMask = UIViewAutoresizing.All;
                    tableView.Frame = textField.Frame;
                    tableView.SeparatorColor = UIColor.Clear;
                    //tableView.Layer.CornerRadius = (float)supportAutoComplete.CornerRadius;
                    //tableView.Layer.BorderWidth = (float)supportAutoComplete.CornerWidth;
                    //tableView.Layer.BorderColor = supportAutoComplete.CornerColor.ToCGColor();

                    //dropSource = new DropItemSource(SupportItemList, HeightOfRow, this, UIFont.FromName(supportAutoComplete.FontFamily, (nfloat)supportAutoComplete.FontSize));
                    dropSource = new DropItemSource(SupportItemList, supportAutoComplete, HeightOfRow);
                    tableView.Source = dropSource;

                    NotifyAdapterChanged();

                    supportAutoComplete.SetItemSelection += (obj) =>
                    {
                        textField.Text = SupportItemList[obj].IF_GetTitle();
                        if (supportAutoComplete.ItemSelecetedEvent != null)
                            supportAutoComplete.ItemSelecetedEvent.Invoke(obj);
                    };

                    SetNativeControl(textField);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(SupportAutoComplete.CurrentCornerColorProperty.PropertyName))
            {
                textField.Layer.BorderColor = supportAutoComplete.CurrentCornerColor.ToCGColor();
            }
            else if (e.PropertyName.Equals(SupportViewBase.TextProperty.PropertyName))
            {
                if (textField != null)
                {
                    textField.Text = supportAutoComplete.Text;
                }
            }
            else if (e.PropertyName.Equals(SupportAutoComplete.ItemsSourceProperty.PropertyName))
            {
                NotifyAdapterChanged();
                IsShowDropList = SupportItemList.Count == 0;
                ShowData();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && tableView != null)
                HideData();
            base.Dispose(disposing);
        }

        void Wrapper_EditingChanged(object sender, EventArgs e)
        {
            var textFieldInput = sender as UITextField;
            if (!string.IsNullOrEmpty(textFieldInput.Text) && textFieldInput.Text.Length > 1)
            {
                supportAutoComplete.SendTextChangeFinished(textFieldInput.Text);
            }
            else
            {
                supportAutoComplete.SendTextChangeFinished(null);
                //HideData();
            }
        }

        bool Wrapper_ShouldBeginEditing(UITextField textFieldInput)
        {
            supportAutoComplete.IsValid = true;
            supportAutoComplete.CurrentCornerColor = supportAutoComplete.FocusCornerColor != Color.Default ? supportAutoComplete.FocusCornerColor : supportAutoComplete.CornerColor;
            textFieldInput.Layer.BorderColor = supportAutoComplete.CurrentCornerColor.ToCGColor();
            supportAutoComplete.SendAutocompleteFocused(true);
            return true;
        }

        bool Wrapper_ShouldEndEditing(UITextField textFieldInput)
        {
            HideData();
            ResetCornerColor();
            supportAutoComplete.SendAutocompleteFocused(false);
            return true;
        }

        //private CancellationTokenSource tokenSearch;
        //private void SearchAndSync(string content)
        //{
        //    if (tokenSearch != null)
        //        tokenSearch.Cancel();
        //    tokenSearch = new CancellationTokenSource();

        //    Task.Run(() =>
        //    {
        //        SupportItemList.Clear();
        //        var key = content.ToLower();
        //        var result = supportAutoComplete.ItemsSource.Where(x => x.IF_GetTitle().ToLower().Contains(key) || x.IF_GetDescription().ToLower().Contains(key) ).Take(30).ToList();
        //        SupportItemList.AddRange(result);

        //        IsShowDropList = false;

        //        InvokeOnMainThread(delegate
        //        {
        //            tableView.ReloadData();
        //            supportAutoComplete.Text = textField.Text;
        //            ShowData();
        //        });

        //    }, tokenSearch.Token);
        //}

        private void ShowData()
        {
            if (textField == null)
                return;

            IsShowDropList = !IsShowDropList;
            if (IsShowDropList)
            {
                var rect = textField.ConvertRectToView(textField.Frame, Window);
                nfloat height = Window.Bounds.Height - rect.Y - 10;
                CGRect r = new CGRect(rect.X, rect.Y, rect.Width, height);

                ShowSubviewAt(r, tableView, () =>
                {
                    tableView.Layer.MasksToBounds = !supportAutoComplete.HasShadow;
                });
            }
            else
            {
                HideData();
            }
        }

        private void HideData()
        {
            tableView.RemoveFromSuperview();
        }

        private void ShowSubviewAt(CGRect rect, UIView subView, Action didFinishAnimation)
        {
            float height = HeightOfRow * SupportItemList.Count();
            var y = rect.Y + textField.Frame.Height + 2;
            if (height > rect.Height / 2)
                height = (float)rect.Height / 2;

            subView.Frame = new CGRect(rect.X, y, rect.Width, 0);
            UIView.Animate(0.2, () =>
            {
                subView.Frame = new CGRect(rect.X, y, rect.Width, height);
                subView.SetShadow(2f, 2, 0.8f);
                Window.AddSubview(subView);
            }, didFinishAnimation);
        }

        private void ResetCornerColor()
        {
            supportAutoComplete.CurrentCornerColor = supportAutoComplete.CornerColor;
            supportAutoComplete.IsValid = true;
            textField.Layer.BorderColor = supportAutoComplete.CurrentCornerColor.ToCGColor();
        }
    }
}