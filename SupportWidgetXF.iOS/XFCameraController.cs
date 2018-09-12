using AVFoundation;
using Foundation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

namespace SupportWidgetXF.iOS
{
    public partial class XFCameraController : UIViewController
    {
        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;
        AVCaptureVideoPreviewLayer videoPreviewLayer;
        bool FlashOn = false;

        public XFCameraController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            await AuthorizeCameraUse();
            SetupLiveCameraStream();
        }

        private async Task AuthorizeCameraUse()
        {
            try
            {
                var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

                if (authorizationStatus != AVAuthorizationStatus.Authorized)
                {
                    await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public void SetupLiveCameraStream()
        {
            captureSession = new AVCaptureSession();

            var viewLayer = cameraView.Layer;
            videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = this.View.Frame
            };
            cameraView.Layer.AddSublayer(videoPreviewLayer);

            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            captureSession.AddInput(captureDeviceInput);

            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };

            captureSession.AddOutput(stillImageOutput);
            captureSession.StartRunning();

            var color = UIColor.FromRGB(64, 64, 64);

            ViewTop.BackgroundColor = color.ColorWithAlpha(0.7f);
            bttBack.SetImage(UIImage.FromBundle("arrow_left").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
            bttFlash.SetImage(UIImage.FromBundle("flash_off").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
            bttSwitch.SetImage(UIImage.FromBundle("switch_camera").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
            bttCapture.SetImage(UIImage.FromBundle("capture_camera").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);

            bttFlash.TouchUpInside += BttFlash_TouchUpInside;
            bttCapture.TouchUpInside += BttCapture_TouchUpInside;
            bttBack.TouchUpInside += BttBack_TouchUpInside;
            bttSwitch.TouchUpInside += BttSwitch_TouchUpInside;
        }

        void BttSwitch_TouchUpInside(object sender, EventArgs e)
        {
            var devicePosition = captureDeviceInput.Device.Position;
            if (devicePosition == AVCaptureDevicePosition.Front)
            {
                devicePosition = AVCaptureDevicePosition.Back;
            }
            else
            {
                devicePosition = AVCaptureDevicePosition.Front;
            }

            var device = GetCameraForOrientation(devicePosition);
            ConfigureCameraForDevice(device);

            captureSession.BeginConfiguration();
            captureSession.RemoveInput(captureDeviceInput);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);
            captureSession.AddInput(captureDeviceInput);
            captureSession.CommitConfiguration();
        }

        public AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
        {
            var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
            foreach (var device in devices)
            {
                if (device.Position == orientation)
                {
                    return device;
                }
            }
            return null;
        }

        void BttBack_TouchUpInside(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        void BttFlash_TouchUpInside(object sender, EventArgs e)
        {
            var device = captureDeviceInput.Device;

            var error = new NSError();
            if (device.HasFlash)
            {
                if (device.FlashMode == AVCaptureFlashMode.On)
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.Off;
                    device.UnlockForConfiguration();

                    FlashOn = false;
                    bttFlash.SetImage(UIImage.FromBundle("flash_off").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                }
                else
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.On;
                    device.UnlockForConfiguration();

                    FlashOn = true;
                    bttFlash.SetImage(UIImage.FromBundle("flash_on").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                }
            }
        }

        async void BttCapture_TouchUpInside(object sender, EventArgs e)
        {
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);

            var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);
            var jpegAsByteArray = jpegImageAsNsData.ToArray();

        }

        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }
    }
}