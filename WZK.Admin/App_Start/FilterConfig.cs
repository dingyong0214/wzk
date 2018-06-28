using System.Web;
using System.Web.Mvc;
using WZK.Admin.Core;

namespace WZK.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthFilter());
        }
    }
}
