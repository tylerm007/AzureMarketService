using System;
using System.Web;
using System.Net.Http;
using System.Configuration;

namespace AzureMarketRESTService
{
    public class ProxyHandler : DelegatingHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
         

        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            string remoteWebSite = ConfigurationManager.AppSettings["EspressoURL"];
            
            string apikey = ConfigurationManager.AppSettings["APIKey"];
            string urlPart = parseURL(request);//request remove everything after /api/
            UriBuilder forwardUri = new UriBuilder(remoteWebSite + urlPart);
            //strip off the proxy port and replace with an Http port
            forwardUri.Port = 80;
            //send it on to the requested URL
            request.RequestUri = forwardUri.Uri;
            HttpClient client = new HttpClient();
            request.Headers.Add("Authorization", string.Format("Espresso {0}", apikey + ":1"));
            if (request.Method == HttpMethod.Get)
            {
                request.Content = null;
            }
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
     
            return response;
        }

        private string parseURL(HttpRequestMessage request)
        {
            string url =  request.RequestUri.OriginalString;
            int index = url.IndexOf("/api/");
            if(index > 0){
                url = url.Substring(index + 5);
            }
            return url;
        }
    }
    
}
