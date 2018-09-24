using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportFlowLayout : Layout<View>
    {
        public static BindableProperty SpacingProperty = BindableProperty.Create("Spacing", typeof(Thickness), typeof(SupportFlowLayout), new Thickness(6));
        public Thickness Spacing
        {
            get => (Thickness)GetValue(SpacingProperty);
            set
            {
                SetValue(SpacingProperty, value);
                InvalidateLayout();
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var layoutInfo = new LayoutInfo(Spacing);
            layoutInfo.ProcessLayout(Children, width);

            for (int i = 0; i < layoutInfo.Bounds.Count; i++)
            {
                if (!Children[i].IsVisible)
                {
                    continue;
                }

                var bounds = layoutInfo.Bounds[i];
                bounds.Left += x;
                bounds.Top += y;

                LayoutChildIntoBoundingRegion(Children[i], bounds);
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var layoutInfo = new LayoutInfo(Spacing);
            layoutInfo.ProcessLayout(Children, widthConstraint);
            return new SizeRequest(new Size(widthConstraint, layoutInfo.HeightRequest));
        }


        public class LayoutInfo
        {
            double _x = 0;
            double _y = 0;
            double _rowHeight = 0;
            Thickness _spacing;

            public LayoutInfo(Thickness spacing)
            {
                _spacing = spacing;
            }

            public List<Rectangle> Bounds { get; private set; }

            public double HeightRequest { get; private set; }

            public void ProcessLayout(IList<View> views, double widthConstraint)
            {
                Bounds = new List<Rectangle>();
                var sizes = SizeViews(views, widthConstraint);
                LayoutViews(views, sizes, widthConstraint);
            }

            private List<Rectangle> SizeViews(IList<View> views, double widthConstraint)
            {
                var sizes = new List<Rectangle>();

                foreach (var view in views)
                {
                    var sizeRequest = view.Measure(widthConstraint, double.PositiveInfinity).Request;
                    var viewWidth = sizeRequest.Width;
                    var viewHeight = sizeRequest.Height;

                    if (viewWidth > widthConstraint)
                    {
                        viewWidth = widthConstraint;
                    }

                    sizes.Add(new Rectangle(0, 0, viewWidth, viewHeight));
                }

                return sizes;
            }

            private void LayoutViews(IList<View> views, List<Rectangle> sizes, double widthConstraint)
            {
                Bounds = new List<Rectangle>();
                _x = 0d;
                _y = 0d;
                HeightRequest = 0;

                for (int i = 0; i < views.Count(); i++)
                {
                    if (!views[i].IsVisible)
                    {
                        Bounds.Add(new Rectangle(0, 0, 0, 0));
                        continue;
                    }

                    var sizeRect = sizes[i];

                    CheckNewLine(sizeRect.Width, widthConstraint);
                    UpdateRowHeight(sizeRect.Height);

                    var bound = new Rectangle(_x, _y, sizeRect.Width, sizeRect.Height);
                    Bounds.Add(bound);

                    _x += bound.Width;
                    _x += _spacing.HorizontalThickness;
                }

                HeightRequest += _rowHeight;
            }

            private void CheckNewLine(double viewWidth, double widthConstraint)
            {
                if (_x + viewWidth > widthConstraint)
                {
                    _y += _rowHeight + _spacing.VerticalThickness;
                    HeightRequest = _y;
                    _x = 0;
                    _rowHeight = 0;
                }
            }

            private void UpdateRowHeight(double viewHeight)
            {
                if (viewHeight > _rowHeight)
                {
                    _rowHeight = viewHeight;
                }
            }
        }
    }
}
