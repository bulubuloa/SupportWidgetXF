// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SupportWidgetXF.iOS
{
    [Register ("XFCameraController")]
    partial class XFCameraController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bttBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bttCapture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bttFlash { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bttSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView cameraView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ViewTop { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bttBack != null) {
                bttBack.Dispose ();
                bttBack = null;
            }

            if (bttCapture != null) {
                bttCapture.Dispose ();
                bttCapture = null;
            }

            if (bttFlash != null) {
                bttFlash.Dispose ();
                bttFlash = null;
            }

            if (bttSwitch != null) {
                bttSwitch.Dispose ();
                bttSwitch = null;
            }

            if (cameraView != null) {
                cameraView.Dispose ();
                cameraView = null;
            }

            if (ViewTop != null) {
                ViewTop.Dispose ();
                ViewTop = null;
            }
        }
    }
}