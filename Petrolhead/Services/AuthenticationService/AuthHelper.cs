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

        public static async Task<bool> AuthenticateAsync(MobileServiceAuthenticationProvider provider)
        {
            bool success = false;
            if (await GetSavedAuthenticationAsync(provider))
            {
                success = true;
                return success;
            }
            else
            {
                success = await SignInAsync(provider);
            }
            return success;
        }
       
        public static Task<bool> GetSavedAuthenticationAsync(MobileServiceAuthenticationProvider provider)
        {
            bool success = false;
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            

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
                
            }

            return Task.FromResult<bool>(success);
           
        }

        public static async Task<bool> SignInAsync(MobileServiceAuthenticationProvider provider)
        {
            bool success = false;
            try
            {
                PasswordVault vault = new PasswordVault();
                PasswordCredential credential = null;
                User = await App.MobileService.LoginAsync(provider, true);
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
            return success;
        } 
    }
}
