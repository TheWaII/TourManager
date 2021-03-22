using System;
using System.Windows.Input;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class AddTourCommand : ICommand
    {

        private readonly AddTourViewModel _viewModel;

        public AddTourCommand(AddTourViewModel viewModel)
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
            return _viewModel.CanUpdate;
        }
        

        public void Execute(object parameter)
        {
            _viewModel.SaveChanges();
        }

    }
}
