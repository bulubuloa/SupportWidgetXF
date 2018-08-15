using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportAutoComplete), typeof(SupportAutoCompleteRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportAutoCompleteRenderer : SupportDropRenderer<SupportAutoComplete>
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
            if (!string.IsNullOrEmpty(textFieldInput.Text) && textFieldInput.Text.Length > 1)
            {
                SupportView.SendOnTextChanged(textFieldInput.Text);
            }
            else
            {
                SupportView.SendOnTextChanged(null);
            }
        }

        bool Wrapper_ShouldBeginEditing(UITextField textFieldInput)
        {
            SupportView.IsValid = true;
            SupportView.CurrentCornerColor = SupportView.FocusCornerColor != Color.Default ? SupportView.FocusCornerColor : SupportView.CornerColor;
            textFieldInput.Layer.BorderColor = SupportView.CurrentCornerColor.ToCGColor();
            SupportView.SendOnTextFocused(true);
            return true;
        }

        bool Wrapper_ShouldEndEditing(UITextField textFieldInput)
        {
            HideData();
            ResetCornerColor();
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
            else if (e.PropertyName.Equals(SupportViewDrop.ItemsSourceDisplayProperty.PropertyName))
            {
                NotifyAdapterChanged();
                FlagShow = SupportItemList.Count ==  0;
                ShowData();
            }
        }

        private void ResetCornerColor()
        {
            SupportView.CurrentCornerColor = SupportView.CornerColor;
            SupportView.IsValid = true;
            textField.Layer.BorderColor = SupportView.CurrentCornerColor.ToCGColor();
        }
    }
}