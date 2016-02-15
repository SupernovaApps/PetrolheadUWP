using Petrolhead.Services.AuthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace Petrolhead.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
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
