using System;
using Smartex.Annotations;
using Smartex.Model;
using Smartex.View;
using Smartex.ViewModel.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Smartex.ViewModel
{
    public class SubjectViewModel : INotifyPropertyChanged
    {
        #region fields

        private ObservableCollection<Grade> _grades;
        private Grade _grade;
        private string _description;
        private int _intGrade;


        public AddGradeCommand AddGradeCommand { get; set; }
        public DeleteSubjectCommand DeleteSubjectCommand { get; set; }

        private Subject _subject;

        public Subject Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                this.Grades = value.Grades;
                OnPropertyChanged("Subject");
            }
        }

        private float _average;

        public float Average
        {
            get { return _average; }
            set
            {
                if (float.IsNaN(value)) Average = 0;
                _average = value;
                OnPropertyChanged("Average");
            }
        }

        private float CountAverage()
        {
            int sum = 0;
            foreach (var grade in Grades)
            {
                sum += grade.IntGrade;
            }

            float avg = (float)sum / (float)Grades.Count;
            return avg;
        }

        public ObservableCollection<Grade> Grades
        {
            get { return _grades; }
            set
            {
                _grades = value;
                OnPropertyChanged("Grades");
            }
        }

        public int IntGrade
        {
            get { return _intGrade; }
            set
            {
                _intGrade = value;
                this.Grade = new Grade()
                {
                    Description = this.Description,
                    IntGrade = this.IntGrade
                };
                OnPropertyChanged("IntGrade");
            }
        }


        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                this.Grade = new Grade()
                {
                    Description = this.Description,
                    IntGrade = this.IntGrade
                };
                OnPropertyChanged("Description");
            }
        }

        private Grade _selectedGrade;

        public Grade SelectedGrade
        {
            get { return _selectedGrade; }
            set
            {
                _selectedGrade = value;
                if (SelectedGrade != null)
                {
                    this.DeleteGrade();
                }
                OnPropertyChanged("SelectedGrade");
            }
        }


        public Grade Grade
        {
            get { return _grade; }
            set
            {
                _grade = value;
                OnPropertyChanged("Grade");
            }
        }


        #region ctor

        public SubjectViewModel(Subject subject)
        {
            this.AddGradeCommand = new AddGradeCommand(this);
            this.DeleteSubjectCommand = new DeleteSubjectCommand(this);
            this.Subject = subject;
            this.Grades = GradeBook.GetGrades(this.Subject.Id);
            this.Average = CountAverage();
        }

        #endregion

        #endregion

        #region binding

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion

        public void AddGrade(Grade grade)
        {
            grade.SubjectId = this.Subject.Id;
            if (GradeBook.AddGrade(grade))
            {
                App.Current.MainPage.DisplayAlert("Powodzenie", "Udało się dodać ocenę", "OK");
                this.Refresh();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Niepowodzenie", "Nie udało się dodać oceny", "OK");
            }
        }

        private void Refresh()
        {
            this.Grades = GradeBook.GetGrades(this.Subject.Id);
            this.Average = CountAverage();
        }

        private async void DeleteGrade()
        {
            var result = await GradeBook.DeleteGrade(SelectedGrade);
            if (result)
            {
                this.Refresh();
                await App.Current.MainPage.DisplayAlert("Powodzenie", "Udało się usunąć ocenę", "OK");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Nie udało się usunąć oceny", "OK");
            }
        }

        public async void DeleteSubject()
        {
            //delete suject and grades
            var result = await GradeBook.DeleteSubject(this.Subject);
            if (result)
            {
                this.Refresh();
                await App.Current.MainPage.DisplayAlert("Powodzenie", "Udało się usunąć przedmiot", "OK");
                (App.Current.MainPage as RootPage).NavigateFromPage(new NavigationPage(new GradeBookPage()));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Nie udało się usunąć przedmiotu", "OK");
            }
        }
    }
}
