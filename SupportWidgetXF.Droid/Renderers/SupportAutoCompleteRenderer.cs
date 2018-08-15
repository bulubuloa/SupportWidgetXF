using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Droid.Renderers.DropCombo;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportAutoComplete), typeof(SupportAutoCompleteRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportAutoCompleteRenderer : SupportDropRenderer<SupportAutoComplete,AutoCompleteTextView>
    {
        private StateListDrawable stateListDrawable;

        public SupportAutoCompleteRenderer(Context context) : base(context)
        {
        }

        protected override void OnInitializeBorderView()
        {
            base.OnInitializeBorderView();

            stateListDrawable = new StateListDrawable();

            var selected = new GradientDrawable();
            selected.SetStroke((int)SupportView.CornerWidth, SupportView.FocusCornerColor.ToAndroid());
            selected.SetShape(ShapeType.Rectangle);
            selected.SetCornerRadius((float)SupportView.CornerRadius);

            var unSelected = new GradientDrawable();
            unSelected.SetStroke((int)SupportView.CornerWidth, SupportView.CornerColor.ToAndroid());
            unSelected.SetShape(ShapeType.Rectangle);
            unSelected.SetCornerRadius((float)SupportView.CornerRadius);

            var validSelected = new GradientDrawable();
            validSelected.SetStroke((int)SupportView.CornerWidth, SupportView.InvalidCornerColor.ToAndroid());
            validSelected.SetShape(ShapeType.Rectangle);
            validSelected.SetCornerRadius((float)SupportView.CornerRadius);

            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused }, selected);
            stateListDrawable.AddState(new int[] { SupportWidgetXF.Droid.Resource.Attribute.state_validate_pass }, unSelected);
            stateListDrawable.AddState(new int[] { }, unSelected);
        }

        protected override void OnInitializeOriginalView()
        {
            base.OnInitializeOriginalView();
            OriginalView = new AutoCompleteTextView(Context);
            OriginalView.SetSingleLine(true);
            if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
            {
                OriginalView.SetBackgroundDrawable(stateListDrawable);
            }
            else
            {
                OriginalView.SetBackground(stateListDrawable);
            }
            OriginalView.SetPadding((int)SupportView.PaddingInside, 0, (int)SupportView.PaddingInside, 0);
            OriginalView.TextSize = (float)SupportView.FontSize;
            OriginalView.SetTextColor(SupportView.TextColor.ToAndroid());
            OriginalView.TextAlignment = Android.Views.TextAlignment.Center;
            OriginalView.Typeface = SpecAndroid.CreateTypeface(Context, SupportView.FontFamily.Split('#')[0]);
            OriginalView.Hint = SupportView.Placeholder;
            OriginalView.InitlizeReturnKey(SupportView.ReturnType);
            OriginalView.EditorAction += (sender, ev) =>
            {
                SupportView.RunReturnAction();
            };
        }

        protected override void RefreshhAdapter()
        {
            base.RefreshhAdapter();
            dropItemAdapter = new DropItemAdapter(Context, SupportItemList, SupportView, this);
            OriginalView.Adapter = dropItemAdapter;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportAutoComplete.CurrentCornerColor)))
            {
                SetBorderColor();
            }
            else if (e.PropertyName.Equals(nameof(SupportViewBase.Text)))
            {
                if (OriginalView != null)
                {
                    OriginalView.Text = SupportView.Text;
                }
            }
        }

        private void SetBorderColor()
        {
            //gradientDrawable.SetStroke(1, supportAutoComplete.CurrentCornerColor.ToAndroid());
            //if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
            //{
            //    autoCompleteTextView.SetBackgroundDrawable(gradientDrawable);
            //}
            //else
            //{
            //    autoCompleteTextView.SetBackground(gradientDrawable);
            //}
        }

        public override void IF_ItemSelectd(int position)
        {
            base.IF_ItemSelectd(position);
            var text = SupportItemList[position].IF_GetTitle();
            SupportView.Text = text;
            OriginalView.Text = text;

            Task.Delay(50).ContinueWith(delegate
            {
                SupportWidgetXFSetup.Activity.RunOnUiThread(delegate
                {
                    OriginalView.SetSelection(text.Length);
                    OriginalView.DismissDropDown();
                });
            });

            if (SupportView.ItemSelecetedEvent != null)
                SupportView.ItemSelecetedEvent.Invoke(position);
        }
    }
}