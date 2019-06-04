using System;
using System.Windows.Input;

namespace Smartex.ViewModel.Command
{
    public class GoToNewEventCommand : ICommand
    {
        private HomeViewModel _viewModel;

        public GoToNewEventCommand(HomeViewModel viewModel)
        {
            this._viewModel = viewModel;

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._viewModel.Navigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}
