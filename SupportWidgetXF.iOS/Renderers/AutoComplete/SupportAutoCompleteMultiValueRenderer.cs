using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.AutoComplete;
using SupportWidgetXF.iOS.Renderers.AutoComplete.Multi;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportAutoCompleteMultiValue), typeof(SupportAutoCompleteMultiValueRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportAutoCompleteMultiValueRenderer : SupportBaseAutoCompleteRenderer<SupportAutoCompleteMultiValue>
    {
        private UIStackView ResultView;
        private UICollectionView CollectionResult;
        private NSLayoutConstraint HeightOfCollection;
        private CollectionResultSource CollectionResultSource;
        private CollectionMultiDelegate CollectionMultiDelegate;
        private CollectionViewLeftFlowLayout CollectionViewLeftFlowLayout;
        private UITapGestureRecognizer uITapGestureRecognizer;
        private List<IAutoDropItem> ResultItems = new List<IAutoDropItem>();

        static readonly NSString CellId = new NSString("CollectionItemMultiCell");

        public SupportAutoCompleteMultiValueRenderer()
        {
        }

        public override void OnInitializeTextField()
        {
            base.OnInitializeTextField();
            ResultView = new UIStackView();
            ResultView.Axis = UILayoutConstraintAxis.Vertical;
            ResultView.Frame = this.Bounds;

            CollectionResult = new UICollectionView(new CGRect(),new CollectionViewLeftFlowLayout());
            CollectionResult.BackgroundColor = UIColor.White;

            HeightOfCollection = NSLayoutConstraint.Create(CollectionResult, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 0);
            CollectionResult.AddConstraint(HeightOfCollection);

            ResultView.AddArrangedSubview(textField);
            ResultView.AddArrangedSubview(CollectionResult);

            CollectionResult.RegisterClassForCell(typeof(CollectionItemMultiCell), CellId);
            CollectionResultSource = new CollectionResultSource(ResultItems);
            CollectionResult.Source = CollectionResultSource;

            CollectionMultiDelegate = new CollectionMultiDelegate(ResultItems, CollectionResult.Frame.Width);
            CollectionResult.Delegate = CollectionMultiDelegate;

            CollectionViewLeftFlowLayout = new CollectionViewLeftFlowLayout();
            CollectionViewLeftFlowLayout.MinimumLineSpacing = 5f;
            CollectionResult.CollectionViewLayout = CollectionViewLeftFlowLayout;

            uITapGestureRecognizer = new UITapGestureRecognizer(() =>
            {
                GestureAction(uITapGestureRecognizer);
            });
            uITapGestureRecognizer.NumberOfTapsRequired = 1;
            CollectionResult.UserInteractionEnabled = true;
            CollectionResult.AddGestureRecognizer(uITapGestureRecognizer);
            CollectionResult.ReloadData();
        }

        protected override void OnSetNativeControl()
        {
            SetNativeControl(ResultView);
        }

        public override void IF_ItemSelectd(int position)
        {
            //base.IF_ItemSelectd(position);
            AddViewToLayoutResult(SupportItemList[position]);
        }

        private void GestureAction(UITapGestureRecognizer tap)
        {
            try
            {
                var touchLocation = tap.LocationOfTouch(0, CollectionResult);
                var indexP = CollectionResult.IndexPathForItemAtPoint(touchLocation);
                var index = (int)indexP.Item;
                if (index >= 0 && index < ResultItems.Count)
                {
                    RemoveItem(index);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private bool CheckIsChoosed(IAutoDropItem input)
        {
            return false;
        }

        private void AddViewToLayoutResult(IAutoDropItem item)
        {
            if (CheckIsChoosed(item))
                return;

            try
            {
                AddItem(item);
                textField.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void RemoveItem(int index)
        {
            CollectionResult.PerformBatchUpdates(delegate {
                ResultItems.RemoveAt(index);
                CollectionResult.DeleteItems(new NSIndexPath[] { NSIndexPath.FromRowSection(index, 0) });
            }, null);
            HeightOfCollection.Constant = CollectionResult.CollectionViewLayout.CollectionViewContentSize.Height;
        }

        private void AddItem(IAutoDropItem item)
        {
            CollectionResult.PerformBatchUpdates(delegate {
                var now = ResultItems.Count;
                ResultItems.Add(item);
                CollectionResult.InsertItems(new NSIndexPath[] { NSIndexPath.FromRowSection(now, 0) });
            }, null);
            HeightOfCollection.Constant = CollectionResult.CollectionViewLayout.CollectionViewContentSize.Height;
        }
    }
}