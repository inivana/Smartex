using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartex2.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
    {
        private RegistrationVM _viewModel;
		public RegistrationPage ()
		{
			InitializeComponent ();
            this._viewModel = new RegistrationVM();
            BindingContext = _viewModel;
        }
	}
}