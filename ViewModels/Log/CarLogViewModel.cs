using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Annotations;

namespace TourPlanner.ViewModels.Log
{
    public class CarLogViewModel : INotifyPropertyChanged
    {


        private int _maxSpeed;

        public int MaxSpeed
        {
            get => _maxSpeed;

            set
            {
                _maxSpeed = value;
                OnPropertyChanged(nameof(MaxSpeed));
            }
        }

        private int _averageSpeed;

        public int AverageSpeed
        {
            get => _averageSpeed;

            set
            {
                _averageSpeed = value;
                OnPropertyChanged(nameof(AverageSpeed));
            }
        }

        private int _fuelUsed;

        public int FuelUsed
        {
            get => _fuelUsed;

            set
            {
                _fuelUsed = value;
                OnPropertyChanged(nameof(FuelUsed));
            }
        }

        private decimal _fuelCost;

        public decimal FuelCost
        {
            get => _fuelCost;

            set
            {
                _fuelCost = value;
                OnPropertyChanged(nameof(FuelCost));
            }
        }

        private int _caloriesBurnt;

        public int CaloriesBurnt
        {
            get => _caloriesBurnt;

            set
            {
                _caloriesBurnt = value;
                OnPropertyChanged(nameof(CaloriesBurnt));
            }
        }

        private bool _caughtSpeeding;

        public bool CaughtSpeeding
        {
            get => _caughtSpeeding;
            set
            {
                _caughtSpeeding = value;
                OnPropertyChanged(nameof(CaughtSpeeding));
            }
        }

        private bool _yes;

        public bool Yes
        {
            get => _yes;
            set
            {
                _yes = value;
                OnPropertyChanged(nameof(Yes));
            }
        }

        private bool _no = true;

        public bool No
        {
            get => _no;
            set
            {
                _no = value;
                OnPropertyChanged(nameof(No));
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
