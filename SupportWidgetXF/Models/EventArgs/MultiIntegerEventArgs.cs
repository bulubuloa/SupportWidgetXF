using System;
using System.Collections.Generic;

namespace SupportWidgetXF.Models
{
    public class MultiIntegerEventArgs : EventArgs
    {
        public IEnumerable<int> ListIntegerValue { set; get; }

        public MultiIntegerEventArgs(IEnumerable<int> _ListIntegerValue)
        {
            ListIntegerValue = _ListIntegerValue;
        }
    }
}