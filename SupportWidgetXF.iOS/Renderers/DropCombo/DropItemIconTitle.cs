using System;

using Foundation;
using UIKit;

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
    }
}
