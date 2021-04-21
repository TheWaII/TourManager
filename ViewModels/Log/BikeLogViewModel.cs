using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Annotations;

namespace TourPlanner.ViewModels.Log
{
    public class BikeLogViewModel : INotifyPropertyChanged
    {
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

        private int _lowestHeartRate;

        public int LowestHeartRate
        {
            get => _lowestHeartRate;

            set
            {
                _lowestHeartRate = value;
                OnPropertyChanged(nameof(LowestHeartRate));
            }
        }

        private int _averageHeartRate;

        public int AverageHeartRate
        {
            get => _averageHeartRate;

            set
            {
                _averageHeartRate = value;
                OnPropertyChanged(nameof(AverageHeartRate));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
