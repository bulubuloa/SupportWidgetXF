using System;
using System.Collections.Generic;
using Foundation;
using SupportWidgetXF.Models.Widgets;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.AutoComplete.Multi
{
    public class CollectionResultSource : UICollectionViewSource
    {
        private List<IAutoDropItem> items;
        static readonly NSString cellId = new NSString("CollectionItemMultiCell");

        public CollectionResultSource(List<IAutoDropItem> words)
        {
            this.items = words;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var textCell = (CollectionItemMultiCell)collectionView.DequeueReusableCell(cellId, indexPath);
            textCell.Text = items[indexPath.Row].IF_GetTitle();
            return textCell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return items.Count;
        }
    }
}