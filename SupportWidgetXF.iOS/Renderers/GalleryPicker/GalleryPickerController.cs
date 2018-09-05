using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using MBProgressHUD;
using Photos;
using SupportWidgetXF.Widgets.Interface;
using UIKit;
using Xamarin.Forms;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class GalleryNative
    {
        public PHAssetCollection Collection { set; get; }
        public List<PhotoSetNative> Images { set; get; }

        public GalleryNative()
        {
            Images = new List<PhotoSetNative>();
        }
    }

    public class PhotoSetNative
    {
        public PHAsset Image { set; get; }
        public bool Checked { set; get; }
        public string Path { set; get; }
        public bool FromCloud { set; get; }
        public ImageSource SourceXF { set; get; }

        public PhotoSetNative()
        {
            Checked = false;
            FromCloud = true;
        }
    }

    public class GalleryPickerController : UIViewController, IDropItemSelected, IGalleryPickerSelected
    {
        private UIView ViewTop, ViewBottom, FixView;
        private UIButton ButtonDone, ButttonBack, ButtonSpinner;
        private UICollectionView collectionView;

        private GalleryDirectorySource galleryDirectorySource;
        private List<GalleryNative> galleryDirectories = new List<GalleryNative>();
       
        private GalleryCollectionSource galleryCollectionSource;
        private List<PhotoSetNative> assets = new List<PhotoSetNative>();

        private UITableView tableView;
        private UIView DialogView, CoverView;
        protected bool FlagShow = false;
        private int CurrentParent = -1;

        MTMBProgressHUD mTMBProgressHUD;

        private void InitializeLayout()
        {
            var color = UIColor.FromRGB(64, 64, 64);
            View.BackgroundColor = color;
            FixView = new UIView(new CGRect(0,20,View.Bounds.Width,View.Bounds.Height-20));
            View.AddSubview(FixView);

            ViewTop = new UIView(new CGRect(0,0, FixView.Bounds.Width,45));
            ViewTop.BackgroundColor = UIColor.Clear;
            ViewTop.Layer.MasksToBounds = false;
            ViewTop.Layer.ShadowOpacity = 1f;
            ViewTop.Layer.ShadowOffset = new CGSize(0, 2);
            ViewTop.Layer.ShadowColor = UIColor.Gray.CGColor;
            ViewTop.Layer.CornerRadius = 0;

            ButttonBack = new UIButton(new CGRect(0, 8, 50, 30));
            ButttonBack.SetImage(UIImage.FromBundle("arrow_left").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal),UIControlState.Normal);
            ViewTop.AddSubview(ButttonBack);

            ButtonSpinner = new UIButton(new CGRect((FixView.Frame.Width - 150) / 2, 8, 150, 30));
            ButtonSpinner.BackgroundColor = UIColor.Clear;
            ButtonSpinner.Font = UIFont.SystemFontOfSize(13);
            ButtonSpinner.SetTitle("Select album", UIControlState.Normal);
            ButtonSpinner.Layer.CornerRadius = 3;
            ButtonSpinner.Layer.BorderColor = UIColor.White.CGColor;
            ButtonSpinner.Layer.BorderWidth = 1f;

            var arrow = new UIImageView();
            arrow.ContentMode = UIViewContentMode.ScaleAspectFit;
            arrow.Image = UIImage.FromBundle("sort_down_white").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            arrow.Frame = new CGRect(ButtonSpinner.Frame.X+ButtonSpinner.Frame.Width-15,ButtonSpinner.Frame.Y+8,  12, 12);

            ViewTop.AddSubview(ButtonSpinner);
            ViewTop.AddSubview(arrow);

            collectionView = new UICollectionView(new CGRect(0, 45, FixView.Bounds.Width, FixView.Bounds.Height - 45), new UICollectionViewFlowLayout());
            collectionView.BackgroundColor = UIColor.White;
            galleryCollectionSource = new GalleryCollectionSource(assets,this);

            var NumOfColumns = 3;
            var Spacing = 2;
            var SceenWidth = (View.Frame.Width-(NumOfColumns - 1)*Spacing) / NumOfColumns;

            var layout = new UICollectionViewFlowLayout 
            { 
                MinimumInteritemSpacing = Spacing, 
                MinimumLineSpacing = Spacing, 
                ScrollDirection = UICollectionViewScrollDirection.Vertical, 
                ItemSize = new CoreGraphics.CGSize(SceenWidth, SceenWidth), 
                FooterReferenceSize = new CoreGraphics.CGSize(View.Frame.Width, 150) 
            }; 
            collectionView.RegisterNibForCell(UINib.FromName("GalleryItemPhotoViewCell", NSBundle.MainBundle), "GalleryItemPhotoViewCell"); 
            //collectionView.RegisterClassForSupplementaryView(typeof(FooterLoading), UICollectionElementKindSection.Footer, new NSString("Header")); 
            //collectionView.Delegate = new CVDelegate(controller, this);
            collectionView.DataSource = galleryCollectionSource; 
            collectionView.SetCollectionViewLayout(layout, true);

            FixView.AddSubview(collectionView);
            FixView.AddSubview(ViewTop);

            ViewBottom = new UIView(new CGRect(0, FixView.Bounds.Height-45, FixView.Bounds.Width, 45));
            ViewBottom.BackgroundColor = color.ColorWithAlpha(0.7f);

            ButtonDone = new UIButton(new CGRect(ViewBottom.Frame.Width - 110,8, 100, 30));
            ButtonDone.Layer.BackgroundColor = UIColor.FromRGB(42, 131, 193).CGColor;
            ButtonDone.Layer.CornerRadius = 12;
            ButtonDone.SetTitle("Done", UIControlState.Normal);

            ViewBottom.AddSubview(ButtonDone);

            FixView.AddSubview(ViewBottom);

            ButttonBack.TouchUpInside += (object sender, EventArgs e) => 
            {
                DismissViewController(true, null);
            };

            ButtonDone.TouchUpInside += (object sender, EventArgs e) => 
            {
                MessagingCenter.Send<GalleryPickerController, List<PhotoSetNative>>(this, "ReturnImage", GetCurrentSelected());
                DismissModalViewController(true);
            };

            ButtonSpinner.TouchUpInside += (sender, e) => {
                ShowData();
            };

            mTMBProgressHUD = new MTMBProgressHUD(this.View);
            View.AddSubview(mTMBProgressHUD);
        }

        public GalleryPickerController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeLayout();
            InitShowDialog();
            FeetchAddPhotos();
        }

        private void InitShowDialog()
        {
            CoverView = new UIView(FixView.Bounds);
            CoverView.BackgroundColor = UIColor.FromRGB(64, 64, 64).ColorWithAlpha(0.95f);
            CoverView.AddGestureRecognizer(new UITapGestureRecognizer(() => { ShowData(); }));

            DialogView = new UIView(new CGRect(10, (FixView.Bounds.Height-400)/2, FixView.Bounds.Width - 20, 400));
            DialogView.Layer.CornerRadius = 9;
            DialogView.Layer.BackgroundColor = UIColor.White.CGColor;

            tableView = new UITableView();
            tableView.RowHeight = UITableView.AutomaticDimension;
            tableView.EstimatedRowHeight = 50f;
            tableView.AutoresizingMask = UIViewAutoresizing.All;
            tableView.Frame = new CGRect(10,10, DialogView.Frame.Width-20, DialogView.Frame.Height-20);
            tableView.SeparatorColor = UIColor.Clear;
            tableView.BackgroundColor = UIColor.Clear;

            galleryDirectorySource = new GalleryDirectorySource(galleryDirectories, this);
            tableView.Source = galleryDirectorySource;

            DialogView.AddSubview(tableView);
            CoverView.AddSubview(DialogView);
        }

        public virtual void ShowData()
        {
            FlagShow = !FlagShow;
            if (FlagShow)
            {
                UIView.Animate(0.2, () =>
                {
                    FixView.AddSubview(CoverView);
                }, delegate {

                });
            }
            else
            {
                HideData();
            }
        }

       
        public virtual void HideData()
        {
            if (CoverView != null)
                CoverView.RemoveFromSuperview();
        }

        private void FeetchAddPhotos()
        {
            PHPhotoLibrary.RequestAuthorization(status => {
                if (status != PHAuthorizationStatus.Authorized)
                    return;
                
                var galleryTemp = new List<PHAssetCollection>();

                var allAlbums = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.Album, PHAssetCollectionSubtype.Any, null).Cast<PHAssetCollection>();
                var smartAlbums = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.SmartAlbum, PHAssetCollectionSubtype.SmartAlbumUserLibrary, null).Cast<PHAssetCollection>();

                galleryTemp.AddRange(allAlbums);
                galleryTemp.AddRange(smartAlbums);

                var gallerySort = galleryTemp.OrderBy(obj => obj.LocalizedTitle);

                NSOperationQueue.MainQueue.AddOperation(() => {
                    foreach (var itemRaw in gallerySort)
                    {
                        var sortOptions = new PHFetchOptions();
                        sortOptions.SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", false) };

                        var items = PHAsset.FetchAssets(itemRaw, sortOptions).Cast<PHAsset>().ToList();

                        if (items.Count > 0)
                        {
                            var colec = new GalleryNative()
                            {
                                Collection = itemRaw,
                                Images = items.Select(obj => new PhotoSetNative()
                                {
                                    Image = obj,
                                    Path = obj.LocalIdentifier
                                }).ToList()
                            };
                            colec.Images.Insert(0, new PhotoSetNative());
                            galleryDirectories.Add(colec);
                        }
                    }

                    tableView.ReloadData();

                    if(galleryDirectories.Count>0)
                    {
                        CurrentParent = 0;
                        IF_ItemSelectd(CurrentParent);
                    }
                });
            });
        }

        public void IF_ItemSelectd(int position)
        {
            CurrentParent = position;

            HideData();

            assets.Clear();
            var xx = galleryDirectories[position];

            ButtonSpinner.SetTitle(xx.Collection.LocalizedTitle,UIControlState.Normal);

            assets.AddRange(xx.Images);

            collectionView.ReloadData();
        }

        public void IF_ImageSelected(int positionDirectory, int positionImage, ImageSource imageSource)
        {
            var item = galleryDirectories[CurrentParent].Images[positionImage];
            item.Checked = !item.Checked;
            collectionView.ReloadData();

            if(imageSource!=null)
            {
                item.SourceXF = imageSource;
            }

            var count = GetCurrentSelected().Count;
            if (count > 0)
            {
                ButtonDone.SetTitle("Done (" + count + ")", UIControlState.Normal);
            }
            else
            {
                ButtonDone.SetTitle("Done", UIControlState.Normal);
            }
        }

        public void IF_ImageSelected(int positionDirectory, int positionImage)
        {
            var item = galleryDirectories[CurrentParent].Images[positionImage];
            item.Checked = !item.Checked;
            collectionView.ReloadData();

            if(item.Checked)
            {
                var options = new PHContentEditingInputRequestOptions()
                {
                    NetworkAccessAllowed = true,
                };
                options.ProgressHandler = HandlePHProgressHandler;

                item.Image.RequestContentEditingInput(options, (contentEditingInput, requestStatusInfo) =>
                {
                    if (contentEditingInput != null)
                    {
                        item.Path = contentEditingInput.FullSizeImageUrl.ToString().Substring(7);
                        Console.WriteLine(item.Path);
                    }
                    else
                    {
                        Console.WriteLine("N/A");
                    }
                });
            }
            else
            {
                item.Path = null;
            }

            var count = GetCurrentSelected().Count;
            if (count > 0)
            {
                ButtonDone.SetTitle("Done (" + count + ")",UIControlState.Normal);
            }
            else
            {
                ButtonDone.SetTitle("Done", UIControlState.Normal);
            }
        }

        void HandlePHProgressHandler(double progress, ref bool stop)
        {
            Console.WriteLine("Downloaded : "+progress);
        }


        public void IF_CameraSelected(int pos)
        {
            //open camera
            DismissModalViewController(true);

            UIImagePickerController uIImagePickerController = new UIImagePickerController();
            uIImagePickerController.SourceType = UIImagePickerControllerSourceType.Camera;
            uIImagePickerController.FinishedPickingImage += (sender, e) => {

            };
            NaviExtensions.OpenController(uIImagePickerController);   
        }

        private List<PhotoSetNative> GetCurrentSelected()
        {
            var result = galleryDirectories.SelectMany(directory => directory.Images).Where(Image => Image.Checked).ToList();
            return result;
        }
    }
}