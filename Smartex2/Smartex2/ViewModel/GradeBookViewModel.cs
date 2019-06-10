using Smartex.Annotations;
using Smartex.Model;
using Smartex.View;
using Smartex.View.Functionalities;
using Smartex.ViewModel.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class GradeBookViewModel : INotifyPropertyChanged
    {
        #region fields

        private ObservableCollection<Subject> _subjects;
        private Subject _selectedSubject;

        public CreateNewSubjectCommand CreateNewSubjectCommand { get; set; }
        public ObservableCollection<Subject> Subjects
        {
            get { return _subjects; }
            set
            {
                _subjects = GradeBook.GetSubjects();
                OnPropertyChanged("Subjects");
            }
        }

        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                if (SelectedSubject != null )
                {
                    (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new SubjectPage(this.SelectedSubject)));
                }
                OnPropertyChanged("SelectedSubject");
            }
        }

        #endregion  

        #region ctor

        public GradeBookViewModel()
        {
            this.CreateNewSubjectCommand = new CreateNewSubjectCommand(this);
            this.Subjects = GradeBook.GetSubjects();
        }

        #endregion 

        #region binding
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Navigate()
        {
            (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new NewSubjectPage()));
        }
    }
}
