using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TourManager.Models
{
    public class AddTour : INotifyPropertyChanged
    {
        private string _titleName;

        public string TitleName
        {
            get => _titleName;
            set
            {
                _titleName = value;
                OnPropertyChanged("Title");
            }
        }

        //private DateTime _startDate;

        //public DateTime StartDate
        //{
        //    get => _startDate;
        //    set
        //    {
        //        _startDate = value;
        //        OnPropertyChanged("StartDateTime");
        //    }
        //}

        //private DateTime _endDate;

        //public DateTime EndDate
        //{
        //    get => _endDate;
        //    set
        //    {
        //        _endDate = value;
        //        OnPropertyChanged("EndDateTime");
        //    }
        //}

        private string _source;

        public string Source
        {
            get => _source;
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        private string _destination;

        public string Destination
        {
            get => _destination;
            set
            {
                _destination = value;
                OnPropertyChanged("Destination");
            }
        }


        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private double _tourDistance;

        public double TourDistance
        {
            get => _tourDistance;

            set
            {
                _tourDistance = value;
                OnPropertyChanged("TourDistance");
            }
        }


        //public AddTour(string titleName, string source, string destination, string description, double tourDistance)
        //{
        //    TitleName = titleName;
        //    Source = source;
        //    Destination = destination;
        //    Description = description;
        //    TourDistance = tourDistance;
        //}


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
