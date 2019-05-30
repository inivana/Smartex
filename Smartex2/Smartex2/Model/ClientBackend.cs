using Newtonsoft.Json;
using Smartex.Exception;
using Smartex.Server;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Smartex.Model
{
    class ClientBackend
    {
        public static System.Uri api_domain = new System.Uri("https://opclouden.pythonanywhere.com/");
        public static HttpClientHandler handler = new HttpClientHandler();
        public static HttpClient client;
        public CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();


        static ClientBackend()
        {
            handler = new HttpClientHandler();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        // use: tylko do backendu 
        internal static async Task<ServerAnswerRecievedUser> CurrentUser()
        {
            try
            {
                String json = await ClientBackend.GetResponse("/user");
                ServerAnswerRecievedUser data = JsonConvert.DeserializeObject<ServerAnswerRecievedUser>(await ClientBackend.GetResponse("/user"));
                return data;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }

        }
        public static async Task<string> GetResponse(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(new Uri(api_domain, url));
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new UnknownException();
            }

        }
        //use : przy zapisie do pliku  DO DEBUGU: WYWOLUJ JAKO 1 
        public static void StroreCredentials(String login, String password)
        {
            credentialCache.Add(
                     api_domain,
                     "Basic",
                      new System.Net.NetworkCredential("kowalski", "qwe"));//username,password -\\

            handler.Credentials = credentialCache;

            client = new HttpClient(handler);
        }


        public static void RemoveCredentials()
        {
            handler.Credentials = credentialCache;
            client = new HttpClient(handler);
        }


        ~ClientBackend()
        {
            handler.Dispose();
            client.Dispose();
        }

    }
}
