using Smartex.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Smartex.ViewModel.Command;
using Smartex.Model;
using System.Globalization;
using Smartex.View;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    class NewEventViewModel : INotifyPropertyChanged
    {
        private Event _event;

        private string _title;

        private string _desc;

        public string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                EventProperty = new Event()
                {
                    Title = this.Title,
                    Desc = this.Desc,
                    StartDate = this.Date.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                    CreationDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                OnPropertyChanged("Desc");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                EventProperty = new Event()
                {
                    Title = this.Title,
                    Desc = this.Desc,
                    StartDate = this.Date.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                    CreationDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                OnPropertyChanged("Title");
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                EventProperty = new Event()
                {
                    Title = this.Title,
                    Desc = this.Desc,
                    StartDate = this.Date.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                    CreationDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                OnPropertyChanged("Date");
            }
        }


        public Event EventProperty
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged("EventProperty");
            }
        }

        public AddEventCommand AddEventCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public NewEventViewModel()
        {
            this.EventProperty = new Event();
            this.AddEventCommand = new AddEventCommand(this);
        }

        public async void AddEvent()
        {
            EventProperty.UserID = 1;
            await User.AddEvent(EventProperty);
            await App.Current.MainPage.DisplayAlert("Dodano wydarzenie", "Udało się dodać wydarzenie", "OK");
            (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new HomePage()));
        }
    }
}
