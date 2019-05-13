using System;
using System.Collections.Generic;
using System.Text;
using Smartex2.View;
using Smartex2.View.Functionalities;
using Smartex2.ViewModel.Command;
using Xamarin.Forms;

namespace Smartex2.ViewModel
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
