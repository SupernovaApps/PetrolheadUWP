using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;

namespace Petrolhead.Services.AuthenticationService
{
    public static class TokenExtension
    {
        public static bool IsTokenExpired(this IMobileServiceClient client)
        {
            // Get just the JWT part of the token.
            var jwt = client.CurrentUser
                .MobileServiceAuthenticationToken
                .Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+');
            jwt = jwt.Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new System.Exception(
               "The base64url string is not valid.");
            }

            // Decode the bytes from base64 and write to a JSON string.
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value, 
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the 
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            // If the expiration date is less than now, the token is expired and we return true.
            return expire < DateTime.UtcNow ? true : false;
        }

        public static bool IsTokenExpired(this IMobileServiceClient client, MobileServiceUser user)
        {
            // Get just the JWT part of the token.
            var jwt = user
                .MobileServiceAuthenticationToken
                .Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+');
            jwt = jwt.Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new System.Exception(
               "The base64url string is not valid.");
            }

            // Decode the bytes from base64 and write to a JSON string.
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value, 
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the 
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            // If the expiration date is less than now, the token is expired and we return true.
            return expire < DateTime.UtcNow ? true : false;
        }
    }
    public class AuthHelper
    {
        private static MobileServiceUser user;
        public static MobileServiceUser User
        {
            get { return user; }
            set { user = value; }
        }

        public static bool IsCachedCredentialsAvailable()
        {
            var credential = GetCachedCredentials();

            if (credential != null)
            {
                var user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;
                if (App.MobileService.IsTokenExpired(user))
                    return false;
                return true;
            }

            return false;
        }
        public static bool IsCachedCredentialsAvailable(PasswordCredential credential)
        {
            
                var user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;
                if (App.MobileService.IsTokenExpired(user))
                    return false;
                return true;
            
                
            
        }
       
        public static PasswordCredential GetCachedCredentials()
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            var provider = MobileServiceAuthenticationProvider.MicrosoftAccount;

            try
            {
                // Try to get an existing credential from the vault.
                credential = vault.FindAllByResource(provider.ToString()).First();

                try
                {
                    
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
                // When there is no matching resource an error occurs, which we ignore.
            }

            return credential;

            
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
                credential = GetCachedCredentials();
            }
            catch (Exception)
            {
                // ignore this error
            }

            if (IsCachedCredentialsAvailable(credential))
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
                    // Login with the identity provider.
                    user = await App.MobileService
                        .LoginAsync(provider);

                    // Create and store the user credentials.
                    credential = new PasswordCredential(provider.ToString(),
                        user.UserId, user.MobileServiceAuthenticationToken);
                    vault.Add(credential);

                    success = true;
                }
                catch (MobileServiceInvalidOperationException)
                {
                    MessageDialog dialog = new MessageDialog("Sorry, but you must sign in using your Microsoft Account to use Petrolhead.", "Whoops!");
                    await dialog.ShowAsync();
                }
                catch (InvalidOperationException)
                {
                    MessageDialog dialog = new MessageDialog("Sorry, but you must sign in using your Microsoft Account to use Petrolhead.", "Whoops!");
                    await dialog.ShowAsync();
                }
                catch (Exception ex)
                {
                    App.Telemetry.TrackException(ex);
                    MessageDialog dialog = new MessageDialog("An unknown error occurred while we were attempting to log you in.");
                    await dialog.ShowAsync();
                }
            }

            return success;
           
        }

        
    }
}
