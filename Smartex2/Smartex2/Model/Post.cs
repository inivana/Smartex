using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smartex.Model
{
    public class Post : INotifyPropertyChanged
    {

        private int _id;
        private int _userId;
        private int _eventId;
        private string _content;
        private string _creationDate;

        [JsonProperty(PropertyName = "creation_date")]
        public string CreationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        [JsonProperty(PropertyName = "content")]
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        [JsonProperty(PropertyName = "event_id")]
        public int EventID
        {
            get { return _eventId; }
            set
            {
                _eventId = value;
                OnPropertyChanged("EventID");
            }
        }

        [JsonProperty(PropertyName = "user_id")]
        public int UserID
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserID");
            }
        }


        [JsonProperty(PropertyName = "id")]
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
