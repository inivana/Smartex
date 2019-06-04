using Smartex.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Smartex.ViewModel.Command
{
    class AddEventCommand : ICommand
    {
        private NewEventViewModel viewModel;
        public AddEventCommand(NewEventViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
            Event ev = (Event) parameter;
            if (ev == null) return false;
            if (string.IsNullOrEmpty(ev.Title) || string.IsNullOrEmpty(ev.Desc) || string.IsNullOrEmpty(ev.StartDate))
            {
                /*
                 * TODO można ewentualnie dodać sprawdzanie formatowania daty tutaj a nie tylko null
                 * or empty
                 */
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            this.viewModel.AddEvent();
        }
    }
}
