using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;

namespace NUnitSeleniumTestProjectExample.Helpers
{
    public static class HttpRequests
    {
        private static string authType;
        private static string authToken;
        private static string baseAddress;
        private static bool authCapable;
        private static bool _initialized;

        public static bool Initialized 
        {
            get 
            {
                if (authCapable)
                {
                    if (!string.IsNullOrWhiteSpace(authType) && !string.IsNullOrWhiteSpace(authToken) && !string.IsNullOrWhiteSpace(baseAddress))
                    {
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(baseAddress))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static bool IsAuth
        {
            get
            {
                return authCapable;
            }
        }

        public static void InitializaClient(string aType, string aToken, string bAddress) 
        {
            authType = aType;
            authToken = aToken;
            baseAddress = bAddress;
            authCapable = true;
        }

        public static void InitializaClient(string bAddress) 
        {
            authType = "";
            authToken = "";
            baseAddress = bAddress;
            authCapable = false;
        }

        public static async Task<T> GetAsync<T>(string uri)
        {
            string responseString  = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                try	
                {
                    client.BaseAddress = new Uri(baseAddress);
                    if (IsAuth)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType, authToken);
                    }
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    responseString = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    //Console.WriteLine(responseString);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");	
                    Console.WriteLine("Message :{0} ",e.Message);
                }
            }

            T resp = JsonSerializer.Deserialize<T>(responseString);
            return resp ;
        }

        public static async Task<T> GetAsync<T>(string uri, Dictionary<string,string> requestParams)
        {
            StringBuilder sbURL = new StringBuilder(uri);
            if (requestParams.Count > 0)
            {
                sbURL = sbURL.Append("?");
                foreach (KeyValuePair<string,string> param in requestParams)
                {
                    sbURL.Append(param.Key).Append("=").Append(param.Value).Append("&");
                }

                //remove last & char
                sbURL.Remove(sbURL.Length-1, 1);
            }
            
            string responseString  = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                try	
                {
                    client.BaseAddress = new Uri(baseAddress);
                    if (IsAuth)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType, authToken);
                    }

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(sbURL.ToString());
                    response.EnsureSuccessStatusCode();
                    responseString = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    //Console.WriteLine(responseString);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");	
                    Console.WriteLine("Message :{0} ",e.Message);
                }
            }

            T resp = JsonSerializer.Deserialize<T>(responseString);
            return resp ;
        }

        public static async Task<T> PostAsync<T,V>(string uri, V obj)
        {
            string jsonString = JsonSerializer.Serialize<V>(obj);
            string responseString  = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                try	
                {
                    client.BaseAddress = new Uri(baseAddress);
                    if (IsAuth)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType, authToken);
                    }
                    
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                    request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");//CONTENT-TYPE header

                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    responseString = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    //Console.WriteLine(responseString);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");	
                    Console.WriteLine("Message :{0} ",e.Message);
                }
            }

            T resp = JsonSerializer.Deserialize<T>(responseString);
            return resp ;
        }
    }
}