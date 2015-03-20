using System;
using System.Web;
using System.Net.Http;
using System.Configuration;
using System.Diagnostics;

namespace AzureMarketRESTService
{
    public class ProxyHandler : DelegatingHandler
    {
        private string restOrData = "/rest/";
        
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            string remoteWebSite = buildRemoteURI();
            string apikey = getOrCreateAPIKey();

            UriBuilder remoteURI = new UriBuilder(remoteWebSite);
            UriBuilder originalURI = new UriBuilder(request.RequestUri);            
            string urlPart = parseURL(request.RequestUri.PathAndQuery);
            UriBuilder forwardUri = new UriBuilder(remoteWebSite + urlPart);
            forwardUri.Port = 80;
            
            request.RequestUri = forwardUri.Uri;
            HttpClient client = new HttpClient();
            request.Headers.Host = remoteURI.Host;
            request.Headers.Add("Authorization", string.Format("Espresso {0}", apikey + ":1"));
            if (request.Method == HttpMethod.Get)
            {
                request.Content = null;
            }
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
               return await stripOutURLLinks(response, remoteURI, originalURI);
            }
      
            return response;
        }

        private string buildRemoteURI()
        {
            //only need to do this once
            string uri = ConfigurationManager.AppSettings["EspressoURL"];
            if (uri == null)
            {
                string serverName = ConfigurationManager.AppSettings["ServerName"];
                string projectName = ConfigurationManager.AppSettings["AccountName"];
                string urlFragment = ConfigurationManager.AppSettings["ProjectName"];
                string version = ConfigurationManager.AppSettings["DefaultAPIVersion"];            
                uri = "http://" + serverName + restOrData + projectName + "/" + urlFragment + "/" + version + "/";
                ConfigurationManager.AppSettings["EspressoURL"] = uri;
            }
            return uri;
        }

        private string getOrCreateAPIKey()
        {
            string apiKey = ConfigurationManager.AppSettings["APIKey"];
            
            bool api = ConfigurationManager.AppSettings["UseAPIKey"] == "true";
            if (api && apiKey == null)
            {
                //need to logon to Espresso and get a temporary apiKey
                ConfigurationManager.AppSettings["APIKey"] = apiKey;
            }

            return apiKey;
        }

        private async System.Threading.Tasks.Task<HttpResponseMessage> stripOutURLLinks(HttpResponseMessage response, UriBuilder remoteURI, UriBuilder originalURI)
        {
            string content = await response.Content.ReadAsStringAsync();
            content = content.Replace(remoteURI.Uri.Authority, originalURI.Uri.Authority);
            content = content.Replace(remoteURI.Path, "/api/");
            string dataPath;
            string path = remoteURI.Path.Replace("/v1/", "");
            
            content = content.Replace(path, "/api");
            var oldHeaders = response.Content.Headers;

            response.Content = new StringContent(content);
            return response;
        }

        private string parseURL(string pathAndQuery)
        {
            string url = "";
            int index = pathAndQuery.IndexOf("/api/");
            if (index == 0)
            {
                url = pathAndQuery.Substring(index + 5);
            }
            else
            {
                throw new Exception("missing /api/{resource|tabl|view}[?{params}]");
            }
            return url;
        }
    }

}
