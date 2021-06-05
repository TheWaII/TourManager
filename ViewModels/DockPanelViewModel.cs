using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.BL.ImportExport;
using TourPlanner.Commands;
using TourPlanner.Model.Tour;
using TourPlanner.Properties;

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

            HelpTourCommand = new RelayCommand(_ => { HelpTour(); });

            HelpLogCommand = new RelayCommand(_ => { HelpLog(); });

            HelpImportExportCommand = new RelayCommand(_ => { HelpImportExport(); });

            HelpReportAndStatisticCommand = new RelayCommand(_ => { HelpReportAndStatistic(); });
        }

        public RelayCommand HelpReportAndStatisticCommand { get; }

        public RelayCommand HelpImportExportCommand { get; }


        public RelayCommand CloseCommand { get; }

        public RelayCommand ExportCommand { get; }

        public RelayCommand ImportCommand { get; }

        public RelayCommand HelpTourCommand { get; }

        public RelayCommand HelpLogCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        private static void HelpTour()
        {
            MessageBox.Show(
                "Insert: Click on the Plus sign. Then, chose a tour name with source and destination which you need to validate. If the bar is red, the tour validation was not successful. . The distance of the tour is calculated automatically. Chose a description then click add." +
                "\n\nDelete: Select a tour, then click the minus sign to delete it. All the corresponding logs will also be deleted." +
                "\n\nEdit: Click the edit button, then change the values as you want. Keep in mind that if you change the source and destination, you will need to validate the locations again." +
                "\n\nRefresh: If you think that your tour isn't in the list, click refresh. If you still cant find it the log doesn't exist.",
                "Tours", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static void HelpLog()
        {
            MessageBox.Show(
                "Insert: Click on the Plus sign. Then fill in all the fields with your data. Keep in mind, the time you should write the total time as such => TOTAL HOURS : TOTAL MINUTES (<= 59). You will need to choose to which tour this log should be added to. For this, check the tour list where you can also see the tour number. " +
                " You can either chose an unspecified tour, a bike or car tour. For bike and car tour, fill in the required fields." +
                "\n\nDelete: Select a log, then click the minus sign to delete it. " +
                "\n\nEdit: Click the edit button, select the log you want to change." +
                "\n\nRefresh: If you think that your log isn't in the list, click refresh. If you still cant find it, the log doesn't exist.",
                "Logs", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static void HelpImportExport()
        {
            MessageBox.Show(
                "Export: If you click export, all the tours that are currently in the list, will be imported as a json data. Select a destination and save it." +
                "\n\nImport: The exported json data can be imported by selecting the exported file.",
                "Import and Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static void HelpReportAndStatistic()
        {
            MessageBox.Show(
                "Report: To create a report, select a tour of which you want to create a report of and click save which is on the bottom left." +
                "\n\nStatistic: After saving a report of a tour you will be asked if you also want to save a statistic of the logs in that tour.",
                "Report and Statistic", MessageBoxButton.OK, MessageBoxImage.Information);
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

            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        }

        public void Import()
        {
            var import = new Import();
            import.ImportJson();
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