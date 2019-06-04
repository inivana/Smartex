using Smartex.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smartex.ViewModel
{
    class EventViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Post> _postsCollection;
        private Event _selectedEvent;

        public EventViewModel()
        {
            this.PostsCollection = new ObservableCollection<Post>();
            this.SelectedEvent = new Event();
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

        private async void SetPostsCollectionAsync(int eventId)
        {
            this.PostsCollection = await User.GetPosts(eventId);
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
