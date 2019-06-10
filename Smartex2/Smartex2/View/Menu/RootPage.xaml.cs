using Smartex.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public RootPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.HomePage, (NavigationPage)Detail);
        }

        public async void NavigateFromPage(NavigationPage newPage)
        {
            Detail = newPage;

            if (Device.RuntimePlatform == Device.Android)
            {
                await Task.Delay(100);
            }

            IsPresented = false;
        }
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.HomePage:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new ProfilePage()));
                        break;
                    case (int)MenuItemType.Calendar:
                        MenuPages.Add(id, new NavigationPage(new CalendarPage()));
                        break;
                    case (int)MenuItemType.GradeBook:
                        MenuPages.Add(id, new NavigationPage(new GradeBookPage()));
                        break;
                    case (int)MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new MainPage()));
                        //MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_LOGIN_PAGE);
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                if (id == 5)
                {
                    NavigationPage.SetHasBackButton(newPage, false);
                    NavigationPage.SetHasNavigationBar(newPage, false);
                }
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(100);
                }

                IsPresented = false;
            }
        }
    }
}