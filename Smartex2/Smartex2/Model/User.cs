class User
{
    /**
    * Pobiera informacje na temat zalogowanego użytkownika
    * @return UserPersonalInfo 
    */
    public static async Task<UserPersonalInfo> GetPersonalInfo()
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            ServerAnswerRecievedUser recievedUser = JsonConvert.DeserializeObject<ServerAnswerRecievedUser>(await ClientBackend.GetResponse("/user"));

            if (recievedUser.Status.Equals("success"))
            {
                return recievedUser.User;
            }
            else
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
        }
        catch (TaskCanceledException)
        {
            throw;
        }
        catch (System.Exception)
        {
            throw new UnknownException();
        }
    }


    /**
    * Pobiera wydarzenia z serwera
    * @param userID - identyfikator zalogowanego użytkownika
    * @return ObservableCollection<Event> 
    */
    public static async Task<List<Event>> GetEvents(int userID)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            ServerAnswerRecievedEvents recievedEvent = JsonConvert.DeserializeObject<ServerAnswerRecievedEvents>
                  (await ClientBackend.GetResponse("/events/" + userID));

            if (recievedEvent.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                return recievedEvent.EventList;
            }
            else
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
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
    /**
    * Pobiera posty z konkretnego wydarzenia
    * @param eventID - identyfikator wydarzenia 
    * @return ObservableCollection<Post>
    */
    public static async Task<List<Post>> GetPosts(int eventID)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            ServerAnswerRecievedPosts recievedPosts = JsonConvert.DeserializeObject
                <ServerAnswerRecievedPosts>(await ClientBackend.GetResponse("/posts/" + eventID));

            if (recievedPosts.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                return recievedPosts.PostList;
            }
            else
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
        }
        catch (DataFormatException)
        {
            throw;
        }
        catch (System.Exception e)
        {
            throw new UnknownException();
        }
    }

    /**
   * Pobiera post
   * @param postID - identyfikator postu 
   * @return Post
   */
    public static async Task<Post> GetPost(int postID)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            ServerAnswerRecievedPost recievedPost = JsonConvert.DeserializeObject
                <ServerAnswerRecievedPost>(await ClientBackend.GetResponse("/posts/uniqe/"+postID));

            if (recievedPost.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                return recievedPost.Post;
            }
            else
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
        }
        catch (DataFormatException)
        {
            throw;
        }
        catch (System.Exception e)
        {
            throw new UnknownException();
        }
    }

    /**
    * Pobiera wydarzenie 
    * @param eventID - identyfikator wydarzenia 
    * @return Event
    */
    public static async Task<Event> GetEvent(int eventID)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            ServerAnswerRecievedEvent recievedEvent= JsonConvert.DeserializeObject
                <ServerAnswerRecievedEvent>(await ClientBackend.GetResponse("/events/uniqe/" + eventID));

            if (recievedEvent.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                return recievedEvent.Event_;
            }
            else
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
        }
        catch (DataFormatException)
        {
            throw;
        }
        catch (System.Exception e)
        {
            throw new UnknownException();
        }

    }

    /**
    * Wysyła posta 
    * @param Post 
    */
    public static async Task AddPost(Post post)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

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
        catch (InternetConnectionExcepion)
        {
            throw;
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
    /**
    * Wysyła wydarzenie
    * @param Event 
    */
    public static async Task AddEvent(Event event_)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

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
        catch (InternetConnectionExcepion)
        {
            throw;
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

    /**
    * Wysyła aktualizacje wydarzenia 
    * @param Event 
    */
    public static async Task UpdateEvent(Event event_)
    {
        try
        {

            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            string json = JsonConvert.SerializeObject(event_);

            var response = await ClientBackend.client.PutAsync((ClientBackend.api_domain + "/events/" + event_.ID)
                 , new StringContent(json, Encoding.UTF8, "application/json"));

            string responseContent = await response.Content.ReadAsStringAsync();

            ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

            if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
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
    /**
    * Wysyła żadanie usunięcia danego wydarzenia 
    * @param Event  -  wystarczy by zawierał tylko pole ID
    */
    public static async Task DeleteEvent(Event event_)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            var response = await ClientBackend.client.DeleteAsync(ClientBackend.api_domain + "/events/" + event_.ID);

            string responseContent = await response.Content.ReadAsStringAsync();

            ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);

            if (!serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
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
    /**
    * Wysyła żadanie aktualizacji danego postu 
    * @param Post  -  wystarczy by zawierał tylko pole ID
    */
    public static async Task UpdatePost(Post post)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

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
        catch (InternetConnectionExcepion)
        {
            throw;
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

    /**
    * Wysyła żadanie usunięcia danego postu 
    * @param Post  -  wystarczy by zawierał tylko pole ID
    */
    public static async Task DeletePost(Post post)
    {
        try
        {
            if (!ClientBackend.IsConnection()) throw new InternetConnectionExcepion();

            var response = await ClientBackend.client.DeleteAsync(ClientBackend.api_domain + "/posts/" + post.ID);

            string responseContent = await response.Content.ReadAsStringAsync();

            ServerFeedback serverFeedback = JsonConvert.DeserializeObject<ServerFeedback>(responseContent);


            if (serverFeedback.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                throw new DataFormatException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
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
}
