using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Catel.Collections;
using TourPlanner.Annotations;
using TourPlanner.BL.Database.Log;
using TourPlanner.Commands;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.ViewModels.Log
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BikeData> _bikeCollection = new();

        private BikeData _bikeEditCollection = new();


        private string _bikeVisibility = "Hidden";

        private ObservableCollection<CarData> _carCollection = new();

        private CarData _carEditCollection = new();

        private string _carVisibility = "Hidden";

        private string _editBikeVisibility = "Hidden";

        private string _editCarVisibility = "Hidden";


        private ObservableCollection<LogData> _logCollection = new();

        private LogData _selectedLogData;


        public LogViewModel()
        {
            AddLogToggle = new RelayCommand(o => ToggleAddLog());
            UnspecifiedRadioButton = new RelayCommand(o => ToggleShowUnspecified());
            BikeRadioButton = new RelayCommand(o => ToggleShowBike());
            CarRadioButton = new RelayCommand(o => ToggleShowCar());
            AddLog = new RelayCommand(o => SaveChangesLog(), o => CanAddLog);

            EditLogToggle = new RelayCommand(o => ToggleEditLog());

            EditLog = new RelayCommand(o => UpdateChangesLog(), o => CanEditLog);

        }

        public RelayCommand AddLogToggle { get; }

        public RelayCommand EditLogToggle { get; }
        public RelayCommand UnspecifiedRadioButton { get; }
        public RelayCommand BikeRadioButton { get; }
        public RelayCommand CarRadioButton { get; }
        public RelayCommand AddLog { get; }

        public ObservableCollection<LogData> LogCollection
        {
            get => _logCollection;
            set
            {
                _logCollection = value;
                OnPropertyChanged(nameof(LogCollection));
            }
        }

        public ObservableCollection<BikeData> BikeCollection
        {
            get => _bikeCollection;
            set
            {
                _bikeCollection = value;
                OnPropertyChanged(nameof(BikeCollection));
            }
        }

        public BikeData BikeEditCollection
        {
            get => _bikeEditCollection;
            set
            {
                _bikeEditCollection = value;
                OnPropertyChanged(nameof(BikeEditCollection));
            }
        }

        public ObservableCollection<CarData> CarCollection
        {
            get => _carCollection;
            set
            {
                _carCollection = value;
                OnPropertyChanged(nameof(CarCollection));
            }
        }


        public CarData CarEditCollection
        {
            get => _carEditCollection;
            set
            {
                _carEditCollection = value;
                OnPropertyChanged(nameof(CarEditCollection));
            }
        }

        public string EditBikeVisibility
        {
            get => _editBikeVisibility;
            set
            {
                _editBikeVisibility = value;
                OnPropertyChanged(nameof(EditBikeVisibility));
            }
        }

        public string EditCarVisibility
        {
            get => _editCarVisibility;
            set
            {
                _editCarVisibility = value;
                OnPropertyChanged(nameof(EditCarVisibility));
            }
        }

        public LogData SelectedLogData
        {
            get => _selectedLogData;
            set
            {
                _selectedLogData = value;
                OnPropertyChanged(nameof(SelectedLogData));

                if (SelectedLogData == null) return;
                switch (SelectedLogData.LogType)
                {
                    case 1:
                        EditBikeVisibility = "Visible";
                        EditCarVisibility = "Hidden";
                        var bikeQuery = BikeCollection.Where(b => b.LogId == SelectedLogData.LogId);
                        bikeQuery.ForEach(x => BikeEditCollection = x);
                        break;
                    case 2:
                        EditCarVisibility = "Visible";
                        EditBikeVisibility = "Hidden";
                        var carQuery = CarCollection.Where(c => c.LogId == SelectedLogData.LogId);
                        carQuery.ForEach(x => CarEditCollection = x);
                        break;
                    default:
                        EditCarVisibility = "Hidden";
                        EditBikeVisibility = "Hidden";
                        break;
                }
            }
        }

        public RelayCommand EditLog { get; set; }

        public bool IsCheckedEdit { get; set; }

        public bool CanAddLog
        {

            get
            {

                var regex = new Regex("[0-9]+:[0-5][0-9]$");
                return Id >= 0 && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Date) &&
                       !string.IsNullOrWhiteSpace(Time) && Rating != 0 && !string.IsNullOrWhiteSpace(Report) &&
                       regex.IsMatch(Time);
            }
        }

        public bool CanEditLog
        {
            get
            {
                var regexDate = new Regex("[0-9]+:[0-5][0-9]$");

                //var regexNumber = new Regex("(?<Number>[0-9])");

                //var regexDecimal = new Regex("^-?(?:\\d+|\\d{1,3}(?:,\\d{3})+)?(?:\\.\\d+)?$");

                return SelectedLogData != null &&
                       !string.IsNullOrWhiteSpace(SelectedLogData.LogName) &&
                       !string.IsNullOrWhiteSpace(SelectedLogData.LogDate) &&
                       !string.IsNullOrWhiteSpace(SelectedLogData.LogTotalTime) &&
                       !string.IsNullOrWhiteSpace(SelectedLogData.LogReport) &&
                       SelectedLogData.LogRating != 0 &&
                       regexDate.IsMatch(SelectedLogData.LogTotalTime);
            }
        }

        public string BikeVisibility
        {
            get => _bikeVisibility;
            set
            {
                _bikeVisibility = value;
                OnPropertyChanged(nameof(BikeVisibility));
            }
        }

        public string CarVisibility
        {
            get => _carVisibility;

            set
            {
                _carVisibility = value;
                OnPropertyChanged(nameof(CarVisibility));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ToggleEditLog()
        {
            IsCheckedEdit = IsCheckedEdit == false;

            if (IsCheckedEdit)
            {
                EditLogVisibility = "Visible";
                LogVisibility = "Hidden";
            }
            else
            {
                EditLogVisibility = "Hidden";
                LogVisibility = "Visible";
            }


            RefreshLogList();
        }


        private void ToggleAddLog()
        {
            IsCheckedAdd = IsCheckedAdd == false;

            if (IsCheckedAdd)
            {
                AddLogVisibility = "Visible";
                LogVisibility = "Hidden";
            }
            else
            {
                AddLogVisibility = "Hidden";
                LogVisibility = "Visible";
            }
        }


        public void ToggleShowBike()
        {
            IsCheckedBike = IsCheckedBike == false;


            if (IsCheckedBike)
            {
                BikeVisibility = "Visible";
                CarVisibility = "Hidden";
                IsCheckedCar = false;
            }
            else
            {
                BikeVisibility = "Hidden";
            }
        }

        public void ToggleShowCar()
        {
            IsCheckedCar = IsCheckedCar == false;

            if (IsCheckedCar)
            {
                CarVisibility = "Visible";
                BikeVisibility = "Hidden";
                IsCheckedBike = false;
            }
            else
            {
                CarVisibility = "Hidden";
            }
        }

        private void ToggleShowUnspecified()
        {
            IsCheckedUnspecified = IsCheckedUnspecified == false;

            if (!IsCheckedAdd) return;
            BikeVisibility = "Hidden";
            CarVisibility = "Hidden";
            IsCheckedCar = false;
            IsCheckedBike = false;
        }

        public void SaveChangesLog()
        {
            var dbLogLogic = new LogLogic();

            //convert date from mm/dd/yyyy to dd/mm/yyyy
            var date = Date.Split(' ')[0];

            var day = date.Split('/')[1];
            var month = date.Split('/')[0];
            var year = date.Split('/')[2];

            if (Enumerable.Range(1, 9).Contains(Convert.ToInt32(day)))
                day = "0" + day;
            if (Enumerable.Range(1, 9).Contains(Convert.ToInt32(month)))
                month = "0" + month;

            var newDate = day + "/" + month + "/" + year;

            var log = new LogData
            {
                TourId = Id,
                LogName = Name,
                LogDate = newDate,
                LogDistance = Distance,
                LogTotalTime = Time,
                LogRating = Rating,
                LogReport = Report
            };

            if (IsCheckedBike)
                log.LogType = 1;
            else if (IsCheckedCar)
                log.LogType = 2;
            else
                log.LogType = 0;

            dbLogLogic.InsertLog(log);

            //if bike tour => add to bike table
            if (IsCheckedBike)
            {
                var logData = dbLogLogic.LoadLogs().Where(x => x.LogName == log.LogName);

                var logId = logData.Select(i => i.LogId).FirstOrDefault();

                var dbBikeLogic = new BikeLogic();


                var bike = new BikeData
                {
                    LogId = logId,
                    PeakHeartRate = BikeLogViewModel.PeakHeartRate,
                    LowestHeartRate = BikeLogViewModel.LowestHeartRate,
                    AvgHeartRate = BikeLogViewModel.AverageHeartRate,
                    AvgSpeed = BikeLogViewModel.AverageSpeed,
                    CaloriesBurnt = BikeLogViewModel.CaloriesBurnt
                };

                dbBikeLogic.InsertLog(bike);
                BikeLogViewModel.PeakHeartRate = 0;
                BikeLogViewModel.LowestHeartRate = 0;
                BikeLogViewModel.AverageHeartRate = 0;
                BikeLogViewModel.AverageSpeed = 0;
                BikeLogViewModel.CaloriesBurnt = 0;
            }

            //if car tour => add to car table
            if (IsCheckedCar)
            {
                var logData = dbLogLogic.LoadLogs().Where(x => x.LogName == log.LogName);
                var logId = logData.Select(i => i.LogId).FirstOrDefault();
                var dbCarLogic = new CarLogic();

                var car = new CarData
                {
                    LogId = logId,
                    MaxSpeed = CarLogViewModel.MaxSpeed,
                    AvgSpeed = CarLogViewModel.AverageSpeed,
                    FuelUsed = CarLogViewModel.FuelUsed,
                    FuelCost = CarLogViewModel.FuelCost
                };

                if (CarLogViewModel.Yes)
                    car.CaughtSpeeding = true;
                else if (CarLogViewModel.No)
                    car.CaughtSpeeding = false;

                dbCarLogic.InsertLog(car);
                CarLogViewModel.MaxSpeed = 0;
                CarLogViewModel.AverageSpeed = 0;
                CarLogViewModel.FuelUsed = 0;
                CarLogViewModel.FuelCost = "0.0";
                CarLogViewModel.CaughtSpeeding = false;
            }


            Name = string.Empty;
            Date = string.Empty;
            Distance = 0;
            Time = string.Empty;
            Rating = 1;
            Report = string.Empty;
            Id = 0;
        }

        public void UpdateChangesLog()
        {
            var dbLogLogic = new LogLogic();

            var logData = new LogData
            {
                LogId = SelectedLogData.LogId,
                TourId = SelectedLogData.TourId,
                LogName = SelectedLogData.LogName,
                LogDate = SelectedLogData.LogDate,
                LogDistance = SelectedLogData.LogDistance,
                LogTotalTime = SelectedLogData.LogTotalTime,
                LogRating = SelectedLogData.LogRating,
                LogType = SelectedLogData.LogType,
                LogReport = SelectedLogData.LogReport
            };

            dbLogLogic.UpdateLogs(logData);


            switch (SelectedLogData.LogType)
            {
                case 1:
                    {
                        var dbBikeLogic = new BikeLogic();

                        var bikeData = new BikeData
                        {
                            LogId = SelectedLogData.LogId,
                            PeakHeartRate = BikeEditCollection.PeakHeartRate,
                            LowestHeartRate = BikeEditCollection.LowestHeartRate,
                            AvgHeartRate = BikeEditCollection.AvgHeartRate,
                            AvgSpeed = BikeEditCollection.AvgSpeed,
                            CaloriesBurnt = BikeEditCollection.CaloriesBurnt
                        };
                        dbBikeLogic.UpdateLogs(bikeData);
                        break;
                    }
                case 2:
                    {
                        var dbCarLogic = new CarLogic();

                        var carData = new CarData
                        {
                            LogId = SelectedLogData.LogId,
                            MaxSpeed = CarEditCollection.MaxSpeed,
                            AvgSpeed = CarEditCollection.AvgSpeed,
                            FuelUsed = CarEditCollection.FuelUsed,
                            FuelCost = CarEditCollection.FuelCost,
                            CaughtSpeeding = CarEditCollection.CaughtSpeeding
                        };
                        dbCarLogic.UpdateLogs(carData);
                        break;
                    }
            }

            BikeEditCollection = null;
            CarEditCollection = null;
            RefreshLogList();
        }

        public void RefreshLogList()
        {
            _logCollection.Clear();
            _bikeCollection.Clear();
            _carCollection.Clear();

            var dbLogLogic = new LogLogic();

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

                _logCollection.Add(logData);
            }

            var dbBikeLogic = new BikeLogic();

            foreach (var item in dbBikeLogic.LoadBikes())
            {
                var bikeData = new BikeData
                {
                    LogId = item.LogId,
                    PeakHeartRate = item.PeakHeartRate,
                    LowestHeartRate = item.LowestHeartRate,
                    AvgHeartRate = item.AvgHeartRate,
                    AvgSpeed = item.AvgSpeed,
                    CaloriesBurnt = item.CaloriesBurnt
                };

                _bikeCollection.Add(bikeData);
            }

            var dbCarLogic = new CarLogic();

            foreach (var item in dbCarLogic.LoadCars())
            {
                var carData = new CarData
                {
                    LogId = item.LogId,
                    MaxSpeed = item.MaxSpeed,
                    AvgSpeed = item.AvgSpeed,
                    FuelUsed = item.FuelUsed,
                    FuelCost = item.FuelCost,
                    CaughtSpeeding = item.CaughtSpeeding
                };

                _carCollection.Add(carData);
            }
        }

        //public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        //{
        //    var regex = new Regex("[^0-9]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region properties

        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

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

        private string _date;

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


        private int _distance;

        public int Distance
        {
            get => _distance;

            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }


        private string _time;

        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private int _rating = 1;

        public int Rating
        {
            get => _rating;

            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        private string _report;

        public string Report
        {
            get => _report;

            set
            {
                _report = value;
                OnPropertyChanged(nameof(Report));
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

        private string _logVisibility = "Visible";

        public string LogVisibility
        {
            get => _logVisibility;
            set
            {
                _logVisibility = value;
                OnPropertyChanged(nameof(LogVisibility));
            }
        }

        private string _addLogVisibility = "Hidden";

        public string AddLogVisibility
        {
            get => _addLogVisibility;
            set
            {
                _addLogVisibility = value;
                OnPropertyChanged(nameof(AddLogVisibility));
            }
        }

        private string _editLogVisibility = "Hidden";

        public string EditLogVisibility
        {
            get => _editLogVisibility;

            set
            {
                _editLogVisibility = value;
                OnPropertyChanged(nameof(EditLogVisibility));
            }
        }

        private bool _isCheckedUnspecified = true;

        public bool IsCheckedUnspecified
        {
            get => _isCheckedUnspecified;
            set
            {
                _isCheckedUnspecified = value;
                OnPropertyChanged(nameof(IsCheckedUnspecified));
            }
        }


        private bool _isCheckedCar;

        public bool IsCheckedCar
        {
            get => _isCheckedCar;
            set
            {
                _isCheckedCar = value;
                OnPropertyChanged(nameof(IsCheckedCar));
            }
        }

        private bool _isCheckedBike;

        public bool IsCheckedBike
        {
            get => _isCheckedBike;
            set
            {
                _isCheckedBike = value;
                OnPropertyChanged(nameof(IsCheckedBike));
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
            }
        }

        public BikeLogViewModel BikeLogViewModel { get; } = new();
        public CarLogViewModel CarLogViewModel { get; } = new();

        #endregion
    }
}