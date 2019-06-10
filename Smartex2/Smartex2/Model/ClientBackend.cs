using Newtonsoft.Json;
using Smartex.Exception;
using Smartex.Server;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Smartex.Model
{
    /**
     * Klasa odpowiedzialna za zarządzanie danymi logowania użytkownika.
     * Odpowiada także za rejestracje i logowanie.
     */
    class ClientBackend
    {
        //adres serwera 
        public static System.Uri api_domain = new System.Uri("https://opclouden.pythonanywhere.com/");
        private static HttpClientHandler handler = new HttpClientHandler();
        private static HttpClient client = new HttpClient();
        private static System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();

       //ustawianie JSON'a
        static ClientBackend()
        {
            handler = new HttpClientHandler();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        /**
         * Zwraca informacje dotyczace konkretnego użytkownika
         * @return ServerAnswerRecievedUser - informacje dotyczace użytkownika + powodzenie/niepowodzenie
         */
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

        /**
         * @param url - adres pod ktory ma zostac wykonane żadanie GET
         * @return JSON
         */
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


        /**
         * Zapisuje dane logowania
        * @param login,hasło używkonika
        */
        public static void StroreCredentials(String login, String password)
        {
            credentialCache.Add(
                     api_domain,
                     "Basic",
                      new System.Net.NetworkCredential(login, password));

            handler.Credentials = credentialCache;

            client = new HttpClient(handler);
        }

        /**
         * Usuwa dane logowania
        */
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

        /**
         * Rejestracja użytkownika
         * @param UserPersonalInfo - wszystkie potrzebne dane do rejestracji
         */
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
            catch(Exception e)
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
