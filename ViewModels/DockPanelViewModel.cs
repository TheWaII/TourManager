using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Annotations;
using TourPlanner.BL;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Commands;
using TourPlanner.Model.Tour;
using TourPlanner.ViewModels.Tour;


namespace TourPlanner.ViewModels
{
    public class DockPanelViewModel : INotifyPropertyChanged
    { 
        public readonly ObservableCollection<TourData> TourCollection = new ObservableCollection<TourData>();
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
            var dbTourLogic = new TourLogic();
            foreach (var item in dbTourLogic.LoadTours())
            {
                var tourData = new TourData
                {
                    TourId = item.TourId,
                    TourName = item.TourName,
                    TourDescription = item.TourDescription,
                    TourSource = item.TourSource,
                    TourDestination = item.TourDestination,
                    TourDistance = item.TourDistance,
                    TourRoute = item.TourRoute
                };

                TourCollection.Add(tourData);
            }

            var removeMaps = new RemoveMaps(TourCollection);
            
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
