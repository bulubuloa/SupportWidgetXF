using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.AutoComplete.Multi
{
    public class CollectionViewLeftFlowLayout : UICollectionViewFlowLayout
    {
        nfloat maxCellSpacing = 5;

        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CoreGraphics.CGRect rect)
        {
            var arr = base.LayoutAttributesForElementsInRect(rect);
            if (arr.Length > 0)
            {
                UICollectionViewLayoutAttributes currentOne = arr[0];
                CGRect frame = currentOne.Frame;
                frame.X = 0;
                currentOne.Frame = frame;
            }


            for (int i = 1; i < arr.Length; ++i)
            {
                UICollectionViewLayoutAttributes currentLayoutAttributes = arr[i];
                UICollectionViewLayoutAttributes prevLayoutAttributes = arr[i - 1];
                nfloat origin = prevLayoutAttributes.Frame.GetMaxX();

                if (origin + maxCellSpacing + currentLayoutAttributes.Frame.Size.Width < CollectionView.ContentSize.Width)
                {
                    CGRect frame = currentLayoutAttributes.Frame;
                    frame.X = origin + maxCellSpacing;
                    currentLayoutAttributes.Frame = frame;
                }
                else
                {
                    CGRect frame = currentLayoutAttributes.Frame;
                    frame.X = 0;
                    currentLayoutAttributes.Frame = frame;

                }
            }
            return arr;
        }

        public override UICollectionViewLayoutAttributes LayoutAttributesForItem(Foundation.NSIndexPath indexPath)
        {
            var currentItemAttributes = base.LayoutAttributesForItem(indexPath);


            var collectionViewFlowLayout = CollectionView.CollectionViewLayout as UICollectionViewFlowLayout;

            if (collectionViewFlowLayout != null)
            {
                var sectionInset = collectionViewFlowLayout.SectionInset;
                if (indexPath.Item == 0)
                { // first item of section
                    var frame = currentItemAttributes.Frame;
                    frame.X = sectionInset.Left; // first item of the section should always be left aligned
                    currentItemAttributes.Frame = frame;
                    return currentItemAttributes;
                }

                var previousIndexPath = NSIndexPath.FromItemSection(indexPath.Item - 1, indexPath.Section);
                var previousFrame = base.LayoutAttributesForItem(previousIndexPath).Frame;

                previousFrame.X = base.LayoutAttributesForItem(previousIndexPath).Frame.Left;
                if (previousFrame.X != base.LayoutAttributesForItem(previousIndexPath).Frame.Left)
                {
                    var n = base.LayoutAttributesForItem(previousIndexPath).Frame.Left;
                    previousFrame.X = n;
                    maxCellSpacing = 0;
                }
                var previousFrameRightPoint = (previousFrame.X) + (previousFrame.Size.Width) + maxCellSpacing;

                var currentFrame = currentItemAttributes.Frame;
                var width = 0.0;

                var collectionViewWidth = CollectionView == null ? 0 : CollectionView.Frame.Size.Width;
                width = collectionViewWidth;
                var strecthedCurrentFrame = new CGRect(0, currentFrame.Y, width, currentFrame.Size.Height);

                if (CGRect.Intersect(previousFrame, strecthedCurrentFrame) == CGRect.Empty)
                { // if current item is the first item on the line
                  // the approach here is to take the current frame, left align it to the edge of the view
                  // then stretch it the width of the collection view, if it intersects with the previous frame then that means it
                  // is on the same line, otherwise it is on it's own new line
                    var frame = currentItemAttributes.Frame;
                    frame.X = sectionInset.Left; // first item on the line should always be left aligned
                    currentItemAttributes.Frame = frame;
                    return currentItemAttributes;
                }

                var frame2 = currentItemAttributes.Frame;
                frame2.X = previousFrameRightPoint;
                currentItemAttributes.Frame = frame2;
            }
            return currentItemAttributes;
        }
    }
}
