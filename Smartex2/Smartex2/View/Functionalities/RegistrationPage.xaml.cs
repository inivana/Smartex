using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartex.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
    {
        private RegistrationViewModel _viewModel;
		public RegistrationPage ()
		{
			InitializeComponent ();
            this._viewModel = new RegistrationViewModel();
            BindingContext = _viewModel;
        }
	}
}