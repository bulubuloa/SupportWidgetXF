using System;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using Photos;
using SupportWidgetXF.Widgets.Interface;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class GalleryDirectorySource : UITableViewSource
    {
        private List<GalleryNative> galleryDirectories;
        private IDropItemSelected IDropItemSelected;

        public GalleryDirectorySource(List<GalleryNative> galleryDirectories,IDropItemSelected IDropItemSelected)
        {
            this.galleryDirectories = galleryDirectories;
            this.IDropItemSelected = IDropItemSelected;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cellChild = tableView.DequeueReusableCell("GalleryDirectoryViewCell") as GalleryDirectoryViewCell;
            cellChild = new GalleryDirectoryViewCell();
            var viewChild = NSBundle.MainBundle.LoadNib("GalleryDirectoryViewCell", cellChild, null);
            cellChild = Runtime.GetNSObject(viewChild.ValueAt(0)) as GalleryDirectoryViewCell;
            cellChild.Tag = indexPath.Row;
            cellChild.BindDataToCell(galleryDirectories[indexPath.Row], delegate {
                IDropItemSelected.IF_ItemSelectd(indexPath.Row);
            });
            return cellChild;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return galleryDirectories.Count;
        }
    }
}