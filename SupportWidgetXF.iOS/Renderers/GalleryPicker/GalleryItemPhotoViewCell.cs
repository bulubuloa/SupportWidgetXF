using System;

using Foundation;
using Photos;
using SupportWidgetXF.Widgets.Interface;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public partial class GalleryItemPhotoViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("GalleryItemPhotoViewCell");
        public static readonly UINib Nib;

        static GalleryItemPhotoViewCell()
        {
            Nib = UINib.FromName("GalleryItemPhotoViewCell", NSBundle.MainBundle);
        }

        protected GalleryItemPhotoViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public GalleryItemPhotoViewCell(){

        }

        private Action ActionClick;
        public void BindDataToCell(PhotoSetNative pHAsset, IGalleryPickerSelected action, int index, bool IsCamera)
        {
            imgIcon.ClipsToBounds = true;
            bttClick.Hidden = false;

            if (IsCamera)
            {
                imgIcon.Image = UIImage.FromBundle("camera").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                imgIcon.ContentMode = UIViewContentMode.ScaleAspectFit;
                CheckBox.Hidden = true;
                bttClick.Tag = index;

                if (ActionClick == null)
                {
                    ActionClick = delegate {
                        action.IF_CameraSelected((int)CheckBox.Tag);
                    };
                    bttClick.TouchUpInside += (sender, e) =>
                    {
                        ActionClick();
                    };
                }
            }
            else
            {
                imgIcon.ContentMode = UIViewContentMode.ScaleAspectFill;
                CheckBox.Hidden = false;
                CheckBox.Checked = pHAsset.Checked;
                CheckBox.Tag = index;

                if(pHAsset.Checked)
                {
                    bttClick.BackgroundColor = UIColor.DarkGray.ColorWithAlpha(0.5f);
                }
                else
                {
                    bttClick.BackgroundColor = UIColor.Clear;
                }

                var options = new PHImageRequestOptions
                {
                    Synchronous = true,
                    DeliveryMode = PHImageRequestOptionsDeliveryMode.FastFormat
                };

                PHImageManager.DefaultManager.RequestImageForAsset(pHAsset.Image, Bounds.Size, PHImageContentMode.AspectFit, options,(result, info) => {
                    imgIcon.Image = result;
                    try
                    {
                        if (info != null && info["PHImageFileURLKey"] != null)
                        {
                            Console.WriteLine(info["PHImageFileURLKey"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                });

                //pHAsset.Image.RequestContentEditingInput(new PHContentEditingInputRequestOptions(), (contentEditingInput, requestStatusInfo) =>
                //{
                //    if (contentEditingInput != null)
                //    {
                //        Console.WriteLine(contentEditingInput.FullSizeImageUrl.ToString());
                //    }
                //});

                if (ActionClick == null)
                {
                    ActionClick = delegate {
                        action.IF_ImageSelected(0, (int)CheckBox.Tag);
                    };
                    CheckBox.TouchUpInside += (sender, e) =>
                    {
                        ActionClick();
                    };
                }
            }
        }
    }
}