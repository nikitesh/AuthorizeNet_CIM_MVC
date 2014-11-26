using AuthorizeNet;
using AuthorizeNet_Library.CustomerProfileWS;
using AuthorizeNet_Library;
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
        private ServiceMode serviceMode;
        private string AuthNetUrl;
        private AuthorizeNetMerchant AuthNetMerchant;
        //= new AuthorizeNetMerchant { Merchant_Key = "26rLaK483ZCEf5cm", Merchant_Name = "4pmE98RwE" };

        public CustomerProfileSetup(AuthorizeNetMerchant authMerchant, string AuthNetUrl)
        {
            if (string.IsNullOrEmpty(AuthNetUrl))
                throw new ArgumentNullException("AuthNetUrl argument is null.");
            if (authMerchant == null)
                throw new ArgumentNullException("Authorize merchant argument is null.");

            this.serviceMode = authMerchant.IsTest == true ? ServiceMode.Test : ServiceMode.Live;
            this.AuthNetUrl = AuthNetUrl;
            this.AuthNetMerchant = authMerchant;
        }

        /// <summary>
        /// Create Customer in Payment Gateway
        /// </summary>
        /// <returns> Customer  </returns>
        public string CreateCustomerProfile(Customer customerAccount, string appPath)
        {
            return this.CreateCustomerProfile(customerAccount.Email, customerAccount.Description, appPath);
        }

        public string CreateCustomerProfile(string Email, string Description, string appPath)
        {
            try
            {
                AuthorizeNet.CustomerGateway customerGateway = new AuthorizeNet.CustomerGateway(AuthNetMerchant.Merchant_Name, AuthNetMerchant.Merchant_Key, serviceMode);
                Customer customer = customerGateway.CreateCustomer(Email, Description);

                if (customer == null)
                {
                    throw new ArgumentNullException("Customer");
                }
                string token = this.GetHostedProfilePage(customer.ProfileID, appPath);
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
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

                st.settingValue = siteBaseURL + "/Home/IframeCommunicator";
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
