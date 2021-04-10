using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Annotations;
using TourPlanner.Commands;

namespace TourPlanner.ViewModels.Log
{
    public class AddLogViewModel : INotifyPropertyChanged
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

        #endregion

        public RelayCommand AddLogToggle { get; }

        public AddLogViewModel()
        {
            AddLogToggle = new RelayCommand(o => ToggleAddLog());
        }

        private void ToggleAddLog()
        {
            this.IsCheckedAdd = this.IsCheckedAdd == false;

            if (this.IsCheckedAdd)
            {
                this.AddLogVisibility = "Visible";
                this.LogVisibility = "Hidden";
            }
            else
            {
                this.AddLogVisibility = "Hidden";
                this.LogVisibility = "Visible";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
