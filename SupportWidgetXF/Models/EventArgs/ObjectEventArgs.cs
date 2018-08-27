using System;
namespace SupportWidgetXF.Models
{
    public class ObjectEventArgs : EventArgs
    {
        public object Item { set; get; }

        public ObjectEventArgs(object _Item)
        {
            Item = _Item;
        }
    }
}