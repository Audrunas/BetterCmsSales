using System;
using System.Web.Mvc;
using System.Web.Routing;
using BetterCms.Core;
using BetterCms.Core.Environment.Host;

namespace BetterCmsSales.Web.Mvc4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static ICmsHost cmsHost;

        protected void Application_Start()
        {
            cmsHost = CmsContext.RegisterHost();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            cmsHost.OnApplicationStart(this);
        }

        protected void Application_BeginRequest()
        {
            // [YOUR CODE]

            cmsHost.OnBeginRequest(this);
        }

        protected void Application_EndRequest()
        {
            // [YOUR CODE]

            cmsHost.OnEndRequest(this);
        }

        protected void Application_Error()
        {
            // [YOUR CODE]

            cmsHost.OnApplicationError(this);
        }

        protected void Application_End()
        {
            // [YOUR CODE]

            cmsHost.OnApplicationEnd(this);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // [YOUR CODE]
            cmsHost.OnAuthenticateRequest(this);
        }
    }
}