using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Configuration;

namespace AzureMarketRESTService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ConfigurationManager.AppSettings["ServerName"] = "espressologic-msftdemo.cloudapp.net";
            ConfigurationManager.AppSettings["AccountName"] = "msftdemo";
            ConfigurationManager.AppSettings["ProjectName"] = "demo";
            ConfigurationManager.AppSettings["DefaultAPIVersion"] = "v1";
            ConfigurationManager.AppSettings["UseAPIKey"] = "true";
            ConfigurationManager.AppSettings["APIKey"] = "demo_full";
        }
    }
}
