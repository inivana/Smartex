using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartex.Model;
using Smartex.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GradeBookPage : ContentPage
    {
        private GradeBookViewModel viewModel;
		public GradeBookPage ()
		{
			InitializeComponent ();
            this.viewModel = new GradeBookViewModel();
            BindingContext = this.viewModel;
        }

        public GradeBookPage(string subjectName)
        {
            /*
             * Po dodaniu przedmiotu w NewSubjectPage, przechodzimy na te strone przez
             * ten konstruktor
             */
            this.viewModel = new GradeBookViewModel();
            bool added = GradeBook.Add(new Subject()
            {
                Name = subjectName
            });
            if (added)
            {
               this.DisplayAlert("Udało się", "Przedmiot został dodany", "OK");
            }
            else
            {
                this.DisplayAlert("Niepowodzenie", "Nazwa przedmiotu nie może być pusta", "OK");
            }
            this.viewModel.Subjects = GradeBook.GetSubjects();

            InitializeComponent();
            BindingContext = this.viewModel;
            
        }
	}
}