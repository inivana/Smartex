using System;
using Smartex.Annotations;
using Smartex.Exception;
using Smartex.Model;
using Smartex.ViewModel.Command;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public RegisterCommand RegisterCommand { get; set; }
        private UserPersonalInfo _user;
        private string _firstName;
        private string _lastName;
        private string _login;
        private string _password;
        private string _university;
        private string _faculty;
        private string _fieldOfStudy;

        //prop
        public UserPersonalInfo UserPersonalInfoProp
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("UserPersonalInfoProp");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("FirstName");
            }
        }


        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("LastName");
            }
        }



        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("Password");
            }
        }


        public string University
        {
            get { return _university; }
            set
            {
                _university = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("University");
            }
        }



        public string Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("Faculty");
            }
        }



        public string FieldOfStudy
        {
            get { return _fieldOfStudy; }
            set
            {
                _fieldOfStudy = value;
                UserPersonalInfoProp = new UserPersonalInfo()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password,
                    University = this.University,
                    Faculty = this.Faculty,
                    FieldOfStudy = this.FieldOfStudy
                };
                OnPropertyChanged("FieldOfStudy");
            }
        }

        public RegistrationViewModel()
        {
            this._user = new UserPersonalInfo();
            this.RegisterCommand = new RegisterCommand(this);
        }
        public async void RegisterUser()
        {
            try
            {
                await User.RegisterUser(this.UserPersonalInfoProp);
                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE); //przejdź na stronę główną
            }
            catch (DataFormatException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
            catch (ArgumentNullException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");

            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
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
