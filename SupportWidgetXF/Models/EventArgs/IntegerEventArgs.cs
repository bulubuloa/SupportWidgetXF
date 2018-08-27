using System;
namespace SupportWidgetXF.Models
{
    public class IntegerEventArgs : EventArgs
    {
        public int IntegerValue { get; set; }

        public IntegerEventArgs(int _value)
        {
            IntegerValue = _value;
        }
    }
}