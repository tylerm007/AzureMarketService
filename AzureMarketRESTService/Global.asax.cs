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
            ConfigurationManager.AppSettings["UserName"] = "";
            ConfigurationManager.AppSettings["Password"] = "";
            ConfigurationManager.AppSettings["UseAPIKey"] = "true";
            //ConfigurationManager.AppSettings["EspressoURL"] = "http://espressologic-msftdemo.cloudapp.net/rest/msftdemo/demo/v1/";
            //"http://espressologic-msftdemo.cloudapp.net/rest/msftdemo/advwrk/v1/";
            ConfigurationManager.AppSettings["APIKey"] = "demo_full";//"dJDGTz6f88cmu4kk1hpW";
        }
    }
}
