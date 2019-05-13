using Smartex2.Annotations;
using Smartex2.Model;
using Smartex2.ViewModel.Command;
using System.ComponentModel;
using Xamarin.Forms;

namespace Smartex2.ViewModel
{
    public class RegistrationVM : INotifyPropertyChanged
    {
        public RegisterCommand RegisterCommand { get; set; }
        private User _user;
        private string _firstName;
        private string _lastName;
        private string _login;
        private string _email;
        private string _password;
        private string _university;
        private string _faculty;
        private string _fieldOfStudy;

        //prop
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }


        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }



        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");

            }
        }


        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


        public string University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChanged("University");
            }
        }



        public string Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChanged("Faculty");
            }
        }



        public string FieldOfStudy
        {
            get { return _fieldOfStudy; }
            set
            {
                _fieldOfStudy = value;
                OnPropertyChanged("FieldOfStudy");
            }
        }

        public RegistrationVM()
        {
            this._user = new User();
            this.RegisterCommand = new RegisterCommand(this);
        }
        public async void RegisterUser()
        {
            bool canRegister = User.RegisterUser(User.FirstName, User.LastName, User.Login, User.Password,
                User.University, User.Faculty, User.FieldOfStudy);

            if (canRegister)
            {
                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE); //przejdź na stronę główną
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Rejestracja nie powiodła się", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
