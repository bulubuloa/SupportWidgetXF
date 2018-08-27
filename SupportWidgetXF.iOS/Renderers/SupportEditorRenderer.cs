using System;
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

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Element is SupportEditor)
                {
                    supportEditor = Element as SupportEditor;
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