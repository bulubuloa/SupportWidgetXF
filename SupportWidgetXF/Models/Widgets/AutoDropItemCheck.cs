using System;
namespace SupportWidgetXF.Models.Widgets
{
    public class AutoDropItemCheck : AutoDropItem
    {
        public int Index { set; get; }
        public bool Checked { set; get; }
        public Action action { set; get; }

        public override Action IF_GetAction()
        {
            return action;
        }

        public override bool IF_GetChecked()
        {
            return Checked;
        }

        public override void IF_SetChecked(bool _Checked)
        {
            Checked = _Checked;
        }

        public AutoDropItemCheck()
        {
            Checked = false;
        }
    }
}