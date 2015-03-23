using System;
using System.Web;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using System.Diagnostics;


namespace AzureMarketRESTService
{
    public class ProxyHandler : DelegatingHandler
    {
        private string restOrData = "/rest/";

        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            UriBuilder originalURI = new UriBuilder(request.RequestUri);
            string remoteWebSite = buildRemoteURI(request);
            string apikey = getOrCreateAPIKey();
            UriBuilder remoteURI = new UriBuilder(remoteWebSite);
            string urlPart = parseURL(request.RequestUri.PathAndQuery);
            UriBuilder forwardUri = new UriBuilder(remoteWebSite + urlPart);
            forwardUri.Port = 80;

            request.RequestUri = forwardUri.Uri;
            HttpClient client = new HttpClient();
            request.Headers.Host = remoteURI.Host;
            request.Headers.Add("Authorization", string.Format("Espresso {0}", apikey + ":1"));
            HttpResponseMessage response = null;
            if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Options)
            {
                request.Content = null;
                response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            }
            else
            {
                if (request.Method == HttpMethod.Post)
                {
                    response = await client.PostAsync(request.RequestUri, request.Content, cancellationToken);
                }
                else
                {
                    if (request.Method == HttpMethod.Put)
                    {
                        response = await client.PutAsync(request.RequestUri, request.Content, cancellationToken);
                    }
                    else
                    {
                        if (request.Method == HttpMethod.Delete)
                        {
                            response = await client.DeleteAsync(request.RequestUri, cancellationToken);
                        }
                    }
                }
            }

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Trace.WriteLine("Response is Successful - starting strip code.");
                string content = await response.Content.ReadAsStringAsync();
                int len = content.Length;
                content = stripOutURLLinks(content, remoteURI, originalURI);
                response.Content = new StringContent(content);
                
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                System.Diagnostics.Trace.WriteLine(response.Headers.ToString());
                System.Diagnostics.Trace.WriteLine(len);
            }
            System.Diagnostics.Trace.WriteLine("Response returns - should be done. ");
            return response;
        }

        private string buildRemoteURI(HttpRequestMessage request)
        {
            restOrData = "/rest/";
            if (request.RequestUri.PathAndQuery.StartsWith("/api/data/"))
            {
                restOrData = "/data/";
            }
            string uri = ConfigurationManager.AppSettings["EspressoURL"];
            string serverName = ConfigurationManager.AppSettings["ServerName"];
            string projectName = ConfigurationManager.AppSettings["AccountName"];
            string urlFragment = ConfigurationManager.AppSettings["ProjectName"];
            string version = ConfigurationManager.AppSettings["DefaultAPIVersion"];
            if (serverName != null && projectName != null & urlFragment != null)
            {
                uri = "http://" + serverName + restOrData + projectName + "/" + urlFragment + "/" + version + "/";
            }
            ConfigurationManager.AppSettings["EspressoURL"] = uri;
            return uri;
        }

        private string getOrCreateAPIKey()
        {
            string apiKey = ConfigurationManager.AppSettings["APIKey"];
           // bool api = ConfigurationManager.AppSettings["UseAPIKey"] == "true";
           // if (api && apiKey == null)
           // {
           //need to logon to Espresso and get a temporary apiKey
           //    ConfigurationManager.AppSettings["APIKey"] = apiKey;
           // }
            return apiKey;
        }

        private string stripOutURLLinks(string content, UriBuilder remoteURI, UriBuilder originalURI)
        {
            //System.Diagnostics.Trace.WriteLine(content);
            content = content.Replace(remoteURI.Uri.Authority, originalURI.Uri.Authority);
            content = content.Replace(remoteURI.Path, "/api/");
            string projectpart = remoteURI.Path.Substring(5);
            projectpart = "data" + projectpart;
            string path = remoteURI.Path.Replace("/v1/", "");
            content = content.Replace(path, "/api");
            content = content.Replace(projectpart, "api/data/");
            System.Diagnostics.Trace.WriteLine("Finished Strip Code.");
            return content;
        }

        private string parseURL(string pathAndQuery)
        {
            string url = "";
            string newURL = pathAndQuery;
            if (pathAndQuery.StartsWith("/api/data/"))
            {
                newURL = pathAndQuery.Replace("/api/data/", "/api/");
            }
            int index = newURL.IndexOf("/api/");
            if (index == 0)
            {
                url = newURL.Substring(index + 5);
            }
            else
            {
                throw new Exception("incorrect syntax - missing /api/[data]/{resource|table|view}[?{params}]");
            }
            return url;
        }
    }

}
