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
    [Register ("GalleryItemPhotoViewCell")]
    partial class GalleryItemPhotoViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SupportWidgetXF.iOS.Renderers.DropCombo.SupportCheckBoxiOS CheckBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ViewMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CheckBox != null) {
                CheckBox.Dispose ();
                CheckBox = null;
            }

            if (imgIcon != null) {
                imgIcon.Dispose ();
                imgIcon = null;
            }

            if (ViewMain != null) {
                ViewMain.Dispose ();
                ViewMain = null;
            }
        }
    }
}