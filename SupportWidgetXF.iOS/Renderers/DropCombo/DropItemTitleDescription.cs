using System;

using Foundation;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    public partial class DropItemTitleDescription : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DropItemTitleDescription");
        public static readonly UINib Nib;

        static DropItemTitleDescription()
        {
            Nib = UINib.FromName("DropItemTitleDescription", NSBundle.MainBundle);
        }

        protected DropItemTitleDescription(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public DropItemTitleDescription() { }

        private Action ActionClick;

        public void BindDataToCell(IAutoDropItem dropItem, Action action, SupportViewDrop _ConfigStyle, bool _ShowCheckBox = false)
        {
            try
            {
                txtTitle.Text = dropItem.IF_GetTitle();
                txtDescription.Text = dropItem.IF_GetDescription();
                txtSeperator.BackgroundColor = _ConfigStyle.SeperatorColor.ToUIColor();
                NsHeightSeperator.Constant = _ConfigStyle.SeperatorHeight;
                txtTitle.TextColor = _ConfigStyle.TextColor.ToUIColor();
                txtDescription.TextColor = _ConfigStyle.DescriptionTextColor.ToUIColor();

                if (_ShowCheckBox)
                {
                    NSSpaceBetWeen.Constant = 5;
                    NSSizeOfCheckbox.Constant = 25;
                    cbxCheckBox.Checked = dropItem.IF_GetChecked();
                    cbxCheckBox.Hidden = false;
                }
                else
                {
                    NSSpaceBetWeen.Constant = 0;
                    NSSizeOfCheckbox.Constant = 0;
                    cbxCheckBox.Hidden = true;
                }

                if (ActionClick == null)
                {
                    ActionClick = action;
                    bttClick.TouchUpInside += (sender, e) =>
                    {
                        ActionClick();
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
