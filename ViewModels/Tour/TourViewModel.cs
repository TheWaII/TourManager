using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.BL.Route;
using TourPlanner.Commands;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;
using System.Reflection;
using TourPlanner.BL.ImportExport;
using TourPlanner.BL.Reporting;

namespace TourPlanner.ViewModels.Tour
{
    public class TourViewModel : INotifyPropertyChanged, IValueConverter
    {



        #region Constructor

        public TourViewModel()
        {
            AddTourToggle = new RelayCommand(o => ToggleAddTour());

            ValidateCommand = new RelayCommand(o => Validate(), o => CanUpdateValidate);

            ValidateCommandEdit = new RelayCommand(o => ValidateEdit());

            AddTourCommand = new RelayCommand(o => SaveChanges(), o => CanUpdate);

            WindowLoaded = new RelayCommand(o => RefreshTourList());

            EditTourCommand = new RelayCommand(o => UpdateChanges());

            EditTourToggle = new RelayCommand(o => ToggleEditTour());

            DeleteTourCommand = new RelayCommand(o => DeleteTour());

            DeleteLogCommand = new RelayCommand(o => DeleteLog());

            SaveTourReport = new RelayCommand(o => CreateTourReport());

            //WindowExit = new RelayCommand(o => ExitWindow());

            RefreshTourCommand = new RelayCommand(o => RefreshTourList());

            RefreshLogCommand = new RelayCommand(o => RefreshLogList());
        }

        public RelayCommand RefreshTourCommand { get; set; }

        #endregion


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value != null
                    ? BitmapFrame.Create(new Uri(value.ToString(), UriKind.Relative))
                    : BitmapFrame.Create(new Uri(@"..\..\img\route\error.jpeg", UriKind.Relative));
            }
            catch (Exception)
            {
                return BitmapFrame.Create(new Uri(@"..\..\img\route\error.jpeg", UriKind.Relative));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public void RefreshTourList()
        {

            _tourCollection.Clear();

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

                _tourCollection.Add(tourData);
            }
        }

        public void SaveChanges()
        {
            var dbTourLogic = new TourLogic();

            var tour = new TourData
            {
                TourName = Name,
                TourSource = Source,
                TourDestination = Destination,
                TourDistance = Distance,
                TourDescription = Description
            };

            dbTourLogic.InsertTours(tour);

            Name = string.Empty;
            Source = string.Empty;
            Destination = string.Empty;
            Distance = 0;
            Description = string.Empty;
            ProgressBarColor = "Gray";
            RefreshTourList();
        }

        public void ToggleAddTour()
        {
            IsCheckedAdd = IsCheckedAdd == false;

            if (IsCheckedAdd)
            {
                AddTourVisibility = "Visible";
                RouteDescriptionVisibility = "Hidden";
            }
            else
            {
                AddTourVisibility = "Hidden";
                RouteDescriptionVisibility = "Visible";
            }
        }

        public void ToggleEditTour()
        {
            IsCheckedEdit = IsCheckedEdit == false;

            if (IsCheckedEdit)
            {
                EditTourVisibility = "Visible";
                RouteDescriptionVisibility = "Hidden";
            }
            else
            {
                EditTourVisibility = "Hidden";
                RouteDescriptionVisibility = "Visible";
            }
        }

        public void Validate()
        {
            var mapDistanceLogic = new MapDistanceLogic();


            Distance = System.Convert.ToDouble(mapDistanceLogic.DistanceInKm(Source, Destination));


            ProgressBarColor = Distance != 0 ? "Green" : "Red";

            mapDistanceLogic.SaveImage(Source, Destination);
        }

        public void ValidateEdit()
        {
            var mapDistanceLogic = new MapDistanceLogic();


            SelectedTourData.TourDistance = System.Convert.ToDouble(
                mapDistanceLogic.DistanceInKm(SelectedTourData.TourSource, SelectedTourData.TourDestination));

            ProgressBarColor = SelectedTourData.TourDistance != 0 ? "Green" : "Red";

            mapDistanceLogic.SaveImage(SelectedTourData.TourSource, SelectedTourData.TourDestination);
        }

