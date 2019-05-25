using Newtonsoft.Json;
using Smartex.Exception;
using Smartex.Server;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smartex.Model
{
    class User
    {
        private static List<Event> events = new List<Event>();
        private static Dictionary<int, Event> eventMap = new Dictionary<int, Event>();

        // use: sieganie po zasoby usera( W TRAKCIE UZYTKOWANIA APPKI)
        public static UserPersonalInfo GetPersonalInfo()
        {
            try
            {
                //wczytaj dane z pliku i ustaw credential REST.StoreCredential(string login,password);
                //jesli danych nie ma throw new SessionExpiredException() -> ponowne zalogowanie
                ServerAnswerRecievedUser recievedUser = ClientBackend.CurrentUser().Result;
                if (recievedUser.Status.Equals("success"))
                {
                    return recievedUser.User;
                }
                else throw new DataFormatException();
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (SessionExpiredException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }
        }

        // use: sieganie po eventy usera
        public static async Task<List<Event>> GetEvents(int userID)
        {

            try
            {
                ServerAnswerRecievedEvents recievedEvent = JsonConvert.DeserializeObject<ServerAnswerRecievedEvents>
                      (await ClientBackend.GetResponse("/events/" + userID));

                if (recievedEvent.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    eventMap.Clear();
                    events = recievedEvent.EventList;
                    if (events == null) throw new UnknownException();
                    foreach (Event element in events) eventMap.Add(element.ID, element);
                    return events;
                }
                else throw new DataFormatException();
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }


        }

        // use: sieganie po posty w konkretnym eventcie
        public static async Task<List<Post>> GetPosts(int eventID)
        {
            try
            {
                ServerAnswerRecievedPosts sarp = JsonConvert.DeserializeObject
                    <ServerAnswerRecievedPosts>(await ClientBackend.GetResponse("/posts/" + eventID));

                if (sarp.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    eventMap[eventID].Posts = sarp.PostList;
                    if (eventMap[eventID].Posts == null) throw new System.Exception();
                    return eventMap[eventID].Posts;
                }
                else throw new DataFormatException();
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }


        }


        public static async Task AddPost(Post post)
        {
            try
            {
                string json = JsonConvert.SerializeObject(post);

                var response = await ClientBackend.client.PostAsync((ClientBackend.api_domain + "/posts")
                     , new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

                if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    throw new DataFormatException();
                }

            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new System.Exception();
            }

        }

        public static async Task AddEvent(Event event_)
        {
            try
            {
                string json = JsonConvert.SerializeObject(event_);

                var response = await ClientBackend.client.PostAsync((ClientBackend.api_domain + "/events")
                     , new StringContent(json, Encoding.UTF8, "application/json"));

                string responseContent = await response.Content.ReadAsStringAsync();


                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

                if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    throw new DataFormatException();
                }
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }

        }
        public static async Task UpdateEvent(Event event_)
        {
            try
            {
                string json = JsonConvert.SerializeObject(event_);

                var response = await ClientBackend.client.PutAsync((ClientBackend.api_domain + "/events/" + event_.ID)
                     , new StringContent(json, Encoding.UTF8, "application/json"));

                string responseContent = await response.Content.ReadAsStringAsync();

                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

                if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                    throw new DataFormatException();

            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }
        }
        public static async Task DeleteEvent(Event event_)
        {
            try
            {
                var response = await ClientBackend.client.DeleteAsync(ClientBackend.api_domain + "/events/" + event_.ID);

                string responseContent = await response.Content.ReadAsStringAsync();

                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

                if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                    throw new DataFormatException();
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }

        }

        public static async Task UpdatePost(Post post)
        {
            try
            {
                string json = JsonConvert.SerializeObject(post);

                var response = await ClientBackend.client.PutAsync((ClientBackend.api_domain + "/posts/" + post.ID)
                     , new StringContent(json, Encoding.UTF8, "application/json"));

                string responseContent = await response.Content.ReadAsStringAsync();


                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

                if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    throw new DataFormatException();
                }
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }

        }
        public static async Task DeletePost(Post post)
        {
            try
            {
                var response = await ClientBackend.client.DeleteAsync(ClientBackend.api_domain + "/posts/" + post.ID);

                string responseContent = await response.Content.ReadAsStringAsync();

                ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);


                if (serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                    throw new DataFormatException();
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }
        }


        public static Event GetEvent(int eventID)
        {
            if (!eventMap.ContainsKey(eventID)) throw new System.Exception(); //do zmiany
            return eventMap[eventID];
        }

        public static Post GetPost(int postID, int eventID)
        {
            if (!eventMap.ContainsKey(eventID)) throw new System.Exception(); //do zmiany
            return eventMap[eventID].Posts.Find(post => post.ID == postID); // ArgumentNullException
        }

        // use: tylko po przycisku zaloguj
        public static async Task Login(String username, String password)
        {
            try
            {
                ClientBackend.StroreCredentials(username, password);
                String json = await ClientBackend.GetResponse("/user");

                ServerAnswerRecievedUser userData = JsonConvert.DeserializeObject<ServerAnswerRecievedUser>(await ClientBackend.GetResponse("/user"));

                if (userData.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    //ClientBackend.StroreCredentials(username, password);
                    //zapisz dane do pliku jesli sie udalo zalogowac
                    //jesli nie exception  throw new LoginException()
                }
                else
                    throw new LoginException();
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
                    ClientBackend.StroreCredentials(userPersonalInfo.Login, userPersonalInfo.Password);
                }
                else
                    throw new DataFormatException();
            }
            catch (DataFormatException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw new UnknownException();
            }

        }
        //use: usuwa z pliku login,haslo
        public static void Logout()
        {
            ClientBackend.RemoveCredentials();
            //try catch throw new UnExpectedException();
        }

    }
}