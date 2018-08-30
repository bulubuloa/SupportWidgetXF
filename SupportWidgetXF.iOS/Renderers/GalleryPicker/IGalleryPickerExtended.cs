using System;
using System.Linq;
using System.Threading;
using Foundation;
using ObjCRuntime;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.iOS.Renderers.GalleryPicker;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IGalleryPickerExtended))]
namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class IGalleryPickerExtended : IGalleryPicker
    {
        IGalleryPickerResultListener galleryPickerResultListener;

        public IGalleryPickerExtended()
        {
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener)
        {
            galleryPickerResultListener = pickerResultListener;
            //var myVC = NSBundle.MainBundle.LoadNib("GalleryPickerController", null, null);
            //var xxx = Runtime.GetNSObject(myVC.ValueAt(0)) as GalleryPickerController;

            OpenController(new GalleryPickerController());
        }

        private void OpenController(UIViewController openController)
        {
            UIViewController viewController = null;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            if (window == null)
                throw new InvalidOperationException("There's no current active window");

            if (window.WindowLevel == UIWindowLevel.Normal)
                viewController = window.RootViewController;

            if (viewController == null)
            {
                window = UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel).FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);
                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller");
                else
                    viewController = window.RootViewController;
            }

            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            //MediaPickerDelegate ndelegate = new MediaPickerDelegate(viewController, sourceType, options);
            //var od = Interlocked.CompareExchange(ref pickerDelegate, ndelegate, null);
            //if (od != null)
                //throw new InvalidOperationException("Only one operation can be active at at time");


            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)// && sourceType == UIImagePickerControllerSourceType.PhotoLibrary)
            {
                //ndelegate.Popover = popover = new UIPopoverController(picker);
                //ndelegate.Popover.Delegate = new MediaPickerPopoverDelegate(ndelegate, picker);
                //ndelegate.DisplayPopover();
            }
            else
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
                {
                    //picker.ModalPresentationStyle = options?.ModalPresentationStyle == MediaPickerModalPresentationStyle.OverFullScreen  ? UIModalPresentationStyle.OverFullScreen : UIModalPresentationStyle.FullScreen;
                }
                openController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
                openController.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                openController.ModalPresentationCapturesStatusBarAppearance = true;
                viewController.PresentModalViewController(openController, true);
            }
        }
    }
}