using System;

using Foundation;
using UIKit;

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
    }
}
