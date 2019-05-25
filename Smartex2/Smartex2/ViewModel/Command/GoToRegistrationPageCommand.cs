using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Smartex.View;

namespace Smartex.ViewModel.Command
{
    public class GoToRegistrationPageCommand : ICommand
    {
        public MainVM ViewModel { get; set; }

        public GoToRegistrationPageCommand(MainVM viewModel)
        {
            this.ViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Navigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}
