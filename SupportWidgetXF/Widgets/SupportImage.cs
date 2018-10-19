using System;
using System.Runtime.CompilerServices;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportImage : Image
    {
        public event EventHandler ImageLoaded;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(IsLoadingProperty.PropertyName))
            {
                if (IsLoading && Source!=null)
                    ImageLoaded?.Invoke(this, null);
            }
        }
    }
}