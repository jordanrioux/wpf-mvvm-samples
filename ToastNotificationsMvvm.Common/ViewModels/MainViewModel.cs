using SimpleMvvmToolkit.Express;
using System;
using System.Linq.Expressions;
using System.Windows.Input;
using ToastNotificationsMvvm.Common.Services;

namespace ToastNotificationsMvvm.Common.ViewModels
{
    public class MainViewModel : ViewModelBase<MainViewModel>
    {        
        private readonly ToastService _toastService;

        public ICommand ShowToastCommand => new DelegateCommand(ShowToast);

        public MainViewModel()
        {
            _toastService = new ToastService();            
        }

        private void ShowToast()
        {
            _toastService.ShowToastInformation("Oh noes! Where did this information comes from?");
        }

        #region Simplified Binding code example

        //
        // Below code is not related to the Toast, it's only serving as an example on
        // how you could create some base methods to simplify the binding code, etc.
        //
        private readonly Random _random;

        private int _randomValue;
        public int RandomValue
        {
            get => _randomValue;
            set => SetAndNotifyPropertyChanged(ref _randomValue, value, vm => vm.RandomValue);
        }

        // Method should be moved to a base class to be re-used.
        // It uses the ref keyword to pass the field as a reference, sets the value and notify that the property has changed.
        private void SetAndNotifyPropertyChanged<TValue, TResult>(
                ref TValue field,
                TValue newValue, 
                Expression<Func<MainViewModel, TResult>> property)
        {
            field = newValue;
            NotifyPropertyChanged(property);
        }

        #endregion
    }
}
