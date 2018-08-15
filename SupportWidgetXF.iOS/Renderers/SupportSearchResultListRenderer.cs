using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportSearchResultList), typeof(SupportSearchResultListRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportSearchResultListRenderer : SupportDropRenderer<SupportSearchResultList>
    {
        public override void OnInitializeTextField()
        {
            base.OnInitializeTextField();
            textField.AttributedPlaceholder = new NSAttributedString(SupportView.Placeholder, font: UIFont.FromName(SupportView.FontFamily, size: (float)SupportView.FontSize));
            textField.Placeholder = SupportView.Placeholder;
            textField.LeftView = new UIView(new CGRect(0, 0, SupportView.PaddingInside, 0));
            textField.LeftViewMode = UITextFieldViewMode.Always;

            textField.EditingChanged += Wrapper_EditingChanged; ;
            textField.ShouldEndEditing += Wrapper_ShouldEndEditing;
            textField.ShouldBeginEditing += Wrapper_ShouldBeginEditing;
            textField.ShouldReturn += (textField) =>
            {
                SupportView.SendOnReturnKeyClicked();
                return true;
            };
            textField.InitlizeReturnKey(SupportView.ReturnType);
        }

        void Wrapper_EditingChanged(object sender, EventArgs e)
        {
            var textFieldInput = sender as UITextField;
            SupportView.SendOnTextChanged(textFieldInput.Text);
        }

        bool Wrapper_ShouldBeginEditing(UITextField textFieldInput)
        {
            SupportView.SendOnTextFocused(true);
            return true;
        }

        bool Wrapper_ShouldEndEditing(UITextField textFieldInput)
        {
            HideData();
            SupportView.SendOnTextFocused(false);
            return true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(SupportAutoComplete.CurrentCornerColorProperty.PropertyName))
            {
                textField.Layer.BorderColor = SupportView.CurrentCornerColor.ToCGColor();
            }
            else if(e.PropertyName.Equals(SupportViewDrop.ItemsSourceProperty.PropertyName))
            {
                OnInitializeTableSource();
                tableView.ReloadData();
                FlagShow = false;
                ShowData();
            }
        }
    }
}