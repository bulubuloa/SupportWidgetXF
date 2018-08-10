// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    [Register ("DropItemSingleTitle")]
    partial class DropItemSingleTitle
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bttClick { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtSeperator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ViewMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bttClick != null) {
                bttClick.Dispose ();
                bttClick = null;
            }

            if (txtSeperator != null) {
                txtSeperator.Dispose ();
                txtSeperator = null;
            }

            if (txtTitle != null) {
                txtTitle.Dispose ();
                txtTitle = null;
            }

            if (ViewMain != null) {
                ViewMain.Dispose ();
                ViewMain = null;
            }
        }
    }
}