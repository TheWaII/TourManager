using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Annotations;
using TourPlanner.Commands;


namespace TourPlanner.ViewModels
{
    public class DockPanelViewModel : INotifyPropertyChanged
    {
        public RelayCommand CloseCommand { get; }
        

        public DockPanelViewModel()
        {
            CloseCommand = new RelayCommand((_) =>
            {
                ApplicationExit();
            });
        }

        public void ApplicationExit()
        {
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
