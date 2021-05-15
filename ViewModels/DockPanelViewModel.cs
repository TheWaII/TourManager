using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Windows;
using TourPlanner.Annotations;
using TourPlanner.BL;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.BL.ImportExport;
using TourPlanner.Commands;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;
using TourPlanner.ViewModels.Tour;
using LogData = Catel.Logging.LogData;

namespace TourPlanner.ViewModels
{
    public class DockPanelViewModel : INotifyPropertyChanged
    {
        public readonly ObservableCollection<TourData> TourCollection = new();


        public DockPanelViewModel()
        {
            CloseCommand = new RelayCommand(_ => { ApplicationExit(); });

            ImportCommand = new RelayCommand(_ => { Import(); });

            ExportCommand = new RelayCommand(_ => { Export(); });
        }

        public RelayCommand CloseCommand { get; }

        public RelayCommand ExportCommand { get; }

        public RelayCommand ImportCommand { get; }


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

        public void Import()
        {
            var import = new Import();
            import.ImportJson();
            var x = new TourViewModel();

            

        }

        public void Export()
        {
            var dbTourLogic = new TourLogic();
            var dbLogLogic = new LogLogic();
            var dbBikeLogic = new BikeLogic();
            var dbCarLogic = new CarLogic();

            var export = new Export();
            export.CreateJson(dbTourLogic.LoadTours(), dbLogLogic.LoadLogs(), dbBikeLogic.LoadBikes(),
                dbCarLogic.LoadCars());
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}