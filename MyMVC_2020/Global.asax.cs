using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Web.Mvc;

namespace MyMVC_2020
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
  
            //===
            //Enabling Bundling and Minification
            BundleTable.EnableOptimizations = false;

            BundleConfig.RegisterBundles(BundleTable.Bundles);            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                //application.Context.Response.Headers.Remove("Server");                
                //為了資安，把http header 的server標記移除，不讓Client端查出Server是用什麼軟體當Server。
            }            
        }
    }
}
