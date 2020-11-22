using SimpleMvvmToolkit.Express;
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ToastNotificationsMvvm.Common.Models;

namespace ToastNotificationsMvvm.UI.Services
{
    // By extending the ViewModelBase, we get access to the MessageBus functionality.
    // We could rewrite a wrapper to do it using the MessageBus and MessageBusProxy.   
    // For simplicity, we re-use existing ViewModelBase class event if the "name" doesn't match the intended role here.
    public class ToastHelper : ViewModelBase<ToastHelper>
    {
        private Notifier _notifier;
        private readonly Application _app;        

        public ToastHelper(Application app)
        {
            _app = app;
            InitializeNotifier();
            RegisterToReceiveMessages();            
        }

        private void InitializeNotifier()
        {
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: _app.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = _app.Dispatcher;
            });
        }

        private void RegisterToReceiveMessages()
        {
            // Register to listen to message identified by the unique token.
            // If the token matches with any of the registered messages, the method will then be executed.
            RegisterToReceiveMessages(MessageTokens.ToastInformationMessage, OnToastMessageReceived);

            // NOTE: Use generic version to pass/receive your own class as data
            // RegisterToReceiveMessages<MyClassInfo>("unique_token", OnToastMessageReceived);
            // In the OnToastMessageReceived method, use e.Data to access your class information.
        }

        void OnToastMessageReceived(object sender, NotificationEventArgs e)
        {
            _app.Dispatcher.InvokeAsync(() => _notifier.ShowInformation(e.Message));
        }
    }
}
