using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartex.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        //RootPage RootPage { get => Application.Current.MainPage as RootPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage ()
		{
			InitializeComponent ();
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.HomePage, Title="Strona główna" },
                new HomeMenuItem {Id = MenuItemType.Profile, Title="Profil" },
                new HomeMenuItem {Id = MenuItemType.GradeBook, Title="Dzienniczek" },
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Wyloguj" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                RootPage curr = App.Current.MainPage as RootPage;
                await curr.NavigateFromMenu(id);
            };
        }
	}
}