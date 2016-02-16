using Petrolhead.Services.AuthenticationService;
using Petrolhead.Services.DeviceInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Utils;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml.Navigation;

namespace Petrolhead.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {

        public LoginPageViewModel()
        {
            DeviceInfoHelper.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DeviceInfoHelper.Instance.HasInternet)
            {
                ErrorText = "";
                IsLoginBtnEnabled = true;
            }
            else
            {
                ErrorText = "You must be connected to the Internet to sign in.";
                IsLoginBtnEnabled = false;
            }

        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (DeviceInfoHelper.Instance.HasInternet)
            {
                ErrorText = "";
                IsLoginBtnEnabled = true;
            }
            else
            {
                ErrorText = "You must be connected to the Internet to sign in.";
                IsLoginBtnEnabled = false;
            }
            return Task.CompletedTask;
        }

        private bool? _IsLoginBtnEnabled = false;
        public bool? IsLoginBtnEnabled { get { return _IsLoginBtnEnabled; } set { Set(ref _IsLoginBtnEnabled, value); } }
        


        /// <summary>
        /// Performs an asynchronous authentication using your Microsoft Account.
        /// </summary>
        /// <returns>Returns a boolean value representing status</returns>
        protected async Task<bool> AuthenticateAsync()
        {
            return (await AuthHelper.AuthenticateAsync());
        }

        private string _ErrorText = "";
        public string ErrorText { get { return _ErrorText; } protected set { Set(ref _ErrorText, value); } }
        public async void Login()
        {
            
            if (await AuthenticateAsync())
            {
                if (App.Settings.IsAlreadyConfigured == false)
                {

                    NavigationService.Navigate(typeof(Views.SetupWizardHomePage));
                    NavigationService.ClearHistory();
                }
                else
                {
                    NavigationService.Navigate(typeof(Views.MainPage));
                    NavigationService.ClearHistory();
                }
                
            }
            else
            {
                ErrorText = "Your last login attempt was unsuccessful.";
            }
                
        }
    }
}
