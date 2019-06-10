using Smartex.Model;
using System;
using System.Windows.Input;

namespace Smartex.ViewModel.Command
{
    public class AddGradeCommand : ICommand
    {
        public SubjectViewModel SubjectViewModel { get; set; }

        public AddGradeCommand(SubjectViewModel viewModel)
        {
            this.SubjectViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            Grade grade = (Grade)parameter;
            if (grade == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(grade.Description) && string.IsNullOrEmpty(grade.IntGrade.ToString()))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            this.SubjectViewModel.AddGrade((Grade)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
