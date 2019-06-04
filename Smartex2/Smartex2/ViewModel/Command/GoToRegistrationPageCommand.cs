using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Smartex.View;

namespace Smartex.ViewModel.Command
{
    public class GoToRegistrationPageCommand : ICommand
    {
        public MainViewModel ViewModel { get; set; }

        public GoToRegistrationPageCommand(MainViewModel viewModel)
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
