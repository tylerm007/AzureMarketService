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
            ConfigurationManager.AppSettings["ServerName"] = "";
            ConfigurationManager.AppSettings["AccountName"] = "";
            ConfigurationManager.AppSettings["ProjectName"] = "";
            ConfigurationManager.AppSettings["DefaultAPIVersion"] = "";
            ConfigurationManager.AppSettings["UserName"] = "";
            ConfigurationManager.AppSettings["Password"] = "";
            ConfigurationManager.AppSettings["UseAPIKey"] = "true";
            //ConfigurationManager.AppSettings["EspressoURL"] = "";
            //"http://espressologic-msftdemo.cloudapp.net/rest/msftdemo/demo/v1/";
            //"http://espressologic-msftdemo.cloudapp.net/rest/msftdemo/advwrk/v1/";
            ConfigurationManager.AppSettings["APIKey"] = "";//"";
        }
    }
}
