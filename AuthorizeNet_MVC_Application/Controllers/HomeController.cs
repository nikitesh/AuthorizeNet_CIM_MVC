using AuthorizeNet;
using AuthorizeNet_Library;
using System;
using System.Web.Mvc;

namespace AuthorizeNet_MVC_Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateCustomerProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveCustomerProfile(Customer customer)
        {
            return Json(CreateProfile(customer), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Empty()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult iframeCommunicator()
        {
            return View();
        }
        /// <summary>
        /// Creates the customer profile.
        /// </summary>
        /// <param name="customer">The customer.</param>
        private Message CreateProfile(Customer customer)
        {
            string appPath = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string authNetUrl = "https://apitest.authorize.net/soap/v1/Service.asmx"; //https://api.authorize.net/soap/v1/Service.asmx for live
            try
            {
                AuthorizeNetMerchant authMerchant = new AuthorizeNetMerchant { Merchant_Key = "26rLaK483ZCEf5cm", Merchant_Name = "4pmE98RwE" };
                string token = new CustomerProfileSetup(authMerchant, authNetUrl).CreateCustomerProfile(customer, appPath);
                string additionDetail = String.Format(@"{{ ""Token"": ""{0}""}}", token);

                return new Message { State = "Success", Data = additionDetail };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
