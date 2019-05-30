using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Smartex.Annotations;
using Smartex.Model;
using Smartex.View;
using Smartex.View.Functionalities;
using Smartex.ViewModel.Command;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class HomeVM : INotifyPropertyChanged
    {
        private string _title;
        private string _startDate;
        private List<Event> _events;

        public List<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;

            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public GoToNewEventCommand GoToNewEventCommand { get; set; }

        public HomeVM()
        {
            this.GoToNewEventCommand = new GoToNewEventCommand(this);
            //Events = new List<Event>();
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

        public void Navigate()
        {
            (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new NewEventPage()));
        }
    }
}
