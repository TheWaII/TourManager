using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TourPlanner.Annotations;
using TourPlanner.BL;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Commands;
using TourPlanner.Model.Tour;

namespace TourPlanner.ViewModels
{
    public class DockPanelViewModel : INotifyPropertyChanged
    {
        public readonly ObservableCollection<TourData> TourCollection = new();


        public DockPanelViewModel()
        {
            CloseCommand = new RelayCommand(_ => { ApplicationExit(); });
        }

        public RelayCommand CloseCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

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

            new RemoveMaps(TourCollection);

            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}