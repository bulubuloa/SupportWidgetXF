using System;
using CoreGraphics;
using SupportWidgetXF.Models.Widgets;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers
{
    public static class UIViewExtensions
    {
        public static UIColor ColorSeperator = UIColor.FromRGB(232, 234, 238);

        public static void SetShadow(this UIView subView, nfloat Radius, nfloat size, float Opacity)
        {
            subView.Layer.ShadowRadius = Radius;
            subView.Layer.ShadowColor = ColorSeperator.CGColor;
            subView.Layer.ShadowOffset = new CGSize(size, size);
            subView.Layer.ShadowOpacity = Opacity;
            subView.Layer.ShadowPath = UIBezierPath.FromRect(subView.Layer.Bounds).CGPath;
            subView.Layer.MasksToBounds = true;
        }

        public static CGRect ResyncViewPosition(this CGRect cGRect, UIWindow window, int MinWidth, int ExtendWidth)
        {
            if (cGRect.Width < MinWidth)
                cGRect.Width = MinWidth;

            if (cGRect.X >= window.Bounds.Width - MinWidth)
                cGRect.X = window.Bounds.Width - MinWidth - ExtendWidth;

            if (cGRect.Y >= window.Bounds.Height - cGRect.Height)
                cGRect.Y = window.Bounds.Height - cGRect.Height - ExtendWidth;

            return cGRect;
        }

        public static void InitlizeReturnKey(this UITextField uITextField, SupportEntryReturnType returnType)
        {
            switch (returnType)
            {
                case SupportEntryReturnType.Go:
                    uITextField.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case SupportEntryReturnType.Next:
                    uITextField.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case SupportEntryReturnType.Send:
                    uITextField.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case SupportEntryReturnType.Search:
                    uITextField.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case SupportEntryReturnType.Done:
                    uITextField.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    uITextField.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }
    }
}