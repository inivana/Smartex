using System;
using Smartex.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Smartex.Exception;

namespace Smartex.ViewModel
{
    class EventViewModel : INotifyPropertyChanged
    {
        #region fields
        private ObservableCollection<Post> _postsCollection;
        private Event _selectedEvent;
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = User.GetPersonalInfo().Result.FirstName + User.GetPersonalInfo().Result.LastName;
            }
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }

        public ObservableCollection<Post> PostsCollection
        {
            get { return _postsCollection; }
            set
            {
                _postsCollection = value;
                OnPropertyChanged("PostsCollection");
            }
        }
        #endregion

        #region ctor

        public EventViewModel(Event selectedEvent)
        {
            this.SelectedEvent = selectedEvent;
            this.PostsCollection = new ObservableCollection<Post>();
            try
            {
                //TODO NIE DZIAŁA
                //this.PostsCollection = User.GetPosts(SelectedEvent.ID).Result; 
                this.PostsCollection.Add(new Post()
                {
                    Content = "testowy post 1",
                    CreationDate = DateTime.Now.ToString(),
                    EventID = this.SelectedEvent.ID,
                    UserID = 1
                });
                this.PostsCollection.Add(new Post()
                {
                    Content = "testowy post 2",
                    CreationDate = DateTime.Now.ToString(),
                    EventID = this.SelectedEvent.ID,
                    UserID = 1
                });
                //this.UserName = ClientBackend.GetUser(1).Result.User.FirstName + ClientBackend.GetUser(1).Result.User.LastName;

            }
            catch (ArgumentNullException ex)
            {
                App.DisplayException(ex);
            }
            catch (HttpRequestException ex)
            {
                App.DisplayException(ex);
            }
            catch (NullReferenceException ex)
            {
                App.DisplayException(ex);
            }
            catch (DataFormatException ex)
            {
                App.DisplayException(ex);
            }
            catch (System.Exception ex)
            {
                App.DisplayException(ex);

            }
        }

        #endregion

        #region privateMethods
        private async void SetPostsCollectionAsync(int eventId)
        {
            this.PostsCollection = await User.GetPosts(eventId);
        }

        #endregion

        #region binding

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
