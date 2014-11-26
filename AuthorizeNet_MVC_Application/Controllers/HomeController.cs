using AuthorizeNet;
using AuthorizeNet_Library;
using AuthorizeNet_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public JsonResult SaveCustomerProfile(CustomerAccount customer)
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
        private Message CreateProfile(CustomerAccount customer)
        {
            string appPath = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                AuthorizeNetMerchant authMerchant = new AuthorizeNetMerchant() { Merchant_Key = "", Merchant_Name = "" };

                if (authMerchant == null)
                    return new Message { State = "Failed", Data = "Merchant not found" };

                //Authorize.net Library Entity : Customer
                Customer authCustomer = new CustomerProfileSetup().CreateCustomerProfile(customer);

                if (authCustomer == null)
                {
                    //return a unique error code that client side recognizes that an error occurred while creating a profile payment on gateway
                    return new Message { State = "Failed", Data = "Could not reach auth.net" };
                }
                string token = new CustomerProfileSetup().GetHostedProfilePage(authCustomer.ProfileID, appPath);

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
