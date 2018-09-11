using System;
using System.Threading.Tasks;
using SupportWidgetXF.ViewModels;

namespace SupportWidgetXF.Navigation
{
    public interface INavigationServiceXF
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(bool animate) where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter, bool animate) where TViewModel : BaseViewModel;
        Task NavigateToAsync(Type viewModelType, bool animate);
        Task NavigateToAsync(Type viewModelType, object parameter, bool animate);
        Task NavigateBackAsync(bool animate);
        Task RemoveLastFromBackStackAsync();
        Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : BaseViewModel;
        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : BaseViewModel;
    }
}