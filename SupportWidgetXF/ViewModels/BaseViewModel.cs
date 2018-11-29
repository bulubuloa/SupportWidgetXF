using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace SupportWidgetXF.ViewModels
{
    public interface IHandleViewAppearing
    {
        Task OnViewAppearingAsync(VisualElement view);
    }

    public interface IHandleViewDisappearing
    {
        Task OnViewDisappearingAsync(VisualElement view);
    }

    public abstract class BaseViewModel : BindableObject, IHandleViewDisappearing, IHandleViewAppearing
    {
        /*
         * Validations
         */
        private string _WarningMessage;
        public string WarningMessage
        {
            get => _WarningMessage;
            set
            {
                _WarningMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _IsValidThisForms;
        public bool IsValidThisForms
        {
            get => _IsValidThisForms;
            set
            {
                _IsValidThisForms = value;
                OnPropertyChanged();
            }
        }


        /*
         * View Status
         */
        private bool _IsInitialize;
        public bool IsInitialize
        {
            get => _IsInitialize;
            set
            {
                _IsInitialize = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                IsNotBusy = !_isBusy;
                OnPropertyChanged();
            }
        }

        private bool _isNotBusy;
        public bool IsNotBusy
        {
            get => _isNotBusy;
            set
            {
                _isNotBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _IsRefresh;
        public bool IsRefresh
        {
            get => _IsRefresh;
            set
            {
                _IsRefresh = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshScreenCommand => new AsyncCommand(OnRefreshScreenCommand);
        public virtual Task OnRefreshScreenCommand()
        {
            return Task.FromResult(true);
        }

        protected void DebugMessage(string content, string title = "")
        {
            Debug.WriteLine(title + ": " + content);
        }

        protected virtual async void RunActionOnNewThread(Func<Task> action, CancellationTokenSource cancellationToken, bool IsSyncBusy = false, int timeDelay = 100)
        {
            try
            {
                if(IsSyncBusy)
                    IsBusy = true;

                await Task.Delay(timeDelay);
                await Task.Run(action, cancellationToken.Token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (IsSyncBusy)
                    IsBusy = false;
            }
        }

        protected virtual async void RunActionOnNewThread(Action action, bool IsSyncBusy = false, int timeDelay = 100)
        {
            try
            {
                if (IsSyncBusy)
                    IsBusy = true;

                await Task.Delay(timeDelay);
                action();
            }
            catch (Exception ex)
            {
                DebugMessage(ex.StackTrace);
            }
            finally
            {
                if (IsSyncBusy)
                    IsBusy = false;
            }
        }

        protected virtual async void RunActionOnNewThread(Func<Task> action, bool IsSyncBusy = false, int timeDelay = 100)
        {
            try
            {
                if (IsSyncBusy)
                    IsBusy = true;

                await Task.Delay(timeDelay);
                await action();
            }
            catch (Exception ex)
            {
                DebugMessage(ex.StackTrace);
            }
            finally
            {
                if (IsSyncBusy)
                    IsBusy = false;
            }
        }

        public BaseViewModel()
        {

        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task OnViewAppearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }

        public virtual Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }
    }
}