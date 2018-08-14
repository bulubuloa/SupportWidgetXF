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

    public enum SupportEntryVerticalTextAligment
    {
        Top, Center, Bottom
    }

    public enum SupportEntryDrawableInsideAligment
    {
        Left, Right
    }

    public enum SupportEntryCorner
    {
        None, UnderLine, Border
    }
}