using System;
using System.Collections.Generic;
using System.Text;
using Smartex.View;
using Smartex.View.Functionalities;
using Smartex.ViewModel.Command;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class HomeVM
    {
        public GoToNewEventCommand GoToNewEventCommand { get; set; }

        public HomeVM()
        {
            this.GoToNewEventCommand = new GoToNewEventCommand(this);
        }

        public void Navigate()
        {
            (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new NewEventPage()));
        }
    }
}
