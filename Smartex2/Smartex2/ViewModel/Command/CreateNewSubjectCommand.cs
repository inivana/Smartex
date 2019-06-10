using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Smartex.ViewModel.Command
{
    public class CreateNewSubjectCommand : ICommand
    {
        public GradeBookViewModel ViewModel { get; set; }
        
        public CreateNewSubjectCommand(GradeBookViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.Navigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}
