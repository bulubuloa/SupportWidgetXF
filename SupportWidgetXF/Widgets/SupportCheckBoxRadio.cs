using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportCheckBoxRadio : Button
    {
        public SupportCheckBoxRadio()
        {
            BackgroundColor = Color.Transparent;
            BorderWidth = 0;

            if (IsCheckboxType)
                Image = Checked ? ImageNameHelper.Icon_Checkbox_Checked : ImageNameHelper.Icon_Checkbox_UnChecked;
            else
                Image = Checked ? ImageNameHelper.Icon_Radio_Checked : ImageNameHelper.Icon_Radio_UnChecked;
        }

        static void RadioValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SupportCheckBoxRadio _cbxCustom && newValue != oldValue)
            {
                if (_cbxCustom.IsCheckboxType)
                {
                    _cbxCustom.Image = _cbxCustom.Checked ? ImageNameHelper.Icon_Checkbox_Checked : ImageNameHelper.Icon_Checkbox_UnChecked;
                }
                else
                {
                    _cbxCustom.Image = _cbxCustom.Checked ? ImageNameHelper.Icon_Radio_Checked : ImageNameHelper.Icon_Radio_UnChecked;
                }
            }
        }

        static void CheckboxValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SupportCheckBoxRadio _cbxCustom && newValue != oldValue)
            {
                _cbxCustom.Image = _cbxCustom.IsCheckboxType ? ImageNameHelper.Icon_Checkbox_UnChecked : ImageNameHelper.Icon_Checkbox_Checked;
            }
        }

        #region Bindable Properties
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked", typeof(bool), typeof(SupportCheckBoxRadio), false, propertyChanged: RadioValueChanged);

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public static readonly BindableProperty IsCheckboxTypeProperty =
            BindableProperty.Create("IsCheckboxType", typeof(bool), typeof(SupportCheckBoxRadio), false, propertyChanged: CheckboxValueChanged);

        public bool IsCheckboxType
        {
            get { return (bool)GetValue(IsCheckboxTypeProperty); }
            set { SetValue(IsCheckboxTypeProperty, value); }
        }
        #endregion
    }

    public class ImageNameHelper
    {
        public const string Icon_Checkbox_Checked = "checkbox_on";
        public const string Icon_Checkbox_UnChecked = "checkbox";
        public const string Icon_Radio_Checked = "radio_on";
        public const string Icon_Radio_UnChecked = "radio";
    }
}
