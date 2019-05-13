using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartex2.Model;
using Smartex2.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
    {

        private HomeVM _viewModel;
		public HomePage ()
		{
            /*User user = new User();
            user.Events.Add(new Event()
            {
                CreationDateTime = new DateTime(2019, 5, 13, 19, 9, 0),
                Description = "PUM projekt",
                Id = 1,
                StartDateTime = new DateTime(2019, 5, 14, 8, 30, 0),
                Title = "Prezentacja PUM",
                UserId = 1
            });

            user.Events.Add(new Event()
            {
                CreationDateTime = new DateTime(2019, 5, 13, 19, 9, 0),
                Description = "Kolos z wfu super pingpong",
                Id = 1,
                StartDateTime = new DateTime(2019, 5, 14, 12, 00, 0),
                Title = "Kolokwium WF",
                UserId = 1
            });

            eventListView.ItemsSource = user.Events;*/

            InitializeComponent ();
            this._viewModel = new HomeVM();
            BindingContext = this._viewModel;

            
        }
	}
}