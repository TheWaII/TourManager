using System.ComponentModel;
using System.Runtime.CompilerServices;
using TourPlanner.Annotations;
using TourPlanner.Commands;

namespace TourPlanner.ViewModels
{
    public class DarkModeViewModel : INotifyPropertyChanged
    {
        public static string White = "White";
        public static string Black = "Black";
        public static string BurlyWood = "BurlyWood";

        private string _colorDark = Black;

        private string _colorLight = White;

        private string _darkMode = "Hidden";


        private bool _darkModeBoolean;

        private string _editImage = EditImageNormal;

        private string _header = BurlyWood;


        private string _lightMode = "Visible";

        private string _logoImage = LogoImageNormal;

        private string _minusImage = MinusImageNormal;

        private string _plusImage = PlusImageNormal;

        private string _reloadImage = ReloadImageNormal;


        private string _saveImage = SaveImageNormal;

        private string _wood = BurlyWood;

        public DarkModeViewModel()
        {
            DarkModeToggle = new RelayCommand(o => SetDarkMode());
        }

        public static string LogoImageNormal => @"..\..\img\icons\logo.png";
        public static string LogoImageInverted => @"..\..\img\icons\logoInverted.png";

        public string LogoImage
        {
            get => _logoImage;
            set
            {
                _logoImage = value;
                OnPropertyChanged(nameof(LogoImage));
            }
        }

        public string PlusImage
        {
            get => _plusImage;
            set
            {
                _plusImage = value;
                OnPropertyChanged(nameof(PlusImage));
            }
        }

        public string MinusImage
        {
            get => _minusImage;
            set
            {
                _minusImage = value;
                OnPropertyChanged(nameof(MinusImage));
            }
        }

        public string EditImage
        {
            get => _editImage;
            set
            {
                _editImage = value;
                OnPropertyChanged(nameof(EditImage));
            }
        }

        public string ReloadImage
        {
            get => _reloadImage;
            set
            {
                _reloadImage = value;
                OnPropertyChanged(nameof(ReloadImage));
            }
        }

        public string SaveImage
        {
            get => _saveImage;
            set
            {
                _saveImage = value;
                OnPropertyChanged(nameof(SaveImage));
            }
        }

        public static string PlusImageNormal => @"..\..\img\icons\plus.png";
        public static string PlusImageInverted => @"..\..\img\icons\plusInverted.png";

        public static string MinusImageNormal => @"..\..\img\icons\minus.png";
        public static string MinusImageInverted => @"..\..\img\icons\minusInverted.png";

        public static string EditImageNormal => @"..\..\img\icons\edit.png";
        public static string EditImageInverted => @"..\..\img\icons\editInverted.png";

        public static string ReloadImageNormal => @"..\..\img\icons\reload.png";
        public static string ReloadImageInverted => @"..\..\img\icons\reloadInverted.png";

        public static string SaveImageNormal => @"..\..\img\icons\save.png";
        public static string SaveImageInverted => @"..\..\img\icons\saveInverted.png";

        public string ColorLight
        {
            get => _colorLight;
            set
            {
                _colorLight = value;
                OnPropertyChanged(nameof(ColorLight));
            }
        }

        public string ColorDark
        {
            get => _colorDark;
            set
            {
                _colorDark = value;
                OnPropertyChanged(nameof(ColorDark));
            }
        }


        public string Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                OnPropertyChanged(nameof(Wood));
            }
        }

        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }


        public static string LightBulbOn => @"..\..\img\icons\lightOn.png";

        public static string LightBulbOff => @"..\..\img\icons\lightOff.png";

        public string LightMode
        {
            get => _lightMode;
            set
            {
                _lightMode = value;
                OnPropertyChanged(nameof(LightMode));
            }
        }


        public string DarkMode
        {
            get => _darkMode;
            set
            {
                _darkMode = value;
                OnPropertyChanged(nameof(DarkMode));
            }
        }

        public RelayCommand DarkModeToggle { get; }


        public event PropertyChangedEventHandler PropertyChanged;


        public void SetDarkMode()
        {
            _darkModeBoolean = _darkModeBoolean == false;

            if (_darkModeBoolean)
            {
                DarkMode = "Visible";
                LightMode = "Hidden";
                ColorLight = Black;
                ColorDark = White;
                Wood = White;
                Header = Black;

                LogoImage = LogoImageInverted;
                PlusImage = PlusImageInverted;
                MinusImage = MinusImageInverted;
                EditImage = EditImageInverted;
                ReloadImage = ReloadImageInverted;
                SaveImage = SaveImageInverted;
            }
            else
            {
                DarkMode = "Hidden";
                LightMode = "Visible";
                ColorLight = White;
                ColorDark = Black;
                Wood = BurlyWood;
                Header = BurlyWood;

                LogoImage = LogoImageNormal;
                PlusImage = PlusImageNormal;
                MinusImage = MinusImageNormal;
                EditImage = EditImageNormal;
                ReloadImage = ReloadImageNormal;
                SaveImage = SaveImageNormal;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}