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
	public partial class ProfilePage : ContentPage
	{
        public ProfileViewModel ProfileViewModel { get; set; }
		public ProfilePage ()
		{
			InitializeComponent ();
            this.ProfileViewModel = new ProfileViewModel();
            BindingContext = this.ProfileViewModel;
        }
	}
}