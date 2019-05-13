using System.Collections.Generic;
using System.ComponentModel;

namespace Smartex2.Model
{
    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private string _password;
        private string _university;
        private string _faculty;
        private string _fieldOfStudy;
        public List<Event> Events { get; set; }

        public User()
        {
            /* TODO
             * Uzupełnić listę Events o eventy, w których uczestniczy user
             */
        }

        //potrzebne do bindowania
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //methods
        public bool LoginUser(string login, string password)
        {
            /* TODO
             * walidacja usera, sprawdzanie z restem
             * itede w/e
             * ma zwracać true, jeżeli się da zalogować a false jak nie
             */
            return true;
        }
        public bool RegisterUser(string firstname, string lastname, string login, string password, string university,
            string faculty, string fieldOfStudy)
        {
            /* TODO
             * sprawdzanie, czy da sie dodac usera
             * np: sprawdzanie, czy nie ma takiego maila w bazie
             */
            return true;
        }

        //properties
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChange("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChange("LastName");
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChange("Login");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChange("Email");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChange("Password");
            }
        }
        public string University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChange("University");
            }
        }
        public string Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChange("Faculty");
            }
        }
        public string FieldOfStudy
        {
            get { return _fieldOfStudy; }
            set
            {
                _fieldOfStudy = value;
                OnPropertyChange("FieldOfStudy");
            }
        }
    }
}
