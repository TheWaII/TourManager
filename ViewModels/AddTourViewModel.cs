using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using TourPlanner.BL;
using TourPlanner.Commands;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class AddTourViewModel : INotifyPropertyChanged
    {
        
        #region Properties
        private string _titleName;

        public string TitleName
        {
            get => _titleName;
            set
            {
                _titleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private string _source;

        public string Source
        {
            get => _source;
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        private string _destination;

        public string Destination
        {
            get => _destination;
            set
            {
                _destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }


        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private double _tourDistance;

        public double TourDistance
        {
            get => _tourDistance;

            set
            {
                _tourDistance = value;
                OnPropertyChanged(nameof(TourDistance));
            }
        }

        private ImageSource _route;
        public ImageSource Route
        {
            get => _route;
            set
            {
                _route = value;
                OnPropertyChanged(nameof(Route));
            }
        }

        public ObservableCollection<TourData> Data { get; set; } = new ObservableCollection<TourData>();


        public ICommand AddTourCommand { get; }

        public ICommand ValidateCommand { get; }
        public string ArrowImage => @"..\..\img\icons\arrow.png";

        #endregion

        public AddTourViewModel()
        {

            AddTourCommand = new AddTourCommand(this);
            ValidateCommand = new ValidateCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        
        public bool CanUpdate =>
            !string.IsNullOrWhiteSpace(TitleName) && !string.IsNullOrWhiteSpace(Description) &&
            !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination) &&
            TourDistance != 0;
        

        public bool CanUpdateValidate =>
            !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination);
        

        public void SaveChanges()
        {
            var tourData = new TourData
            {
                TourName = this.TitleName,
                Source = this.Source,
                Destination = this.Destination,
                Distance = this.TourDistance
            };



            var getDistance = new GetDistance();
            
            Data.Add(tourData);
            //MessageBox.Show(Convert.ToString(getDistance.Distance(Source, Destination), CultureInfo.InvariantCulture));
        }


        public void Validate()
        {
            var getMap = new MapApiStrings();

            var getDistance = new GetDistance();

            //var getMap = new GetMap();

            this.TourDistance = Convert.ToDouble(getDistance.Distance(Source, Destination),
                CultureInfo.InvariantCulture);

            //getMap.SaveImage(Source, Destination);

            //var src = @"..\..\img" + Source + "_" + Destination + ".jpeg";
            //this.Route = new BitmapImage(new Uri(src, UriKind.Relative));

            //var src = @"C:\Users\thewa\source\repos\TourPlanner\img\" + Source + "_" + Destination + ".jpeg";
            //this.Route = new BitmapImage(new Uri(src, UriKind.Absolute));

            this.Route = new BitmapImage(new Uri(getMap.MapUrl(Source, Destination)));

        }
    }
}
