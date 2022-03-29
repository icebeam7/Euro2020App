using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Euro2020App.Services
{
    public static class WebApiService
    {
        private const string apiUrl = "https://euro2020webapi.azurewebsites.net/";

        private static readonly HttpClient client = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        public async static Task<List<T>> GetData<T>(string service)
        {
            try
            {
                var url = $"api/{service}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<T>>(jsonResult);
                    return data;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
