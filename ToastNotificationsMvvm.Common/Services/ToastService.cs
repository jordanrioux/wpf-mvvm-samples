using SimpleMvvmToolkit.Express;
using ToastNotificationsMvvm.Common.Models;

namespace ToastNotificationsMvvm.Common.Services
{
    // By extending the ViewModelBase, we get access to the MessageBus functionality.
    // We could rewrite a wrapper to do it using the MessageBus and MessageBusProxy.   
    // For simplicity, we re-use existing ViewModelBase class event if the "name" doesn't match the intended role here.
    public class ToastService : ViewModelBase<ToastService>
    {
        public void ShowToastInformation(string message)
        {
            // Send a notification with an unique token to identify the message.
            // All classes that are registered on this token will execute the associated method.
            // See ToastHelper class in the UI project.            
            SendMessage(MessageTokens.ToastInformationMessage, new NotificationEventArgs(message));

            // NOTE: Use generic version to pass/receive your own class as data
            // SendMessage("unique_token", new NotificationEventArgs<MyClassInfo>(null, new MyClassInfo()));
        }
    }
}
