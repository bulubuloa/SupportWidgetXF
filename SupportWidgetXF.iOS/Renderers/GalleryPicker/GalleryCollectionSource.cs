using System;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using Photos;
using SupportWidgetXF.Widgets.Interface;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class GalleryCollectionSource : UICollectionViewDataSource
    {
        private List<PhotoSetNative> assets = new List<PhotoSetNative>();
        private IGalleryPickerSelected IGalleryPickerSelected;

        public GalleryCollectionSource(List<PhotoSetNative> assets, IGalleryPickerSelected IGalleryPickerSelected)
        {
            this.assets = assets;
            this.IGalleryPickerSelected = IGalleryPickerSelected;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var data = assets[indexPath.Row];
            var cell = (GalleryItemPhotoViewCell)collectionView.DequeueReusableCell("GalleryItemPhotoViewCell", indexPath);
            if (cell == null)
            {
                cell = new GalleryItemPhotoViewCell();
                var views = NSBundle.MainBundle.LoadNib("GalleryItemPhotoViewCell", cell, null);
                cell = Runtime.GetNSObject(views.ValueAt(0)) as GalleryItemPhotoViewCell;
            }
            cell.BindDataToCell(data,delegate {
                IGalleryPickerSelected.IF_ImageSelected(indexPath.Row, indexPath.Row);
            });
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return assets.Count;
        }
    }
}
