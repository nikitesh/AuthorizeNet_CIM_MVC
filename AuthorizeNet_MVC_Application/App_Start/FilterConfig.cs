using System.Web;
using System.Web.Mvc;

namespace AuthorizeNet_MVC_Application
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}