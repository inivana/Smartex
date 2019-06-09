using System;
using Smartex.Model;
using System.Windows.Input;

namespace Smartex.ViewModel.Command
{
    public class LoginCommand : ICommand
    {
        private MainViewModel _viewModel;

        public LoginCommand(MainViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            var user = (UserPersonalInfo)parameter;

            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            this._viewModel.LoginUser((UserPersonalInfo) parameter);
        }
    }
}
