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
            ConfigurationManager.AppSettings["EspressoURL"] = "http://espressologic-msftdemo.cloudapp.net/rest/msftdemo/advwrk/v1/";
            ConfigurationManager.AppSettings["APIKey"] = "dJDGTz6f88cmu4kk1hpW";
        }
    }
}
