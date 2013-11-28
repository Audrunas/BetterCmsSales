using System.Web.Mvc;
using System.Web.Routing;

namespace BetterCmsSales.Web.Mvc4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}