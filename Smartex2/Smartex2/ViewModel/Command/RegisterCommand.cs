using Smartex.Model;
using System;
using System.Windows.Input;

namespace Smartex.ViewModel.Command
{
    public class RegisterCommand : ICommand
    {
        public RegistrationVM ViewModel { get; set; }

        public RegisterCommand(RegistrationVM viewModel)
        {
            this.ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            var user = (UserPersonalInfo)parameter;

            if (user == null)
            {
                return false;
            }

            return true;
            //coś nie działa
            var ifFirstName = string.IsNullOrEmpty(user.FirstName);
            var ifLastName = string.IsNullOrEmpty(user.LastName);
            var ifLogin = string.IsNullOrEmpty(user.Login);
            var ifPassword = string.IsNullOrEmpty(user.Password);
            var ifUniversity = string.IsNullOrEmpty(user.University);
            var ifFaculty = string.IsNullOrEmpty(user.Faculty);
            var ifField = string.IsNullOrEmpty(user.FieldOfStudy);


            if (ifFirstName || ifLastName || ifLogin || ifPassword || ifUniversity || ifFaculty || ifField)
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
            this.ViewModel.RegisterUser();
        }
    }
}
