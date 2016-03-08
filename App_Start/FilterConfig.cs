using System.Web;
using System.Web.Mvc;

namespace WDTAssignment2NWBA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}