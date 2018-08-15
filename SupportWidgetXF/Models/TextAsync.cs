using System;
namespace SupportWidgetXF.Models
{
    public class TextAsync
    {
        public string Text { set; get; }
        public bool IsFilter { set; get; }

        public TextAsync(string _Text, bool _Filter)
        {
            Text = _Text;
            IsFilter = _Filter;
        }
    }
}