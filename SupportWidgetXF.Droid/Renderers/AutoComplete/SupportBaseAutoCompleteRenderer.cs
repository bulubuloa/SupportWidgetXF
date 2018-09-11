using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using SupportWidgetXF.Droid.Renderers.DropCombo;
using SupportWidgetXF.Widgets;
using Xamarin.Forms.Platform.Android;

namespace SupportWidgetXF.Droid.Renderers.AutoComplete
{
    public class SupportBaseAutoCompleteRenderer<TSupportAutoComplete, TOriginal> : SupportDropRenderer<TSupportAutoComplete, TOriginal>
        where TSupportAutoComplete : SupportAutoComplete
        where TOriginal : Android.Widget.AutoCompleteTextView
    {
        public SupportBaseAutoCompleteRenderer(Context context) : base(context)
        {
        }

        protected override void OnInitializeOriginalView()
        {
            base.OnInitializeOriginalView();

            OriginalView.SetSingleLine(true);
            if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
            {
                OriginalView.SetBackgroundDrawable(gradientDrawable);
            }
            else
            {
                OriginalView.SetBackground(gradientDrawable);
            }
            OriginalView.SetPadding((int)SupportView.PaddingInside, 0, (int)SupportView.PaddingInside, 0);
            OriginalView.TextSize = (float)SupportView.FontSize;
            OriginalView.SetTextColor(SupportView.TextColor.ToAndroid());
            OriginalView.TextAlignment = Android.Views.TextAlignment.Center;
            OriginalView.Typeface = SpecAndroid.CreateTypeface(Context, SupportView.FontFamily.Split('#')[0]);
            OriginalView.Hint = SupportView.Placeholder;
            OriginalView.RequestFocusFromTouch();
            OriginalView.FocusChange += OriginalView_FocusChange;
            OriginalView.InitlizeReturnKey(SupportView.ReturnType);
            OriginalView.EditorAction += (sender, ev) =>
            {
                SupportView.SendOnReturnKeyClicked();
            };
        }

        void OriginalView_FocusChange(object sender, FocusChangeEventArgs e)
        {
            SupportView.SendOnTextFocused(e.HasFocus);
        }

        protected override void OnInitializeAdapter()
        {
            base.OnInitializeAdapter();
            dropItemAdapter = new DropItemAdapter(Context, SupportItemList, SupportView, this);
            OriginalView.Adapter = dropItemAdapter;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportAutoComplete.CurrentCornerColor)))
            {
                gradientDrawable.SetStroke((int)SupportView.CornerWidth, SupportView.CurrentCornerColor.ToAndroid());
                if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
                {
                    OriginalView.SetBackgroundDrawable(gradientDrawable);
                }
                else
                {
                    OriginalView.SetBackground(gradientDrawable);
                }
            }
            else if (e.PropertyName.Equals(nameof(SupportViewBase.Text)))
            {
                if (OriginalView != null && !OriginalView.Text.Equals(SupportView.Text))
                {
                    OriginalView.SetText(SupportView.Text, false);
                }
            }
        }

        public override void IF_ItemSelectd(int position)
        {
            base.IF_ItemSelectd(position);

            var text = ((DropItemAdapter)dropItemAdapter).items[position].IF_GetTitle();
            OriginalView.SetText(text, false);
            SupportView.Text = text;

            Task.Delay(10).ContinueWith(delegate
            {
                SupportWidgetXFSetup.Activity.RunOnUiThread(delegate
                {
                    OriginalView.SetSelection(text.Length);
                    OriginalView.DismissDropDown();
                });
            });
        }
    }
}
