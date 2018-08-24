using System;

using Foundation;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    public partial class DropItemIconTitle : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DropItemIconTitle");
        public static readonly UINib Nib;

        static DropItemIconTitle()
        {
            Nib = UINib.FromName("DropItemIconTitle", NSBundle.MainBundle);
        }

        protected DropItemIconTitle(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public DropItemIconTitle() { }

        private Action ActionClick;

        public void BindDataToCell(IAutoDropItem dropItem, Action action, SupportViewDrop _ConfigStyle, bool _ShowCheckBox = false)
        {
            try
            {
                txtTitle.Text = dropItem.IF_GetTitle();
                txtSeperator.BackgroundColor = _ConfigStyle.SeperatorColor.ToUIColor();
                NsHeightSeperator.Constant = _ConfigStyle.SeperatorHeight;
                imgIcon.Image = UIImage.FromBundle(dropItem.IF_GetIcon()).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                txtTitle.TextColor = _ConfigStyle.TextColor.ToUIColor();

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