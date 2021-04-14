using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TourPlanner.Annotations;
using TourPlanner.Commands;

namespace TourPlanner.ViewModels.Log
{
    public class LogViewModel : INotifyPropertyChanged
    {
        #region properties
        private string _logTitle;

        public string LogTitle
        {
            get => _logTitle;
            set
            {
                _logTitle = value;
                OnPropertyChanged(nameof(LogTitle));
            }
        }

        private DateTime _logDate;

        public DateTime LogDate
        {
            get => _logDate;
            set
            {
                _logDate = value;
                OnPropertyChanged(nameof(LogDate));
            }
        }


        private double _logDistance;

        public double LogDistance
        {
            get => _logDistance;

            set
            {
                _logDistance = value;
                OnPropertyChanged(nameof(LogDistance));
            }
        }


        private string _logTime;

        public string LogTime
        {
            get => _logTime;
            set
            {
                _logTime = value;
                OnPropertyChanged(nameof(LogTime));
            }
        }

        private int _logRating;

        public int LogRating
        {
            get => _logRating;

            set
            {
                _logRating = value;
                OnPropertyChanged(nameof(LogRating));
            }
        }

        private string _logReport;

        public string LogReport
        {
            get => _logReport;

            set
            {
                _logReport = value;
                OnPropertyChanged(nameof(LogReport));
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


        #endregion

        public RelayCommand AddLogToggle { get; }

        public RelayCommand NoneRadioButton { get; }
        public RelayCommand BikeRadioButton { get; }
        public RelayCommand CarRadioButton { get; }


        public LogViewModel()
        {
            AddLogToggle = new RelayCommand(o => ToggleAddLog());
            NoneRadioButton = new RelayCommand(o => ToggleShowNone());
            BikeRadioButton = new RelayCommand(o => ToggleShowBike());
            CarRadioButton = new RelayCommand(o => ToggleShowCar());
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




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
