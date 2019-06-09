using Smartex.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                //_userName = User.GetPersonalInfo(this.SelectedEvent.UserID).FirstName + User.GetPersonalInfo(this.SelectedEvent.UserID).LastName;

            }
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                //SetPostsCollectionAsync(SelectedEvent.ID);
                this.PostsCollection.Add(new Post()
                {
                    Content = "kontent"
                });
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

        public EventViewModel()
        {
            this.PostsCollection = new ObservableCollection<Post>();
            this.SelectedEvent = new Event();
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
