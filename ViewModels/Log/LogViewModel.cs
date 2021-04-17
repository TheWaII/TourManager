using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TourPlanner.Annotations;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Commands;
using TourPlanner.Model.Tour;
using TourPlanner.Model.Log;
using TourPlanner.ViewModels.Tour;

namespace TourPlanner.ViewModels.Log
{
    public class LogViewModel : INotifyPropertyChanged
    {
        #region properties

        private int _id = 0;

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

        private bool _isCheckedNone = true;

        public bool IsCheckedNone
        {
            get => _isCheckedNone;
            set
            {
                _isCheckedNone = value;
                OnPropertyChanged(nameof(IsCheckedNone));
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

        #endregion

        public RelayCommand AddLogToggle { get; }

        public RelayCommand NoneRadioButton { get; }
        public RelayCommand BikeRadioButton { get; }
        public RelayCommand CarRadioButton { get; }

        public RelayCommand AddLog { get; }


        public LogViewModel()
        {
            AddLogToggle = new RelayCommand(o => ToggleAddLog());
            NoneRadioButton = new RelayCommand(o => ToggleShowNone());
            BikeRadioButton = new RelayCommand(o => ToggleShowBike());
            CarRadioButton = new RelayCommand(o => ToggleShowCar());
            AddLog = new RelayCommand(o => SaveChangesLog(), o=> CanAddLog);
        }

        public bool CanAddLog => Id != 0 && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Date) && !string.IsNullOrWhiteSpace(Time) && Rating != 0 && !string.IsNullOrWhiteSpace(Report);


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


        private string _bikeVisibility = "Hidden";

        public string BikeVisibility
        {
            get => _bikeVisibility;
            set
            {
                _bikeVisibility = value;
                OnPropertyChanged(nameof(BikeVisibility));
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

        private string _carVisibility = "Hidden";

        public string CarVisibility
        {
            get => _carVisibility;

            set
            {
                _carVisibility = value;
                OnPropertyChanged(nameof(CarVisibility));
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

        private void ToggleShowNone()
        {
            IsCheckedNone = IsCheckedNone == false;

            if (!IsCheckedAdd) return;
            BikeVisibility = "Hidden";
            CarVisibility = "Hidden";
            IsCheckedCar = false;
            IsCheckedBike = false;

        }

        public void SaveChangesLog()
        {
            var dbDatabaseLogic = new LogLogic();

            var log = new LogData
            {
                TourId = Id,
                LogName = Name,
                LogDate = Date.Split(' ')[0],
                LogDistance = Distance,
                LogTotalTime = Time,
                LogRating = Rating,
                LogReport = Report
            };

            dbDatabaseLogic.InsertLog(log);

            Name = string.Empty;
            Date = string.Empty; 
            Distance = 0;
            Time = string.Empty;
            Rating = 0;
            Report = string.Empty;
            Id = 0;

            var tvm = new TourViewModel();
            tvm.RefreshLogList();


        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
