using System.ComponentModel;
using System.Runtime.CompilerServices;
using TourPlanner.Annotations;

namespace TourPlanner.ViewModels.Log
{
    public class CarLogViewModel : INotifyPropertyChanged
    {
        private int _averageSpeed;

        private int _caloriesBurnt;

        private bool _caughtSpeeding;

        private decimal _fuelCost;

        private int _fuelUsed;


        private int _maxSpeed;

        private bool _no = true;

        private bool _yes;

        public int MaxSpeed
        {
            get => _maxSpeed;

            set
            {
                _maxSpeed = value;
                OnPropertyChanged(nameof(MaxSpeed));
            }
        }

        public int AverageSpeed
        {
            get => _averageSpeed;

            set
            {
                _averageSpeed = value;
                OnPropertyChanged(nameof(AverageSpeed));
            }
        }

        public int FuelUsed
        {
            get => _fuelUsed;

            set
            {
                _fuelUsed = value;
                OnPropertyChanged(nameof(FuelUsed));
            }
        }

        public decimal FuelCost
        {
            get => _fuelCost;

            set
            {
                _fuelCost = value;
                OnPropertyChanged(nameof(FuelCost));
            }
        }

        public int CaloriesBurnt
        {
            get => _caloriesBurnt;

            set
            {
                _caloriesBurnt = value;
                OnPropertyChanged(nameof(CaloriesBurnt));
            }
        }

        public bool CaughtSpeeding
        {
            get => _caughtSpeeding;
            set
            {
                _caughtSpeeding = value;
                OnPropertyChanged(nameof(CaughtSpeeding));
            }
        }

        public bool Yes
        {
            get => _yes;
            set
            {
                _yes = value;
                OnPropertyChanged(nameof(Yes));
            }
        }

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