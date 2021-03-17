using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using TourPlanner.Commands;
using TourPlanner.Logic;

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

        private string _route;
        public string Route
        {
            get => _route;
            set
            {
                _route = value;
                OnPropertyChanged(nameof(Route));
            }
        }


        public ICommand AddTourCommand { get; }

        public ICommand ValidateCommand { get; }
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
            GetDistance getDistance = new GetDistance();
            //httpclass c = new httpclass
            //c.getDirections();

            //HTTP REQUEST SEND TO MAP API...

            //send to map updater.
            //MessageBox.Show("Title: " + TitleName + "\nSource -> Destination: " + Source + " -> " +
            //Destination + "\nDescription: " + Description + "\n was added");

            MessageBox.Show(Convert.ToString(getDistance.Distance(Source, Destination), CultureInfo.InvariantCulture));

            //Debug.Assert(false, $"{Tour.TitleName + " " + Tour.Destination} was updated.");
        }

        


        public void Validate()
        {
            var getDistance = new GetDistance();

            GetMap getMap = new GetMap();

            this.TourDistance =  Convert.ToDouble(getDistance.Distance(Source, Destination),
                CultureInfo.InvariantCulture);


             getMap.SaveImage(Source, Destination);

             var src = @"..\images\" + Source + "_" + Destination + ".jpeg";
             //var src = Source + "_" + Destination + ".jpeg";
             
             this.Route = src;

             //this.Route = new BitmapImage(new Uri(src, UriKind.Relative));

             //MessageBox.Show(Convert.ToString(getDistance.Distance(Source, Destination), CultureInfo.InvariantCulture));
        }


        public void SaveLogChanges()
        {

        }

    }
}
