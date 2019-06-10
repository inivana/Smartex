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
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Smartex.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
    {

        private HomeViewModel _viewModel;
		public HomePage ()
        {
            InitializeComponent ();
            this._viewModel = new HomeViewModel();
            BindingContext = this._viewModel;
            GetListEvents(1); //TODO zmienić to ID na ID obecnego usera 
        }

        private async void GetListEvents(int id)
        {
            try
            {
                //TODO kolorki, odpowiedni format daty, może opisy kolumn? po kliknięciu w  event wyświetlały się szczegóły
                //ladne wyswietlanie tych eventow - max 3, format daty odpowiedni + jakies kolorki
                this._viewModel.Events = await User.GetEvents(id);
                eventListView.ItemsSource = _viewModel.Events.Where(e => e.StartDate != null && DateTime.Parse(e.StartDate) > DateTime.Now).OrderBy(e => e.StartDate).Take(3);
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