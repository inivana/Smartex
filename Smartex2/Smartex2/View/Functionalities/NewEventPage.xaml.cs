using Smartex.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View.Functionalities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEventPage : ContentPage
    {
        private NewEventVM _viewModel;

        public NewEventPage()
        {
            InitializeComponent();
            this._viewModel = new NewEventVM();
            BindingContext = this._viewModel;

        }
    }
}