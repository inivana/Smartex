using System;
using Smartex2.Model;
using System.Windows.Input;

namespace Smartex2.ViewModel.Command
{
    public class LoginCommand : ICommand
    {
        public MainVM ViewModel { get; set; }

        public LoginCommand(MainVM viewModel)
        {
            this.ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            /* TODO
             * walidacja z usera, przekazywanego jako parametr 
             */
            var user = (User)parameter;

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
            ViewModel.LoginUser();
        }
    }
}
