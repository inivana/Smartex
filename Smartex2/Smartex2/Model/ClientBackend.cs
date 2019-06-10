using Newtonsoft.Json;
using Smartex.Exception;
using Smartex.Server;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smartex.Model
{
    class ClientBackend
    {
        public static System.Uri api_domain = new System.Uri("https://opclouden.pythonanywhere.com/");
        private static HttpClientHandler handler = new HttpClientHandler();
        public static HttpClient client = new HttpClient();
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
                      new System.Net.NetworkCredential(login, password));

            handler.Credentials = credentialCache;

            client = new HttpClient(handler);
        }


        public static void RemoveCredentials()
        {
            credentialCache = new System.Net.CredentialCache();
            handler.Credentials = credentialCache;
            client = new HttpClient(handler);
        }

        public static async Task Login(String username, String password)
        {
            try
            {
                StroreCredentials(username, password);
                String json = await ClientBackend.GetResponse("/user");

                ServerAnswerRecievedUser userData = JsonConvert.DeserializeObject<ServerAnswerRecievedUser>(await ClientBackend.GetResponse("/user"));

                if (!userData.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    RemoveCredentials();
                    throw new LoginException();
                }
            }
            catch (LoginException)
            {
                throw new LoginException();
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }
        }


        public static async Task RegisterUser(UserPersonalInfo userPersonalInfo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(userPersonalInfo);

                var response = await ClientBackend.client.PostAsync((ClientBackend.api_domain + "/user"),
                    new StringContent(json, Encoding.UTF8, "application/json"));

                string responseContent = await response.Content.ReadAsStringAsync();

                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);
                if (serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    StroreCredentials(userPersonalInfo.Login, userPersonalInfo.Password);
                }
                else
                {
                    throw new DataFormatException();
                }
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }
        }
       
        public static void Logout()
        {
            try
            {
                RemoveCredentials();
            }
            catch(System.Exception e)
            {
                throw new UnknownException();
            }
        }

        ~ClientBackend()
        {
            handler.Dispose();
            client.Dispose();
        }

    }
}
