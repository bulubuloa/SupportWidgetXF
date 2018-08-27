using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views.InputMethods;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportEditor), typeof(SupportEditorRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportEditorRenderer : EditorRenderer
    {
        private SupportEditor supportEditor;

        public SupportEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportEditor)
                {
                    supportEditor = Element as SupportEditor;

                    GradientDrawable gd = new GradientDrawable();
                    gd.SetCornerRadius((float)supportEditor.CornerRadius);
                    gd.SetStroke((int)supportEditor.CornerWidth, supportEditor.CornerColor.ToAndroid());
                    Control.SetBackground(gd);
                    InitlizeReturnKey();
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
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case SupportEntryReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case SupportEntryReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case SupportEntryReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }
    }
}