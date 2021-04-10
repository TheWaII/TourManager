using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Catel.Linq;
using TourPlanner.BL;
using TourPlanner.Commands;
using TourPlanner.Model;


namespace TourPlanner.ViewModels.Tour
{
    public class AddTourViewModel : INotifyPropertyChanged
    {

        #region Properties
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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


        private string _routeDescriptionVisibility = "Visible";

        public string RouteDescriptionVisibility
        {
            get => _routeDescriptionVisibility;
            set
            {
                _routeDescriptionVisibility = value;
                OnPropertyChanged(nameof(RouteDescriptionVisibility));
            }
        }

        private string _addTourVisibility = "Hidden";

        public string AddTourVisibility
        {
            get => _addTourVisibility;
            set
            {
                _addTourVisibility = value;
                OnPropertyChanged(nameof(AddTourVisibility));
            }
        }

        private string _progressbarColor = "Gray";

        public string ProgressBarColor
        {
            get => _progressbarColor;
            set
            {
                _progressbarColor = value; 
                OnPropertyChanged(nameof(ProgressBarColor));
            }
        }

        public ICommand WindowLoaded { get; set; }

        private ObservableCollection<TourData> _data = new();

        public ObservableCollection<TourData> Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));

            }
        }

        public RelayCommand AddTourCommand { get; }

        public RelayCommand ValidateCommand { get; }

        public RelayCommand AddTourToggle { get; }

        public string ArrowImage => @"..\..\img\icons\arrow.png";


        #endregion

        public AddTourViewModel()
        {
            AddTourToggle = new RelayCommand(o => ToggleAddTour());

            ValidateCommand = new RelayCommand(o=>Validate(), o => CanUpdateValidate);

            AddTourCommand = new RelayCommand(o => SaveChanges(), o=>CanUpdate);

            WindowLoaded = new RelayCommand(o => SaveChanges());
        }




        public bool CanUpdate =>
            !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) &&
            !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination) &&
            TourDistance != 0;


        public bool CanUpdateValidate =>
            !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination);

        public void SaveChanges()
        {
            var dbDatabaseLogic = new DatabaseLogic();

            

            dbDatabaseLogic.LoadTours();

            var tourData = new TourData();


            //foreach (var variable in dbDatabaseLogic.Observable)
            //{

            //    tourData.Name = this.Name;
            //    tourData.Description = this.Description;
            //    tourData.Destination = this.Destination;
            //    tourData.Distance = this.TourDistance;
            //    tourData.Source = this.Source;
            //}

            foreach (var item in dbDatabaseLogic.Observable)
            {
                tourData.Name = item.TourName;
                tourData.Description = item.TourDescription;
                tourData.Source = item.TourSource;
                tourData.Destination = item.TourDestination;
                tourData.Distance = item.TourDistance;
            }


            _data.Add(tourData);

            //add vales to table tours in database

            this.Name = string.Empty;
            this.Source = string.Empty;
            this.Destination = string.Empty;
            this.TourDistance = 0;
            this.Description = string.Empty;
            this.ProgressBarColor = "Gray";
            //MessageBox.Show(Convert.ToString(getDistance.Distance(Source, Destination), CultureInfo.InvariantCulture));
        }

        public void ToggleAddTour()
        {

            this.IsCheckedAdd = this.IsCheckedAdd == false;

            if (this.IsCheckedAdd)
            {
                this.AddTourVisibility = "Visible";
                this.RouteDescriptionVisibility = "Hidden";
            }
            else
            {
                this.AddTourVisibility = "Hidden";
                this.RouteDescriptionVisibility = "Visible";
            }
        }


        public void Validate()
        {
            var getMap = new MapApiStrings();

            var getDistance = new GetDistance();

            //var getMap = new GetMap();

            this.TourDistance = Convert.ToDouble(getDistance.Distance(Source, Destination));


            this.ProgressBarColor = TourDistance != 0 ? "Green" : "Red";

            //getMap.SaveImage(Source, Destination);

            //var src = @"..\..\img" + Source + "_" + Destination + ".jpeg";
            //this.Route = new BitmapImage(new Uri(src, UriKind.Relative));

            //var src = @"C:\Users\thewa\source\repos\TourPlanner\img\" + Source + "_" + Destination + ".jpeg";
            //this.Route = new BitmapImage(new Uri(src, UriKind.Absolute));

            if(ProgressBarColor == "Green")
                this.Route = new BitmapImage(new Uri(getMap.MapUrl(Source, Destination)));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
