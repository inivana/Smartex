using Smartex.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View.Functionalities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEventPage : ContentPage
    {
        private NewEventViewModel _viewModel;

        public NewEventPage()
        {
            InitializeComponent();
            this._viewModel = new NewEventViewModel();
            BindingContext = this._viewModel;

        }
    }
}