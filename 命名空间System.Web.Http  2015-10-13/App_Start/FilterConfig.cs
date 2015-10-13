using System.Web;
using System.Web.Mvc;

namespace 命名空间System.Web.Http__2015_10_13
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}