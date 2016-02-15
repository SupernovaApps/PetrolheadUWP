using Petrolhead.Services.AuthenticationService;
using Petrolhead.Services.DeviceInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.Networking.Connectivity;

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
            }

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
