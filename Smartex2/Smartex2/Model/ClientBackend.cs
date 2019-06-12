class ClientBackend
{
    //adres serwera 
    public static System.Uri api_domain = new System.Uri("https://opclouden.pythonanywhere.com/");
    private static HttpClientHandler handler = new HttpClientHandler();
    public static HttpClient client = new HttpClient();
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
     * @param url - adres pod ktory ma zostac wykonane żadanie GET
     * @return JSON
     */
    internal static async Task<string> GetResponse(string url)
    {
        try
        {
            if (!IsConnection()) throw new InternetConnectionExcepion();

            HttpResponseMessage response = await client.GetAsync(new Uri(api_domain, url));

            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
        catch (InternetConnectionExcepion)
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
        catch (NullReferenceException)
        {
            throw;
        }
        catch (System.Exception e)
        {
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
            if (!IsConnection()) throw new InternetConnectionExcepion();

            StroreCredentials(username, password);
            String json = await ClientBackend.GetResponse("/user");

            ServerAnswerRecievedUser userData = JsonConvert.DeserializeObject<ServerAnswerRecievedUser>(await ClientBackend.GetResponse("/user"));

            if (!userData.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                RemoveCredentials();
                throw new LoginException();
            }
        }
        catch (InternetConnectionExcepion)
        {
            throw;
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
            if (!IsConnection()) throw new InternetConnectionExcepion();

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
        catch (InternetConnectionExcepion)
        {
            throw;
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
        catch (System.Exception e)
        {
            throw new UnknownException();
        }
    }


    /**
    * Pobiera informacje na temat konkretnego użytkownika
    * @param userID - unikalny identyfikator użytkownika
    * @return ServerAnswerRecievedUser 
    */
    public static async Task<ServerAnswerRecievedUser> GetUser(int userID)
    {
        try
        {
            String json = await ClientBackend.GetResponse("/user");
            ServerAnswerRecievedUser data = JsonConvert.DeserializeObject<ServerAnswerRecievedUser>(await ClientBackend.GetResponse("/user/" + userID));
            return data;
        }
        catch (System.Exception)
        {
            throw new UnknownException();
        }

    }

    ~ClientBackend()
    {
        handler.Dispose();
        client.Dispose();
    }


    public static bool IsConnection()
    {
        try
        {
            Ping myPing = new Ping();
            String host = "google.com";
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            return (reply.Status == IPStatus.Success);
        }
        catch (Exception)
        {
            throw new InternetConnectionExcepion();
        }
    }

}
