// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    [Register ("GalleryCameraViewCell")]
    partial class GalleryCameraViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bttClick { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageview { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ViewMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bttClick != null) {
                bttClick.Dispose ();
                bttClick = null;
            }

            if (imageview != null) {
                imageview.Dispose ();
                imageview = null;
            }

            if (ViewMain != null) {
                ViewMain.Dispose ();
                ViewMain = null;
            }
        }
    }
}