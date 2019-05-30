using Smartex.Annotations;
using Smartex.Model;
using Smartex.ViewModel.Command;
using System.ComponentModel;
using Smartex.View;
using Xamarin.Forms;
using Smartex.Exception;

namespace Smartex.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public LoginCommand LoginCommand { get; set; }
        public GoToRegistrationPageCommand GoToRegistrationPageCommand { get; set; }

        private UserPersonalInfo _user;
        private string _login;
        private string _password;

        //properties
        public UserPersonalInfo UserPersonalInfoProp
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("UserPersonalInfoProp");
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
                UserPersonalInfoProp = new UserPersonalInfo()
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
            this.UserPersonalInfoProp = new UserPersonalInfo();
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
            try
            {
                //logowanie działa - kowalski/qwe, trzeba dlugo czekac wiec komentuję
                await User.Login(UserPersonalInfoProp.Login, UserPersonalInfoProp.Password);
                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);

            }
            catch (LoginException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");

            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
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
