using Smartex.Annotations;
using Smartex.Model;
using Smartex.View;
using Smartex.ViewModel.Command;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using Smartex.Exception;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    class NewEventViewModel : INotifyPropertyChanged
    {
        #region fields

        private Event _event;
        private string _title;
        private string _desc;
        private DateTime _date;

        public AddEventCommand AddEventCommand { get; set; }
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

        #region ctor

        public NewEventViewModel()
        {
            this.EventProperty = new Event();
            this.AddEventCommand = new AddEventCommand(this);
        }

        #endregion

        #region commandMethods
        public async void AddEvent()
        {
            EventProperty.UserID = App.CurrentUser.ID;
            try
            {
                await User.AddEvent(EventProperty);
            }
            catch (ArgumentNullException ex)
            {
                App.DisplayException(ex);
            }
            catch (HttpRequestException ex )
            {
                App.DisplayException(ex);
            }
            catch (NullReferenceException ex)
            {
                App.DisplayException(ex); 
            }
            catch (DataFormatException ex)
            {
                App.DisplayException(ex);
            }
            catch (System.Exception ex)
            {
                App.DisplayException(ex);

            }
            await App.Current.MainPage.DisplayAlert("Dodano wydarzenie", "Udało się dodać wydarzenie", "OK");
            (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new HomePage()));
        }
        #endregion


    }
}
