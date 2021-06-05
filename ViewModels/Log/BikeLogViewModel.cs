using System.ComponentModel;
using System.Runtime.CompilerServices;
using TourPlanner.Properties;

namespace TourPlanner.ViewModels.Log
{
    public class BikeLogViewModel : INotifyPropertyChanged
    {
        private int _averageHeartRate;

        private int _averageSpeed;

        private int _caloriesBurnt;

        private int _lowestHeartRate;
        private int _peakHeartRate;

        public int PeakHeartRate
        {
            get => _peakHeartRate;

            set
            {
                _peakHeartRate = value;
                OnPropertyChanged(nameof(PeakHeartRate));
            }
        }

        public int LowestHeartRate
        {
            get => _lowestHeartRate;

            set
            {
                _lowestHeartRate = value;
                OnPropertyChanged(nameof(LowestHeartRate));
            }
        }

        public int AverageHeartRate
        {
            get => _averageHeartRate;

            set
            {
                _averageHeartRate = value;
                OnPropertyChanged(nameof(AverageHeartRate));
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

        public int CaloriesBurnt
        {
            get => _caloriesBurnt;

            set
            {
                _caloriesBurnt = value;
                OnPropertyChanged(nameof(CaloriesBurnt));
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