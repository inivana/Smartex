using Smartex.Annotations;
using Smartex.Exception;
using Smartex.Model;
using Smartex.View;
using Smartex.ViewModel.Command;
using System;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region fields

        public bool CanLogin { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public GoToRegistrationPageCommand GoToRegistrationPageCommand { get; set; }

        private UserPersonalInfo _user;
        private string _login;
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.UserPersonalInfo = new UserPersonalInfo()
                {
                    Login = this.Login,
                    Password = this.Password
                };
                OnPropertyChanged("Password");
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                this.UserPersonalInfo = new UserPersonalInfo()
                {
                    Login = this.Login,
                    Password = this.Password
                };
                OnPropertyChanged("Login");
            }
        }

        public UserPersonalInfo UserPersonalInfo
        {
            get { return _user; }
            set
            {
                _user = value;
                if (UserPersonalInfo != null && LoginCommand != null)
                {
                    this.CanLogin = this.LoginCommand.CanExecute(UserPersonalInfo);
                }
                OnPropertyChanged("UserPersonalInfo");
            }
        }
        #endregion

        #region ctor

        public MainViewModel()
        {
            this.UserPersonalInfo = new UserPersonalInfo();
            this.LoginCommand = new LoginCommand(this);
            this.GoToRegistrationPageCommand = new GoToRegistrationPageCommand(this);
            this.CanLogin = false;
        }

        #endregion


        #region binding

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        #region commandMethods

        public async void LoginUser(UserPersonalInfo user)
        {
            try
            {
                //logowanie działa - kowalski/qwe, trzeba dlugo czekac wiec komentuję
                await ClientBackend.Login(UserPersonalInfo.Login, UserPersonalInfo.Password);
                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);

            }
            catch (LoginException ex)
            {
                App.DisplayException(ex);

            }
            catch (ArgumentNullException ex)
            {
                App.DisplayException(ex);

            }
            catch (HttpRequestException ex)
            {
                App.DisplayException(ex);

            }
            catch (System.Exception ex)
            {
                App.DisplayException(ex);
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

        #endregion

    }
}
