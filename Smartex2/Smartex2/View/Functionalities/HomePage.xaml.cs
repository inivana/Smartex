using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Smartex.Exception;
using Smartex.Model;
using Smartex.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
    {

        private HomeVM _viewModel;
		public HomePage ()
        {
            //var events = await Task.FromResult<List<Event>>(User.GetEvents(1));
            //eventListView.ItemsSource = events

            InitializeComponent ();
            this._viewModel = new HomeVM();
            BindingContext = this._viewModel;
            GetListEvents(1);
        }

        private async void GetListEvents(int id)
        {
            try
            {
                //TODO 
                //ladne wyswietlanie tych eventow - max 3, format daty odpowiedni + jakies kolorki
                this._viewModel.Events = await User.GetEvents(id);
                eventListView.ItemsSource = _viewModel.Events.Take(3);
            }
            catch (ArgumentNullException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
            catch (DataFormatException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
            catch (NullReferenceException ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");

            }
        }
    }
}