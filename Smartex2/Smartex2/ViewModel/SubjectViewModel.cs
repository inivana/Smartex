using Smartex.Annotations;
using Smartex.Model;
using Smartex.ViewModel.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                    GradeBook.DeleteGrade(SelectedGrade);
                    this.Refresh();
                }
                this.Grades = GradeBook.GetGrades(this.Subject.Id);
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
    }
}
