using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Database;
using TourPlanner.BL.Route;
using TourPlanner.Commands;
using TourPlanner.Model;

namespace TourPlanner.ViewModels.Tour
{
    public class TourViewModel : INotifyPropertyChanged, IValueConverter
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

        private double _distance;

        public double Distance
        {
            get => _distance;

            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
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

        private bool _isCheckedEdit;

        public bool IsCheckedEdit
        {
            get => _isCheckedEdit;
            set
            {
                _isCheckedEdit = value;
                OnPropertyChanged(nameof(IsCheckedEdit));
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

        private string _editTourVisibility = "Hidden";

        public string EditTourVisibility
        {
            get => _editTourVisibility;
            set
            {
                _editTourVisibility = value;
                OnPropertyChanged(nameof(EditTourVisibility));
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

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;

                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(MyFilteredItems));
            }
        }

        public IEnumerable<TourData> MyFilteredItems
        {
            get
            {
                return SearchText == null ? Data : Data.Where(x => x.TourName.Contains(SearchText));
            }
        }

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

        private TourData _selectedTourData;

        public TourData SelectedTourData
        {
            get => _selectedTourData;
            set
            {
                _selectedTourData = value;
                OnPropertyChanged(nameof(SelectedTourData));
            }
        }

        public RelayCommand EditTourCommand { get; }

        public RelayCommand EditTourToggle { get; }

        public RelayCommand AddTourCommand { get; }

        public RelayCommand DeleteTourCommand { get; }

        public RelayCommand ValidateCommand { get; }

        public RelayCommand ValidateCommandEdit { get; }

        public RelayCommand AddTourToggle { get; }

        public ICommand WindowLoaded { get; set; }

        #endregion

        #region Constructor
        public TourViewModel()
        {
            AddTourToggle = new RelayCommand(o => ToggleAddTour());

            ValidateCommand = new RelayCommand(o => Validate(), o => CanUpdateValidate);

            ValidateCommandEdit = new RelayCommand(o => ValidateEdit());

            AddTourCommand = new RelayCommand(o => SaveChanges(), o => CanUpdate);

            WindowLoaded = new RelayCommand(o => RefreshList());

            EditTourCommand = new RelayCommand(o => UpdateChanges());

            EditTourToggle = new RelayCommand(o => ToggleEditTour());

            DeleteTourCommand = new RelayCommand(o => DeleteTour());

        }

        #endregion

        #region CanUpdate
        public bool CanUpdate =>
            !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) &&
            !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination) &&
            Distance != 0;


        public bool CanUpdateValidate => !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Destination);
        

        #endregion

        public void RefreshList()
        {
            _data.Clear();

            var dbDatabaseLogic = new TourLogic();
            foreach (var item in dbDatabaseLogic.LoadTours())
            {
                var tourData = new TourData
                {
                    TourId = item.TourId,
                    TourName = item.TourName,
                    TourDescription = item.TourDescription,
                    TourSource = item.TourSource,
                    TourDestination = item.TourDestination,
                    TourDistance = item.TourDistance,
                    TourRoute = item.TourRoute
                };

                _data.Add(tourData);
            }
        }

        public void SaveChanges()
        {
            var dbDatabaseLogic = new TourLogic();

            var tour = new TourData
            {
                TourName = Name,
                TourSource = Source,
                TourDestination = Destination,
                TourDistance = Distance,
                TourDescription = Description
            };

            dbDatabaseLogic.InsertTours(tour);

            Name = string.Empty;
            Source = string.Empty;
            Destination = string.Empty;
            Distance = 0;
            Description = string.Empty;
            ProgressBarColor = "Gray";
            RefreshList();
        }

        public void ToggleAddTour()
        {

            IsCheckedAdd = IsCheckedAdd == false;

            if (IsCheckedAdd)
            {
                AddTourVisibility = "Visible";
                RouteDescriptionVisibility = "Hidden";
            }
            else
            {
                AddTourVisibility = "Hidden";
                RouteDescriptionVisibility = "Visible";
            }
        }

        public void ToggleEditTour()
        {
            IsCheckedEdit = IsCheckedEdit == false;

            if (IsCheckedEdit)
            {
                EditTourVisibility = "Visible";
                RouteDescriptionVisibility = "Hidden";
            }
            else
            {
                EditTourVisibility = "Hidden";
                RouteDescriptionVisibility = "Visible";
            }

        }

        public void Validate()
        {
            var getDistance = new GetDistance();

            var getMap = new GetMap();

            Distance = System.Convert.ToDouble(getDistance.Distance(Source, Destination));


            ProgressBarColor = Distance != 0 ? "Green" : "Red";

            getMap.SaveImage(Source, Destination);

        }

        public void ValidateEdit()
        {
            var getDistance = new GetDistance();

            var getMap = new GetMap();

            SelectedTourData.TourDistance = System.Convert.ToDouble(getDistance.Distance(SelectedTourData.TourSource, SelectedTourData.TourDestination));

            ProgressBarColor = SelectedTourData.TourDistance != 0 ? "Green" : "Red";



            getMap.SaveImage(SelectedTourData.TourSource, SelectedTourData.TourDestination);
        }

        public void UpdateChanges()
        {
            var dbDatabaseLogic = new TourLogic();
            var tour = new TourData
            {
                TourId = SelectedTourData.TourId,
                TourName = SelectedTourData.TourName,
                TourSource = SelectedTourData.TourSource,
                TourDestination = SelectedTourData.TourDestination,
                TourDistance = SelectedTourData.TourDistance,
                TourDescription = SelectedTourData.TourDescription, 
                TourRoute = SelectedTourData.TourSource + "_" + SelectedTourData.TourDestination
            };

            dbDatabaseLogic.UpdateTours(tour);

            Name = string.Empty;
            Source = string.Empty;
            Destination = string.Empty;
            Distance = 0;
            Description = string.Empty;
            ProgressBarColor = "Gray";
            RefreshList();
        }

        public void DeleteTour()
        {
            var dbDatabaseLogic = new TourLogic();

            dbDatabaseLogic.DeleteTours(SelectedTourData);

            RefreshList();
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value != null
                    ? BitmapFrame.Create(new Uri(value.ToString(), UriKind.Relative))
                    : BitmapFrame.Create(new Uri(@"..\..\img\route\error.jpeg", UriKind.Relative));

            }
            catch (Exception)
            {
                return BitmapFrame.Create(new Uri(@"..\..\img\route\error.jpeg", UriKind.Relative));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #region ProperteyChangedEventHandler
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
