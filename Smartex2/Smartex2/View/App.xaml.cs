using System;
using Smartex.Model;
using Smartex.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Smartex
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static string EVENT_LAUNCH_LOGIN_PAGE = "EVENT_LAUNCH_LOGIN_PAGE";
        public static string EVENT_LAUNCH_MAIN_PAGE = "EVENT_LAUNCH_MAIN_PAGE";
        public static UserPersonalInfo CurrentUser { get; set; }
        public App()
        {
            InitializeComponent();
            CurrentUser = new UserPersonalInfo();

            MainPage = new MainPage();

            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_LOGIN_PAGE, SetLoginPageAsRootPage);
            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
        }

        public App(string databaseLocation)
        {
            InitializeComponent();
            CurrentUser = new UserPersonalInfo();


            MainPage = new MainPage();

            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_LOGIN_PAGE, SetLoginPageAsRootPage);
            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static async void DisplayException(System.Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
        }
        private void SetLoginPageAsRootPage(object sender)
        {
            MainPage = new MainPage();
        }

        private void SetMainPageAsRootPage(object sender)
        {
            MainPage = new RootPage();
        }
    }
}
