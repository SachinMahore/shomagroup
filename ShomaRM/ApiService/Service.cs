using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShomaRM.ApiService
{
    public class Service
    {
        public static async Task<string> AccessTokenGenerator()
        {
          
            string clientId = ConfigurationManager.AppSettings["ClientId"]; // Your Azure AD Application ID  
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"]; // Client secret generated in your App  
            string authority = "https://login.microsoftonline.com/"+ConfigurationManager.AppSettings["ClientTenantId"] +"/oauth2/v2.0/token"; // Azure AD App Tenant ID  
            string resourceUrl = "https://graph.microsoft.com/"; // Your Dynamics 365 Organization URL  

            var credentials = new ClientCredential(clientId, clientSecret);
            var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority, false);
            var result = await authContext.AcquireTokenAsync(resourceUrl, credentials);
            return result.AccessToken;
        }
        public static async Task<TResult> PostFormUrlEncoded<TResult>(string url, IEnumerable<KeyValuePair<string, string>> postData)
        {

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    return await response.Content.ReadAsAsync<TResult>();
                }
            }
        }
        public  async Task<HttpResponseMessage> CrmRequest(HttpMethod httpMethod, string requestUri, string body = null)
        {

            var values = new[] {
      new KeyValuePair<string,string>("grant_type","password"),
      new KeyValuePair<string,string>("client_id",ConfigurationManager.AppSettings["ClientId"]),
      new KeyValuePair<string,string>("client_secret",ConfigurationManager.AppSettings["ClientSecret"]),
      new KeyValuePair<string,string>("scope","https://graph.microsoft.com/.default"),
      new KeyValuePair<string,string>("userName",ConfigurationManager.AppSettings["UserName"]),
      new KeyValuePair<string,string>("password",ConfigurationManager.AppSettings["Password"])
    };
            // Acquiring Access Token  
            var accessToken = await PostFormUrlEncoded<object>("https://login.microsoftonline.com/"+ ConfigurationManager.AppSettings["ClientTenantId"] + "/oauth2/v2.0/token", values);
            var Token = JsonConvert.DeserializeObject<Token>(accessToken.ToString());

            // Acquiring Access Token  
           

            var client = new HttpClient();
            var message = new HttpRequestMessage(httpMethod, requestUri);

         

            // Passing AccessToken in Authentication header  
            message.Headers.Add("Authorization", $"Bearer {Token.access_token}");

            // Adding body content in HTTP request   
            if (body != null)
                message.Content = new StringContent(body, UnicodeEncoding.UTF8, "application/json");

            return await client.SendAsync(message);
        }
    }
}