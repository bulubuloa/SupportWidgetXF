using System;
using Android.Views.InputMethods;
using Android.Widget;
using SupportWidgetXF.Models.Widgets;

namespace SupportWidgetXF.Droid.Renderers
{
    public static class ViewExtensions
    {
        public static void InitlizeReturnKey(this EditText editText, SupportEntryReturnType returnType)
        {
            switch (returnType)
            {
                case SupportEntryReturnType.Go:
                    editText.ImeOptions = ImeAction.Go;
                    editText.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case SupportEntryReturnType.Next:
                    editText.ImeOptions = ImeAction.Next;
                    editText.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case SupportEntryReturnType.Send:
                    editText.ImeOptions = ImeAction.Send;
                    editText.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case SupportEntryReturnType.Search:
                    editText.ImeOptions = ImeAction.Search;
                    editText.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    editText.ImeOptions = ImeAction.Done;
                    editText.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }
    }
}