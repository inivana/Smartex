using Smartex.ViewModel;
using System.Collections.ObjectModel;
using Smartex.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View.Functionalities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventPage : CarouselPage
    {
        private EventViewModel viewModel;

        public EventPage(Event selectedEvent)
        {
            InitializeComponent();
            this.viewModel = new EventViewModel(selectedEvent);
            BindingContext = this.viewModel;
            PostsListView.ItemsSource = this.viewModel.PostsCollection;
        }
    }
}
