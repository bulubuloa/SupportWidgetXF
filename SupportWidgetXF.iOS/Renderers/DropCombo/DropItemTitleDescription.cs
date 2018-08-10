using System;

using Foundation;
using UIKit;

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
    }
}
