using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Smartex.Model;

namespace Smartex.ViewModel.Command
{
    public class DeleteSubjectCommand : ICommand
    {
        public SubjectViewModel SubjectViewModel { get; set; }

        public DeleteSubjectCommand(SubjectViewModel viewModel)
        {
            this.SubjectViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            Subject sub = (Subject) parameter;
            if (sub == null) return false;
            return true;
        }

        public void Execute(object parameter)
        {
            this.SubjectViewModel.DeleteSubject();
        }

        public event EventHandler CanExecuteChanged;
    }
}
