using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Photos;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.iOS.Renderers.GalleryPicker;
using SupportWidgetXF.Models;
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
            MessagingCenter.Subscribe<GalleryPickerController, List<PhotoSetNative>>(this, "ReturnImage", (arg1, arg2) => {
               
                var itemResult = new List<ImageSet>();

                var options = new PHContentEditingInputRequestOptions();
                int Count = 0;

                foreach (var item in arg2)
                {
                    var xxx = new ImageSet();
                    xxx.Checked = item.Checked;

                    item.Image.RequestContentEditingInput(new PHContentEditingInputRequestOptions(), (contentEditingInput, requestStatusInfo) =>
                    {
                        if(contentEditingInput!=null)
                        {
                            xxx.Path = contentEditingInput.FullSizeImageUrl.ToString().Substring(7);
                            Console.WriteLine(xxx.Path);
                            itemResult.Add(xxx);
                        }

                        Count += 1;
                        //if (Count == arg2.Count)
                            //galleryPickerResultListener.IF_PickedResult(itemResult);
                    });
                }
            });
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener)
        {
            galleryPickerResultListener = pickerResultListener;
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