        public void UpdateChanges()
        {
            var dbDatabaseLogic = new TourLogic();
            var tour = new TourData
            {
                TourId = SelectedTourData.TourId,
                TourName = SelectedTourData.TourName,
                TourSource = SelectedTourData.TourSource,
                TourDestination = SelectedTourData.TourDestination,
                TourDistance = SelectedTourData.TourDistance,
                TourDescription = SelectedTourData.TourDescription,
                TourRoute = SelectedTourData.TourSource + "_" + SelectedTourData.TourDestination
            };

            dbDatabaseLogic.UpdateTours(tour);

            Name = string.Empty;
            Source = string.Empty;
            Destination = string.Empty;
            Distance = 0;
            Description = string.Empty;
            ProgressBarColor = "Gray";
            RefreshTourList();
        }

        public void DeleteTour()
        {
            var dbDatabaseLogic = new TourLogic();

            dbDatabaseLogic.DeleteTours(SelectedTourData);

            RefreshTourList();
        }

        //private void ExitWindow()
        //{
        //    new RemoveMaps(TourCollection);
        //}


        #region Properties

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _source;

        public string Source
        {
            get => _source;
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        private string _destination;

        public string Destination
        {
            get => _destination;
            set
            {
                _destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }


        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private double _distance;

        public double Distance
        {
            get => _distance;

            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private bool _isCheckedAdd;

        public bool IsCheckedAdd
        {
            get => _isCheckedAdd;
            set
            {
                _isCheckedAdd = value;
                OnPropertyChanged(nameof(IsCheckedAdd));
            }
        }

        private bool _isCheckedEdit;

        public bool IsCheckedEdit
        {
            get => _isCheckedEdit;
            set
            {
                _isCheckedEdit = value;
                OnPropertyChanged(nameof(IsCheckedEdit));
            }
        }

        private string _routeDescriptionVisibility = "Visible";

        public string RouteDescriptionVisibility
        {
            get => _routeDescriptionVisibility;
            set
            {
                _routeDescriptionVisibility = value;
                OnPropertyChanged(nameof(RouteDescriptionVisibility));
            }
        }

        private string _addTourVisibility = "Hidden";

        public string AddTourVisibility
        {
            get => _addTourVisibility;
            set
            {
                _addTourVisibility = value;
                OnPropertyChanged(nameof(AddTourVisibility));
            }
        }

        private string _editTourVisibility = "Hidden";

        public string EditTourVisibility
        {
            get => _editTourVisibility;
            set
            {
                _editTourVisibility = value;
                OnPropertyChanged(nameof(EditTourVisibility));
            }
        }


        private string _progressbarColor = "Gray";

        public string ProgressBarColor
        {
            get => _progressbarColor;
            set
            {
                _progressbarColor = value;
                OnPropertyChanged(nameof(ProgressBarColor));
            }
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;

                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(MyFilteredItems));
            }
        }

        public IEnumerable<TourData> MyFilteredItems
        {
            get
            {
                //return SearchText == null ? TourCollection : TourCollection.Where(x => x.TourName.Contains(SearchText));
                var tl = new TourLogic();
                return tl.MyFilteredItems(SearchText, TourCollection);
            }
        }

        private ObservableCollection<TourData> _tourCollection = new();

        public ObservableCollection<TourData> TourCollection
        {
            get => _tourCollection;
            set
            {
                _tourCollection = value;
                OnPropertyChanged(nameof(TourCollection));
            }
        }


        private ObservableCollection<LogData> _logCollection = new();

        public ObservableCollection<LogData> LogCollection
        {
            get => _logCollection;
            set
            {
                _logCollection = value;
                OnPropertyChanged(nameof(LogCollection));
            }
        }


        private ObservableCollection<BikeData> _bikeCollection = new();

        public ObservableCollection<BikeData> BikeCollection
        {
            get => _bikeCollection;
            set
            {
                _bikeCollection = value;
                OnPropertyChanged(nameof(BikeCollection));
            }
        }


        private ObservableCollection<CarData> _carCollection = new();

        public ObservableCollection<CarData> CarCollection
        {
            get => _carCollection;
            set
            {
                _carCollection = value;
                OnPropertyChanged(nameof(CarCollection));
            }
        }


        private TourData _selectedTourData;

        public TourData SelectedTourData
        {
            get => _selectedTourData;
            set
            {
                _selectedTourData = value;
                OnPropertyChanged(nameof(SelectedTourData));

                RefreshLogList();
            }
        }


        private LogData _selectedDataForLog;

        public LogData SelectedDataForLog
        {
            get => _selectedDataForLog;
            set
            {
                _selectedDataForLog = value;
                OnPropertyChanged(nameof(SelectedDataForLog));

                RefreshBikeLog();
            }
        }

        public RelayCommand EditTourCommand { get; }

        public RelayCommand EditTourToggle { get; }

        public RelayCommand AddTourCommand { get; }

        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand DeleteLogCommand { get; }

        public RelayCommand ValidateCommand { get; }

        public RelayCommand ValidateCommandEdit { get; }

        public RelayCommand AddTourToggle { get; }

        public RelayCommand SaveTourReport { get; }
        public RelayCommand RefreshLogCommand { get; }

        public ICommand WindowLoaded { get; set; }

        public ICommand WindowExit { get; set; }

        #endregion

        #region CanUpdate

        public bool CanUpdate =>
            !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) &&
            !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination) &&
            Distance != 0;


        public bool CanUpdateValidate => !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination);

        #endregion

        #region Logs

        public void RefreshLogList()
        {
            _logCollection.Clear();
            _bikeCollection.Clear();
            _carCollection.Clear();
            var dbLogLogic = new LogLogic();


            if (SelectedTourData == null) return;
            foreach (var item in dbLogLogic.LoadLogs())
            {
                var logData = new LogData
                {
                    LogId = item.LogId,
                    TourId = item.TourId,
                    LogName = item.LogName,
                    LogDate = item.LogDate,
                    LogDistance = item.LogDistance,
                    LogTotalTime = item.LogTotalTime,
                    LogRating = item.LogRating,
                    LogType = item.LogType,
                    LogReport = item.LogReport
                };

                logData.BikeCar = logData.LogType switch
                {
                    1 => "Bike",
                    2 => "Car",
                    _ => "Unspecified"
                };

                if (logData.TourId == SelectedTourData.TourId) _logCollection.Add(logData);
            }
        }

        public void RefreshBikeLog()
        {
            if (SelectedDataForLog == null) return;
            _bikeCollection.Clear();
            _carCollection.Clear();
            switch (SelectedDataForLog.LogType)
            {
                case 1:
                    {
                        var dbBikeLogic = new BikeLogic();
                        foreach (var bikeItem in dbBikeLogic.LoadBikes())
                        {
                            var bikeData = new BikeData
                            {
                                LogId = bikeItem.LogId,
                                AvgHeartRate = bikeItem.AvgHeartRate,
                                AvgSpeed = bikeItem.AvgSpeed,
                                CaloriesBurnt = bikeItem.CaloriesBurnt,
                                LowestHeartRate = bikeItem.LowestHeartRate,
                                PeakHeartRate = bikeItem.PeakHeartRate
                            };

                            if (bikeData.LogId == SelectedDataForLog.LogId)
                                _bikeCollection.Add(bikeData);
                        }

                        break;
                    }
                case 2:
                    {
                        var dbCarLogic = new CarLogic();
                        foreach (var carItem in dbCarLogic.LoadCars())
                        {
                            var carData = new CarData
                            {
                                LogId = carItem.LogId,
                                AvgSpeed = carItem.AvgSpeed,
                                CaughtSpeeding = carItem.CaughtSpeeding,
                                FuelCost = carItem.FuelCost,
                                FuelUsed = carItem.FuelUsed,
                                MaxSpeed = carItem.MaxSpeed
                            };

                            if (carData.LogId == SelectedDataForLog.LogId)
                                _carCollection.Add(carData);
                        }

                        break;
                    }
            }
        }

        public void DeleteLog()
        {
            var dbLogLogic = new LogLogic();

            if (SelectedDataForLog == null) return;
            switch (SelectedDataForLog.LogType)
            {
                case 1:
                    var dbBikeLogic = new BikeLogic();
                    dbBikeLogic.DeleteLogs(SelectedDataForLog.LogId);
                    break;
                case 2:
                    var dbCarLogic = new CarLogic();
                    dbCarLogic.DeleteLogs(SelectedDataForLog.LogId);
                    break;
            }

            dbLogLogic.DeleteLogs(SelectedDataForLog);

            RefreshLogList();
        }

        public void CreateTourReport()
        {
            var rep = new Reporting();
            rep.CreatePdf(SelectedTourData.TourId);
        }

        #endregion

        #region ProperteyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}