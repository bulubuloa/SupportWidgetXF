using System;
using System.ComponentModel;
using Foundation;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.DropCombo
{
    [Register("SupportRadioCheckiOS"), DesignTimeVisible(true)]
    public class SupportRadioCheckiOS : UIButton, INotifyPropertyChanged
    {
        public SupportRadioCheckiOS() : base() { }

        public SupportRadioCheckiOS(IntPtr handle) : base(handle) { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _Checked;
        [Export("Checked")]
        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                OnPropertyChanged("IsCheck");
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            LineBreakMode = UILineBreakMode.TailTruncation;
            ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            PropertyChanged += (sender, e) =>
            {
                if(e.PropertyName.Equals("IsCheck"))
                {
                    if (Checked)
                    {
                        Font = UIFont.BoldSystemFontOfSize(13f);
                        ImageEdgeInsets = new UIEdgeInsets(0, 15, 0, 0);
                        SetImage(UIImage.FromBundle("checked").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                    }
                    else
                    {
                        Font = UIFont.SystemFontOfSize(13f);
                        ImageEdgeInsets = new UIEdgeInsets(0, 15, 0, 0);
                        SetImage(UIImage.FromBundle("nonchecked").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                    }
                }
            };

            Checked = false;
        }
    }
}