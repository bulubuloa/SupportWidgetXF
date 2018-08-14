using System;

using Foundation;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    public partial class DropItemFullTextIcon : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DropItemFullTextIcon");
        public static readonly UINib Nib;

        static DropItemFullTextIcon()
        {
            Nib = UINib.FromName("DropItemFullTextIcon", NSBundle.MainBundle);
        }

        protected DropItemFullTextIcon(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public DropItemFullTextIcon() { }

        private Action ActionClick;

        public void BindDataToCell(IAutoDropItem dropItem, Action action, SupportViewDrop _ConfigStyle)
        {
            try
            {
                txtTitle.Text = dropItem.IF_GetTitle();
                txtDescription.Text = dropItem.IF_GetDescription();
                txtSeperator.BackgroundColor = _ConfigStyle.SeperatorColor.ToUIColor();
                NsHeightSeperator.Constant = _ConfigStyle.SeperatorHeight;
                imgIcon.Image = UIImage.FromBundle(dropItem.IF_GetIcon()).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                txtTitle.TextColor = _ConfigStyle.TextColor.ToUIColor();
                txtDescription.TextColor = _ConfigStyle.DescriptionTextColor.ToUIColor();

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
