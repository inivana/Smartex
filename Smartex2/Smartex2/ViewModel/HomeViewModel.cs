using Smartex.Annotations;
using Smartex.Model;
using Smartex.View;
using Smartex.View.Functionalities;
using Smartex.ViewModel.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        private string _title;
        private string _startDate;
        private ObservableCollection<Event> _events;

        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged("Events");
            }
        }
        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                if (_selectedEvent != null)
                {
                    //TODO nawigacja!!!
                    (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new EventPage(this.SelectedEvent)));
                }
                OnPropertyChanged("SelectedEvent");
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

        public HomeViewModel()
        {
            this.GoToNewEventCommand = new GoToNewEventCommand(this);
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
