using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNet_Models
{
    public class AuthorizeNetMerchant
    {
        public int AuthorizeNet_Merchant_ID { get; set; }
        public string Merchant_Name { get; set; }        
        public string Merchant_Key { get; set; }
        public bool IsTest { get; set; }        
    }
}
