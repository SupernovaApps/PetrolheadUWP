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
       

        private static void NetworkInformation_NetworkStatusChanged(object sender)
        {
            Instance.Connectivity = (NetworkInformation.GetInternetConnectionProfile()).GetNetworkConnectivityLevel();
        }

        private  NetworkConnectivityLevel _connectivity = (NetworkInformation.GetInternetConnectionProfile()).GetNetworkConnectivityLevel();
        public  NetworkConnectivityLevel Connectivity
        {
            get
            {
                return _connectivity;
            }
            protected set
            {
                Set(ref _connectivity, value);
                
            }
        }
        

private bool HasInternet()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null &&
                    connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }
    }
}
