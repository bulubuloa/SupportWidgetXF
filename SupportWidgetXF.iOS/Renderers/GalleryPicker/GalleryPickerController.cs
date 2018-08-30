using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using Photos;
using SupportWidgetXF.Widgets.Interface;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class PhotoSetNative
    {
        public PHAsset Image { set; get; }
        public bool Checked { set; get; }

        public PhotoSetNative()
        {
            Checked = false;
        }
    }

    public class GalleryPickerController : UIViewController, IDropItemSelected, IGalleryPickerSelected
    {
        private UIView ViewTop, ViewBottom, FixView;
        private UIButton ButtonDone, ButttonBack, ButtonSpinner;
        private UICollectionView collectionView;

        private GalleryDirectorySource galleryDirectorySource;
        private List<PHAssetCollection> galleryDirectories = new List<PHAssetCollection>();
       

        private GalleryCollectionSource galleryCollectionSource;
        private List<PhotoSetNative> assets = new List<PhotoSetNative>();

        private UITableView tableView;
        private UIView DialogView, CoverView;
        protected bool FlagShow = false;


        private void InitializeLayout()
        {
           // FixView = new UIView(new CGRect(0,20,View.Bounds.Width,View.Bounds.Height-20));
            FixView = new UIView(new CGRect(0, 0, View.Bounds.Width, View.Bounds.Height));
            View.AddSubview(FixView);

            View.BackgroundColor = UIColor.White;

            ViewTop = new UIView(new CGRect(0,0, FixView.Bounds.Width,45));
            ViewTop.BackgroundColor = UIColor.FromRGB(64,64,64);

            ButttonBack = new UIButton(new CGRect(0, 8, 50, 30));
            ButttonBack.SetImage(UIImage.FromBundle("arrow_left").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal),UIControlState.Normal);
            ViewTop.AddSubview(ButttonBack);

            ButtonSpinner = new UIButton(new CGRect((FixView.Frame.Width - 150) / 2, 8, 150, 30));
            ButtonSpinner.BackgroundColor = UIColor.Black;
            ButtonSpinner.Font = UIFont.SystemFontOfSize(13);
            ButtonSpinner.SetTitle("Select album", UIControlState.Normal);
            ViewTop.AddSubview(ButtonSpinner);


            FixView.AddSubview(ViewTop);

            collectionView = new UICollectionView(new CGRect(0, 45, FixView.Bounds.Width, FixView.Bounds.Height - 45), new UICollectionViewFlowLayout());
            collectionView.BackgroundColor = UIColor.White;
            galleryCollectionSource = new GalleryCollectionSource(assets,this);

            var NumOfColumns = 3;
            var Spacing = 5;
            var SceenWidth = (View.Frame.Width-(Spacing-1)*NumOfColumns) / NumOfColumns;

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

            ViewBottom = new UIView(new CGRect(0, FixView.Bounds.Height-45, FixView.Bounds.Width, 45));
            ViewBottom.BackgroundColor = UIColor.FromRGB(64, 64, 64).ColorWithAlpha(0.7f);

            ButtonDone = new UIButton(new CGRect(ViewBottom.Frame.Width - 110,8, 100, 30));
            ButtonDone.Layer.BackgroundColor = UIColor.FromRGB(42, 131, 193).CGColor;
            ButtonDone.Layer.CornerRadius = 12;
            ViewBottom.AddSubview(ButtonDone);

            FixView.AddSubview(ViewBottom);

            ButttonBack.TouchUpInside += (object sender, EventArgs e) => 
            {
                DismissViewController(true, null);
            };

            ButtonDone.TouchUpInside += (object sender, EventArgs e) => 
            {

            };

            ButtonSpinner.TouchUpInside += (sender, e) => {
                ShowData();
            };



            //View.AddConstraint(NSLayoutConstraint.Create(ViewBottom, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0));
        }

        public GalleryPickerController()
        {
            var UIImage = new UIImageView();
            var image = new UIImage();

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

                var galleryDirectories1 = new List<PHAssetCollection>();

                var allAlbums = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.Album, PHAssetCollectionSubtype.Any, null).Cast<PHAssetCollection>();
                galleryDirectories1.AddRange(allAlbums);

                var smartAlbums = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.SmartAlbum, PHAssetCollectionSubtype.SmartAlbumUserLibrary, null).Cast<PHAssetCollection>();
                galleryDirectories1.AddRange(smartAlbums);
                var xxx = galleryDirectories1.OrderBy(obj => obj.LocalizedTitle);

                NSOperationQueue.MainQueue.AddOperation(() => {
                    foreach (var item in xxx)
                    {
                        var items = PHAsset.FetchAssets(item, new PHFetchOptions()).Cast<PHAsset>().ToList();
                        if (items.Count > 0)
                            galleryDirectories.Add(item);
                    }

                    tableView.ReloadData();    
                });
            });
        }

        public void IF_ItemSelectd(int position)
        {
            HideData();

            assets.Clear();
            var xx = galleryDirectories[position];
            ButtonSpinner.SetTitle(xx.LocalizedTitle,UIControlState.Normal);

            var sortOptions = new PHFetchOptions();
            sortOptions.SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", false) };

            var items = PHAsset.FetchAssets(xx, sortOptions).Cast<PHAsset>().ToList();
            assets.AddRange(items.Select(obj=> new PhotoSetNative(){
                Image = obj
            }));

            collectionView.ReloadData();

        }

        public void IF_ImageSelected(int positionDirectory, int positionImage)
        {
            var item = assets[positionImage];
            item.Checked = !item.Checked;
            collectionView.ReloadData();
        }
    }
}