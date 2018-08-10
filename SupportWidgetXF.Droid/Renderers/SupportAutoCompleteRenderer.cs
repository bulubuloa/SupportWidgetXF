using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportAutoComplete), typeof(SupportAutoCompleteRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportAutoCompleteRenderer : ViewRenderer<SupportAutoComplete, AutoCompleteTextView>
    {
        private SupportAutoComplete supportAutoComplete;
        private GradientDrawable gradientDrawable;
        private AutoCompleteTextView autoCompleteTextView;
        private Context _context;
        private ArrayAdapter arrayAdapter;

        private List<IAutoDropItem> SupportItemList = new List<IAutoDropItem>();
        private void NotifyAdapterChanged()
        {
            SupportItemList.Clear();
            if (supportAutoComplete.ItemsSourceOriginal != null)
            {
                SupportItemList.AddRange(supportAutoComplete.ItemsSourceOriginal.ToList());
            }
            arrayAdapter.NotifyDataSetChanged();
        }

        public SupportAutoCompleteRenderer(Context context) : base(context)
        {
            _context = context;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<SupportAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.NewElement is SupportAutoComplete)
            {
                supportAutoComplete = e.NewElement as SupportAutoComplete;
                gradientDrawable = new GradientDrawable();
                gradientDrawable.SetStroke(1, supportAutoComplete.CornerColor.ToAndroid());
                gradientDrawable.SetShape(ShapeType.Rectangle);
                gradientDrawable.SetCornerRadius(0f);

                autoCompleteTextView = new AutoCompleteTextView(Context);
                autoCompleteTextView.SetSingleLine(true);
                if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
                {
                    autoCompleteTextView.SetBackgroundDrawable(gradientDrawable);
                }
                else
                {
                    autoCompleteTextView.SetBackground(gradientDrawable);
                }
                autoCompleteTextView.SetPadding(10, 0, 10, 0);
                autoCompleteTextView.TextSize = (float)supportAutoComplete.FontSize;
                // _autoComplete.Typeface = SpecAndroid.CreateTypeface(Context, _dropDownView.FontFamily.Split('#')[0]);
                autoCompleteTextView.RequestFocusFromTouch();
                autoCompleteTextView.Hint = supportAutoComplete.Placeholder;
                autoCompleteTextView.InitlizeReturnKey(supportAutoComplete.ReturnType);
                autoCompleteTextView.ItemSelected += _autoComplete_ItemSelected;
                autoCompleteTextView.FocusChange += _autoComplete_FocusChange;
                autoCompleteTextView.ItemClick += _autoComplete_ItemClick;
                autoCompleteTextView.TextChanged += _autoComplete_TextChanged;
                autoCompleteTextView.BeforeTextChanged += _autoComplete_BeforeTextChanged;
                autoCompleteTextView.EditorAction += (sender, ev) =>
                {
                    supportAutoComplete.RunReturnAction();
                };
                if (supportAutoComplete.ItemsSource != null)
                {
                    RefreshhAdapter();
                }

                SetNativeControl(autoCompleteTextView);
            }
        }

        private CancellationTokenSource tokenSearch;
        private void SearchAndSync(string content)
        {
            if (tokenSearch != null)
                tokenSearch.Cancel();
            tokenSearch = new CancellationTokenSource();

            Task.Run(() =>
            {
                data = _dropDownView.ItemsSource.Where(x => x.ToLower().Contains(content.ToLower())).Take(30).ToList();
                MainActivity.Instance.RunOnUiThread(() =>
                {
                    arrayAdapter = new ArrayAdapter<string>(Context, Resource.Layout.listItem_Autocomplete, objects: _dropDownView.ItemsSource.ToArray());
                    _autoComplete.Adapter = arrayAdapter;
                });

            }, tokenSearch.Token);
        }

        void _autoComplete_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(e.Text.ToString()) && e.Text.ToString().Length > 1)
            //{
            //    SearchAndSync(e.Text.ToString());
            //}
        }


        void _autoComplete_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

        }


        void _autoComplete_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            supportAutoComplete.Text = autoCompleteTextView.Text;
            if (supportAutoComplete.ItemSelecetedEvent != null)
                supportAutoComplete.ItemSelecetedEvent.Invoke(e.Position);
        }


        void _autoComplete_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            supportAutoComplete.Text = autoCompleteTextView.Text;
            if (supportAutoComplete.ItemSelecetedEvent != null)
                supportAutoComplete.ItemSelecetedEvent.Invoke(e.Position);
        }


        void _autoComplete_FocusChange(object sender, FocusChangeEventArgs e)
        {
            supportAutoComplete.IsValid = true;
            if (e.HasFocus)
            {
                supportAutoComplete.CurrentCornerColor = supportAutoComplete.FocusCornerColor.ToAndroid() != supportAutoComplete.CornerColor.ToAndroid() ? supportAutoComplete.FocusCornerColor : supportAutoComplete.CornerColor;
                supportAutoComplete.SendAutocompleteFocused(e.HasFocus);
                SetBorderColor();
            }
            else
            {
                supportAutoComplete.CurrentCornerColor = supportAutoComplete.CornerColor;
                supportAutoComplete.SendAutocompleteFocused(e.HasFocus);
                supportAutoComplete.Text = autoCompleteTextView.Text;
                SetBorderColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportAutoComplete.CurrentCornerColor)))
            {
                SetBorderColor();
            }
            else if (e.PropertyName.Equals(nameof(SupportAutoComplete.Text)))
            {
                if (autoCompleteTextView != null)
                {
                    autoCompleteTextView.Text = supportAutoComplete.Text;
                }
            }
            else if (e.PropertyName.Equals(nameof(SupportAutoComplete.ItemsSource)))
            {
                RefreshhAdapter();
            }
        }

        private void RefreshhAdapter()
        {
            arrayAdapter = new ArrayAdapter<string>(Context, Resource.Layout.listItem_Autocomplete, objects: supportAutoComplete.ItemsSource.ToArray());
            autoCompleteTextView.Adapter = arrayAdapter;
        }

        private void SetBorderColor()
        {
            gradientDrawable.SetStroke(1, supportAutoComplete.CurrentCornerColor.ToAndroid());
            if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
            {
                autoCompleteTextView.SetBackgroundDrawable(gradientDrawable);
            }
            else
            {
                autoCompleteTextView.SetBackground(gradientDrawable);
            }
        }
    }
}
