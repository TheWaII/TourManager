using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class ValidateCommand : ICommand
    {
        private readonly AddTourViewModel _viewModel;

        public ValidateCommand(AddTourViewModel viewModel)
        {
            _viewModel = viewModel;
        }


        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanUpdateValidate;
        }

        public void Execute(object parameter)
        {
            _viewModel.Validate();
        }

    }
}
