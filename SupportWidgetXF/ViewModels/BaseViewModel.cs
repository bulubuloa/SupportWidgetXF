using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SupportWidgetXF.ViewModels
{
    public abstract class BaseViewModel : BindableObject
    {
        public BaseViewModel()
        {
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

    }
}