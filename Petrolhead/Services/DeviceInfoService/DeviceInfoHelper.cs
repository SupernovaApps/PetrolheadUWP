using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.Networking.Connectivity;

namespace Petrolhead.Services.DeviceInfoService
{
    public class DeviceInfoHelper : BindableBase
    {
        private static DeviceInfoHelper _instance = new DeviceInfoHelper();
        public static DeviceInfoHelper Instance
        {
            get
            {
                if (_instance == null)
                    return _instance ?? (_instance = new DeviceInfoHelper());
                return _instance;

            }
            set
            {
                _instance = value;
            }

        }
       
        public DeviceInfoHelper()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        private async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            // change network connection status in the background
            await Task.Run(new Action(GetConnectionStatus));
            
        }

        protected virtual void GetConnectionStatus()
        {
            Instance.HasInternet = ((NetworkInformation.GetInternetConnectionProfile()) != null && (NetworkInformation.GetInternetConnectionProfile()).GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        private bool _HasInternet = ((NetworkInformation.GetInternetConnectionProfile()) != null && (NetworkInformation.GetInternetConnectionProfile()).GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        public bool HasInternet
        {
            get
            {
                return _HasInternet;
            }
            protected set
            {
                Set(ref _HasInternet, value);
            }
        }


    }
}
