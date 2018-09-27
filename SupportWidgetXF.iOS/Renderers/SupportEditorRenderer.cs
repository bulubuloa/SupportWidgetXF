using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportEditor), typeof(SupportEditorRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportEditorRenderer : EditorRenderer
    {
        private SupportEditor supportEditor;

        private UILabel PlaceHolderLabel;
        private double PreviousHeight = -1;
        private int PrevLines = 0;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (PlaceHolderLabel == null)
                {
                    CreatePlaceholder();
                }
            }

            if (e.NewElement != null)
            {
                if (Element is SupportEditor)
                {
                    supportEditor = Element as SupportEditor;

                    if (supportEditor.IsExpandable)
                        Control.ScrollEnabled = false;
                    else
                        Control.ScrollEnabled = true;
                    if (supportEditor.HasRoundedCorner)
                        Control.Layer.CornerRadius = 5;
                    else
                        Control.Layer.CornerRadius = 0;
                    Control.InputAccessoryView = new UIView(CGRect.Empty);
                    Control.ReloadInputViews();

                    Control.Layer.CornerRadius = (float)supportEditor.CornerRadius;
                    Control.Layer.BorderWidth = (float)supportEditor.CornerWidth;
                    Control.Layer.BorderColor = supportEditor.CornerColor.ToCGColor();
                    InitlizeReturnKey();

                    Control.ShouldChangeText += (textView, range, text) => {
                        //Control.ResignFirstResponder();
                        return true;
                    };
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Editor.TextProperty.PropertyName)
            {
                if (supportEditor.IsExpandable)
                {
                    CGSize size = Control.Text.StringSize(Control.Font, Control.Frame.Size, UILineBreakMode.WordWrap);
                    int numLines = (int)(size.Height / Control.Font.LineHeight);
                    if (PrevLines > numLines)
                    {
                        supportEditor.HeightRequest = -1;
                    }
                    else if (string.IsNullOrEmpty(Control.Text))
                    {
                        supportEditor.HeightRequest = -1;
                    }
                    PrevLines = numLines;
                }
                PlaceHolderLabel.Hidden = !string.IsNullOrEmpty(Control.Text);
            }
            else if (SupportEditor.PlaceholderProperty.PropertyName == e.PropertyName)
            {
                PlaceHolderLabel.Text = supportEditor.Placeholder;
            }
            else if (SupportEditor.PlaceholderColorProperty.PropertyName == e.PropertyName)
            {
                PlaceHolderLabel.TextColor = supportEditor.PlaceholderColor.ToUIColor();
            }
            else if (SupportEditor.HasRoundedCornerProperty.PropertyName == e.PropertyName)
            {
                if (supportEditor.HasRoundedCorner)
                    Control.Layer.CornerRadius = 5;
                else
                    Control.Layer.CornerRadius = 0;
            }
            else if (SupportEditor.IsExpandableProperty.PropertyName == e.PropertyName)
            {
                if (supportEditor.IsExpandable)
                    Control.ScrollEnabled = false;
                else
                    Control.ScrollEnabled = true;

            }
            else if (SupportEditor.HeightProperty.PropertyName == e.PropertyName)
            {
                if (supportEditor.IsExpandable)
                {
                    CGSize size = Control.Text.StringSize(Control.Font, Control.Frame.Size, UILineBreakMode.WordWrap);
                    int numLines = (int)(size.Height / Control.Font.LineHeight);
                    if (numLines >= 5)
                    {
                        Control.ScrollEnabled = true;
                        supportEditor.HeightRequest = PreviousHeight;
                    }
                    else
                    {
                        Control.ScrollEnabled = false;
                        PreviousHeight = supportEditor.Height;
                    }
                }
            }
        }

        public void CreatePlaceholder()
        {
            var element = Element as SupportEditor;
            PlaceHolderLabel = new UILabel
            {
                Text = element?.Placeholder,
                TextColor = element.PlaceholderColor.ToUIColor(),
                BackgroundColor = UIColor.Clear
            };

            var edgeInsets = Control.TextContainerInset;
            var lineFragmentPadding = Control.TextContainer.LineFragmentPadding;

            Control.AddSubview(PlaceHolderLabel);

            var vConstraints = NSLayoutConstraint.FromVisualFormat(
                "V:|-" + edgeInsets.Top + "-[PlaceholderLabel]-" + edgeInsets.Bottom + "-|", 0, new NSDictionary(),
                NSDictionary.FromObjectsAndKeys(
                    new NSObject[] { PlaceHolderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
            );

            var hConstraints = NSLayoutConstraint.FromVisualFormat(
                "H:|-" + lineFragmentPadding + "-[PlaceholderLabel]-" + lineFragmentPadding + "-|",
                0, new NSDictionary(),
                NSDictionary.FromObjectsAndKeys(
                    new NSObject[] { PlaceHolderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
            );
            PlaceHolderLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            Control.AddConstraints(hConstraints);
            Control.AddConstraints(vConstraints);
        }

        private void SwitchReturnAction()
        {
            var type = supportEditor.ReturnType;
            switch (type)
            {
                case SupportEntryReturnType.Go:
                    supportEditor.InvokeCompleted();
                    break;
                case SupportEntryReturnType.Next:
                    supportEditor.OnNext();
                    break;
                case SupportEntryReturnType.Send:
                    supportEditor.InvokeCompleted();
                    break;
                case SupportEntryReturnType.Search:
                    supportEditor.InvokeCompleted();
                    break;
                case SupportEntryReturnType.Done:
                    supportEditor.InvokeCompleted();
                    break;
                default:
                    supportEditor.InvokeCompleted();
                    break;
            }
        }

        private void InitlizeReturnKey()
        {
            var type = supportEditor.ReturnType;

            switch (type)
            {
                case SupportEntryReturnType.Go:
                    Control.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case SupportEntryReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case SupportEntryReturnType.Send:
                    Control.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case SupportEntryReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case SupportEntryReturnType.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }
    }
}