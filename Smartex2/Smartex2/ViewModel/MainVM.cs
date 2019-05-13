using Smartex2.Annotations;
using Smartex2.Model;
using Smartex2.ViewModel.Command;
using System.ComponentModel;
using Smartex2.View;
using Xamarin.Forms;

namespace Smartex2.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        /* TODO
         * po dodaniu klasy User zrobić tu properties na email, password i usera
         * zbindować dane w xamlu
         * zrobić nawigację do strony głównej
         */
        public LoginCommand LoginCommand { get; set; }
        public GoToRegistrationPageCommand GoToRegistrationPageCommand { get; set; }

        private User _user;
        private string _login;
        private string _password;

        //properties
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                User = new User()
                {
                    Login = this.Login,
                    Password = this.Password
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
                User = new User()
                {
                    Login = this.Login,
                    Password = this.Password
                };
                OnPropertyChanged("Password");
            }
        }

        //konstruktor
        public MainVM()
        {
            this.User = new User();
            this.LoginCommand = new LoginCommand(this);
            this.GoToRegistrationPageCommand = new GoToRegistrationPageCommand(this);
        }

        //inotify, bindowanie
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //metody
        public async void LoginUser()
        {
            bool canLogin = User.LoginUser(User.Login, User.Password);
            if (canLogin)
            {
                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Login lub hasło są nieprawidłowe", "OK");
            }
        }

        public async void Navigate()
        {
            if (App.Current.MainPage.GetType() == typeof(MainPage))
            {
                App.Current.MainPage = new NavigationPage(App.Current.MainPage as MainPage);
                await App.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
            }
            else
            {
                (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new RegistrationPage()));
            }
        }
    }
}
