using System;
using System.Security.Policy;
using System.Windows.Input;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands.DockPanel.File
{
    internal class CloseCommand : ICommand
    {

        private readonly DockPanelViewModel _viewModel;

        public CloseCommand(DockPanelViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _viewModel.ApplicationExit();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
