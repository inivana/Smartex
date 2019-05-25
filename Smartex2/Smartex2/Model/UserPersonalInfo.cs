using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Smartex.Model
{
    public class UserPersonalInfo : INotifyPropertyChanged
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _login;
        private string _password;
        private string _university;
        private string _faculty;
        private string _fieldOfStudy;

        [JsonProperty(PropertyName = "id")]
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("ID");
            }
        }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChange("FirstName");
            }
        }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChange("LastName");
            }
        }

        [JsonProperty(PropertyName = "login")]
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChange("Login");
            }
        }

        [JsonProperty(PropertyName = "university")]
        public string University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChange("University");
            }
        }

        [JsonProperty(PropertyName = "faculty")]
        public string Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChange("Faculty");
            }
        }

        [JsonProperty(PropertyName = "field_of_study")]
        public string FieldOfStudy
        {
            get { return _fieldOfStudy; }
            set
            {
                _fieldOfStudy = value;
                OnPropertyChange("FieldOfStudy");
            }
        }

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChange("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
