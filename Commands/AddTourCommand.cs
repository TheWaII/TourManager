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

        /*
         * private Action<object> _execute;
           private Predicate<object> _canExecute;
           
           public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
           {
           _execute = execute ?? throw new ArgumentNullException(nameof(execute));
           _canExecute = canExecute;
           }
           
           public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
           public void Execute(object parameter) => _execute.Invoke(parameter);
           
           public event EventHandler CanExecuteChanged
           {
           add { CommandManager.RequerySuggested += value; }
           remove { CommandManager.RequerySuggested -= value; }
           }
         *
         */
    }
}
