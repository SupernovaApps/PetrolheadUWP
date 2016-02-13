using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Petrolhead.Services.AuthenticationService
{
    public class AuthHelper
    {
        private static MobileServiceUser user;
        public static MobileServiceUser User
        {
            get { return user; }
            set { user = value; }
        }

       
        public static async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            var provider = MobileServiceAuthenticationProvider.MicrosoftAccount;

            try
            {
                // Try to get an existing credential from the vault.
                credential = vault.FindAllByResource(provider.ToString()).First();
            }
            catch (Exception)
            {
                // ignore this error
            }

            if (credential != null)
            {
                user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;
                App.MobileService.CurrentUser = user;
                success = true;
            }
            else
            {
                try
                {
                    User = await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount, true);
                    // Create and store the user credentials.
                    credential = new PasswordCredential(provider.ToString(),
                        user.UserId, user.MobileServiceAuthenticationToken);
                    vault.Add(credential);
                    success = true;
                }
                catch (MobileServiceInvalidOperationException)
                {
                    
                }
                catch (InvalidOperationException)
                {

                }
            }

            return success;
           
        }
    }
}
