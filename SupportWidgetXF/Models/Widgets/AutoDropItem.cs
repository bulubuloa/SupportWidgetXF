using System;
namespace SupportWidgetXF.Models.Widgets
{
    public class AutoDropItem : IAutoDropItem
    {
        public string Description { set; get; }
        public string Title { set; get; }
        public string Icon { set; get; }

        public AutoDropItem()
        {
        }

        public string IF_GetDescription()
        {
            return Description;
        }

        public string IF_GetIcon()
        {
            return Icon;
        }

        public string IF_GetTitle()
        {
            return Title;
        }

        public virtual Action IF_GetAction()
        {
            return null;
        }

        public virtual bool IF_GetChecked()
        {
            throw new NotImplementedException();
        }

        public virtual void IF_SetChecked(bool _Checked)
        {
            throw new NotImplementedException();
        }
    }
}