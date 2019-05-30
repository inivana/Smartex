using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Smartex.Model
{
    public class Event : INotifyPropertyChanged
    {
        private string _title;
        private string _startDate;
        private string _desc;
        private List<Post> _posts;

        //konstruktor parametryczny na potrzebe debugowania 
        public Event(string _title, string _startDate)
        {
            this._title = _title;
            this._startDate = _startDate;
        }

        public Event()
        {
            //musi byc konstruktor domyslny bo mamy na górze parametryczny
        }
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        [JsonProperty(PropertyName = "start_date")]
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        [JsonProperty(PropertyName = "desc")]
        public string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value; 
                OnPropertyChanged("Desc");
            }
        }
        

        [JsonProperty(PropertyName = "creation_date")]
        public string CreationDate { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int UserID { get; set; }

        public List<Post> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged("Posts");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
