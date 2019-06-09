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
        #region fields

        private string _title;
        private string _startDate;
        private ObservableCollection<Event> _events;
        private Event _selectedEvent;

        public GoToNewEventCommand GoToNewEventCommand { get; set; }

        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged("Events");
            }
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                if (_selectedEvent != null)
                {
                    (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new EventPage(this.SelectedEvent)));
                }
                OnPropertyChanged("SelectedEvent");
            }
        }

        #endregion

        #region ctor

        public HomeViewModel()
        {
            this.GoToNewEventCommand = new GoToNewEventCommand(this);
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

        public void Navigate()
        {
            (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new NewEventPage()));
        }

        #endregion

    }
}
