using System;
using System.Windows.Input;
using Smartex.Model;

namespace Smartex.ViewModel.Command
{
    public class UpdateUserCommand : ICommand
    {
        public ProfileViewModel ProfileViewModel { get; set; }

        public UpdateUserCommand(ProfileViewModel viewModel)
        {
            this.ProfileViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ProfileViewModel.UpdateUser((UserPersonalInfo)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
