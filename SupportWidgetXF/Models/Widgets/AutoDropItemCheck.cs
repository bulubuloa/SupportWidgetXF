using System;
namespace SupportWidgetXF.Models.Widgets
{
    public class AutoDropItemCheck : AutoDropItem
    {
        public int Index { set; get; }
        public bool Checked { set; get; }

        public AutoDropItemCheck()
        {
        }
    }
}