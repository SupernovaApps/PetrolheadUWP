using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Petrolhead.ViewModels
{
    public class SetupWizardStartPageViewModel : ViewModelBase
    {

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await ShowIncompleteDialog();
        }

        private async Task ShowIncompleteDialog()
        {
            Views.IncompleteAreaDialog dialog = new Views.IncompleteAreaDialog()
            {
                WarningText = "Sorry, but the setup wizard has not been implemented yet."
            };
            dialog.Closed += (s, e) =>
            {
                if (e.Result == ContentDialogResult.Primary)
                    NavigationService.Navigate(typeof(Views.MainPage));


            };
            await dialog.ShowAsync();
        }
        public void Start()
        {
            throw new NotImplementedException("The setup wizard has not been implemented beyond this point.");
        }
    }
}
