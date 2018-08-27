using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using SupportWidgetXF.Models.Widgets;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.AutoComplete.Multi
{
    public class CollectionMultiDelegate : UICollectionViewDelegateFlowLayout
    {
        List<IAutoDropItem> items;
        UIStringAttributes attr;
        nfloat maxWidth;

        public CollectionMultiDelegate(List<IAutoDropItem> items, nfloat maxW)
        {
            this.maxWidth = maxW;
            this.items = items;
            this.attr = new UIStringAttributes
            {
                Font = new UILabel().Font
            };
        }

        //[Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public override CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
        {
            CGSize size = new NSString(items[indexPath.Row].IF_GetTitle()).GetSizeUsingAttributes
            (
                new UIStringAttributes() 
                { 
                    Font = UIFont.SystemFontOfSize(13) 
                }
            );
            size.Width += 20;
            size.Height = 35;
            collectionView.SystemLayoutSizeFittingSize(size, 1.0f, 1.0f);
            return size;
        }
    }
}
