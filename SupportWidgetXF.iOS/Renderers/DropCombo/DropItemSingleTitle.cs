using System;

using Foundation;
using SupportWidgetXF.Models.Widgets;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    public partial class DropItemSingleTitle : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DropItemSingleTitle");
        public static readonly UINib Nib;

        static DropItemSingleTitle()
        {
            Nib = UINib.FromName("DropItemSingleTitle", NSBundle.MainBundle);
        }

        protected DropItemSingleTitle(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public DropItemSingleTitle() { }

        private Action ActionClick;

        public void BindDataToCell(IAutoDropItem dropItem,  Action action)
        {
            try
            {
                txtTitle.Text = dropItem.IF_GetTitle();

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