using System;
namespace SupportWidgetXF.Models.Widgets
{
    public enum SupportEntryReturnType
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }

    public enum SupportAutoCompleteDropMode
    {
        SingleTitle, TitleWithDescription, IconAndTitle, FullTextAndIcon
    }
}