using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels.Log
{
    public class Car : AddLogViewModel
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

        private double _fuelCost;

        public double FuelCost
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

        private int _caughtSpeeding;

        public int CaughtSpeeding
        {
            get => _caughtSpeeding;
            set
            {
                _caughtSpeeding = value;
                OnPropertyChanged(nameof(CaughtSpeeding));
            }
        }

    }
}
