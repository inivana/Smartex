using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartex.Model;
using Smartex.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View.Functionalities
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubjectPage : ContentPage
	{
        public SubjectViewModel ViewModel { get; set; }
		public SubjectPage ()
		{
			InitializeComponent ();
		}

        public SubjectPage(Subject subject)
        {
			InitializeComponent ();
            ViewModel = new SubjectViewModel(subject);
            BindingContext = ViewModel;
        }
    }
}