using System;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using SupportWidgetXF.Models.Widgets;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    public class DropItemSource : UITableViewSource
    {
        private List<IAutoDropItem> ItemsList;
        private int HeightofRow = 35;

        public DropItemSource(List<IAutoDropItem> _ItemsList)
        {
            ItemsList = _ItemsList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cellChild = tableView.DequeueReusableCell("DropItemSingleTitleID") as DropItemSingleTitle;
            cellChild = new DropItemSingleTitle();
            var viewChild = NSBundle.MainBundle.LoadNib("CustomDropItemViewCell", cellChild, null);
            cellChild = Runtime.GetNSObject(viewChild.ValueAt(0)) as DropItemSingleTitle;

            var item = ItemsList[indexPath.Row];

            cellChild.BindDataToCell(item,delegate {
            

            });
            return cellChild;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemsList.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return HeightofRow;
        }
    }
}