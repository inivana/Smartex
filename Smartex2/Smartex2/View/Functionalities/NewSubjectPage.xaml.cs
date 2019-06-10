using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartex.View.Functionalities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewSubjectPage : ContentPage
    {
        public NewSubjectPage()
        {
            InitializeComponent();
        }

        private void AddSubjectButton_OnClicked(object sender, EventArgs e)
        {
            if (this.NameEntry.Text != null)
            {
                (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new GradeBookPage(this.NameEntry.Text)));
            }
        }
    }
}