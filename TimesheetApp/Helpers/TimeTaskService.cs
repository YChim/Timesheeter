using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Helpers
{
    public class TimeTaskService
    {
        private readonly HttpClientHandler _sharedHandler = new HttpClientHandler();
        public readonly string _token;

        public string BaseUri => "https://api.myintervals.com/";

        public TimeTaskService(string token)
        {
            _token = token;
        }
        public async Task<string> MakeServiceCall(string uri, RequestType type, StringContent json = null)
        {
            try
            {
                var client = GetClient(ConvertTokenToBasic(_token));

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                switch (type)
                {
                    case RequestType.Get:
                        var response = await client.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            return response.Content.ReadAsStringAsync().Result;
                        }
                        client.Dispose();
                        break;
                    case RequestType.Post:
                        var timeResponse = await client.PostAsync(uri, json);

                        if (timeResponse.IsSuccessStatusCode)
                        {
                            return timeResponse.Content.ReadAsStringAsync().Result;
                        }
                        client.Dispose();
                        break;
                }

                client.Dispose();
                return string.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private HttpClient GetClient(string token)
        {
            return new HttpClient(_sharedHandler, false)
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Basic", token)
                }
            };
        }

        private string ConvertTokenToBasic(string username)
        {
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + "RandomString")); 
            //Access token on it own it fails ???????
            return encoded;
        }
    }
}
