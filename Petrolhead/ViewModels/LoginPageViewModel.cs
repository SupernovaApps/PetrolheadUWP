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
            if (DeviceInfoHelper.Instance.Connectivity == NetworkConnectivityLevel.InternetAccess)
            {
                ErrorText = "";
                IsLoginBtnEnabled = true;
            }
            else
            {
                ErrorText = "You must have an Internet connection to sign in.";
                IsLoginBtnEnabled = false;
            }

        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (DeviceInfoHelper.Instance.Connectivity == NetworkConnectivityLevel.InternetAccess)
            {
                ErrorText = "";
                IsLoginBtnEnabled = true;
            }
            else if (DeviceInfoHelper.Instance.Connectivity == NetworkConnectivityLevel.LocalAccess || DeviceInfoHelper.Instance.Connectivity == NetworkConnectivityLevel.ConstrainedInternetAccess)
            {
                if (DeviceUtils.Current().IsPhone() || DeviceUtils.Current().IsContinuum())
                {
                    if (NetworkInformation.GetInternetConnectionProfile().IsWwanConnectionProfile)
                    {
                        ErrorText = "Your 3G/LTE connection appears to be flaky. Please try again later.";
                        IsLoginBtnEnabled = false;
                        return Task.CompletedTask;
                    }

                }
                ErrorText = "Petrolhead cannot connect to the login server. Please try again later.";
                IsLoginBtnEnabled = false;
            }
            else
            {
                ErrorText = "You're not connected to a network. Please connect to a network to sign in.";
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
                NavigationService.Navigate(typeof(Views.MainPage));
            }
            else
            {
                ErrorText = "Your last login attempt was unsuccessful.";
            }
                
        }
    }
}
