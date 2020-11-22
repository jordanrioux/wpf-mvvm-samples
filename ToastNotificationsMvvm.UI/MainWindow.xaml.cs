using System.Windows;
using ToastNotificationsMvvm.Common.ViewModels;
using ToastNotificationsMvvm.UI.Services;

namespace ToastNotificationsMvvm.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToastHelper _toastHelper;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            _toastHelper = new ToastHelper(Application.Current);
        }
    }
}
