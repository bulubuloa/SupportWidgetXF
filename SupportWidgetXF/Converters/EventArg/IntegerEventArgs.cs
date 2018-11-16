using System;
namespace SupportWidgetXF.Converters.EventArg
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
