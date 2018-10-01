using System;
namespace SupportWidgetXF.Models.Widgets
{
    public interface IAutoDropItem
    {
        string IF_GetTitle();
        string IF_GetDescription();
        string IF_GetIcon();
        Action IF_GetAction();
        bool IF_GetChecked();
        void IF_SetChecked(bool _Checked);
    }
}