using AuthorizeNet;
using AuthorizeNet_Library.CustomerProfileWS;
using AuthorizeNet_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNet_Library
{
    public class CustomerProfileSetup
    {
        private const ServiceMode serviceMode = ServiceMode.Test;
        private const string AuthNetUrl = "https://apitest.authorize.net/soap/v1/Service.asmx"; //https://api.authorize.net/soap/v1/Service.asmx for live
        private AuthorizeNetMerchant AuthNetMerchant = new AuthorizeNetMerchant { Merchant_Key = "26rLaK483ZCEf5cm", Merchant_Name = "4pmE98RwE" };

        

        /// <summary>
        /// Create Customer in Payment Gateway
        /// </summary>
        /// <returns> Customer  </returns>
        public Customer CreateCustomerProfile(CustomerAccount parkerAccount)
        {
            return this.CreateCustomerProfile(parkerAccount.Email, parkerAccount.Details);
        }

        public Customer CreateCustomerProfile(string Email, string Description)
        {
            try
            {
                AuthorizeNet.CustomerGateway customerGateway = new AuthorizeNet.CustomerGateway(AuthNetMerchant.Merchant_Name, AuthNetMerchant.Merchant_Key, serviceMode);
                Customer customer = customerGateway.CreateCustomer(Email, Description);
                return customer;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetHostedProfilePage(string customerProfileID, string siteBaseURL)
        {
            try
            {
                MerchantAuthenticationType merchant = new MerchantAuthenticationType();
                merchant.name = AuthNetMerchant.Merchant_Name;
                merchant.transactionKey = AuthNetMerchant.Merchant_Key;
                SettingType st = new SettingType();
                st.settingName = "hostedProfileIFrameCommunicatorUrl";
                //get the base address of the site

                st.settingValue = siteBaseURL + "/Parker/IframeCommunicator";
                SettingType[] setArray = new SettingType[1];
                setArray[0] = st;

                BasicHttpsBinding httpsBinding = new BasicHttpsBinding();
                httpsBinding.Name = "ServiceSoap";
                EndpointAddress endPointAddress = new EndpointAddress(AuthNetUrl);

                ServiceSoapClient _ssc = new ServiceSoapClient(httpsBinding, endPointAddress);

                GetHostedProfilePageResponseType responseType = _ssc.GetHostedProfilePage(merchant, long.Parse(customerProfileID), setArray);

                string token = responseType.token;
                var resultCode = responseType.resultCode;
                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the payment information.
        /// </summary>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns>Customer </returns>
        public Customer GetPaymentInfo(string profileId)
        {
            try
            {
                AuthorizeNet.CustomerGateway customerGateway =
                new AuthorizeNet.CustomerGateway(AuthNetMerchant.Merchant_Name, AuthNetMerchant.Merchant_Key, serviceMode);
                Customer customer = customerGateway.GetCustomer(profileId);
                return customer;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
