using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using ControlzEx.Standard;
using TourPlanner.Annotations;
using TourPlanner.Commands;

namespace TourPlanner.ViewModels
{
    public class DarkModeViewModel : INotifyPropertyChanged
    {

        public static string White = "White";
        public static string Black = "Black";
        public static string BurlyWood = "BurlyWood";

        public static string LogoImageNormal => @"..\..\img\icons\logo.png";
        public static string LogoImageInverted => @"..\..\img\icons\logoInverted.png";

        private string _logoImage = LogoImageNormal;

        public string LogoImage
        {
            get => _logoImage;
            set
            {
                _logoImage = value;
                OnPropertyChanged(nameof(LogoImage));
            }
        }

        private string _plusImage = PlusImageNormal;
        public string PlusImage
        {
            get => _plusImage;
            set
            {
                _plusImage = value;
                OnPropertyChanged(nameof(PlusImage));
            }
        }

        private string _minusImage = MinusImageNormal;
        public string MinusImage
        {
            get => _minusImage;
            set
            {
                _minusImage = value;
                OnPropertyChanged(nameof(MinusImage));
            }
        }

        private string _editImage = EditImageNormal;
        public string EditImage
        {
            get => _editImage;
            set
            {
                _editImage = value;
                OnPropertyChanged(nameof(EditImage));
            }
        }

        private string _reloadImage = ReloadImageNormal;
        public string ReloadImage
        {
            get => _reloadImage;
            set
            {
                _reloadImage = value;
                OnPropertyChanged(nameof(ReloadImage));
            }
        }


        private string _saveImage = SaveImageNormal;
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

        private string _colorLight = White;

        public string ColorLight
        {
            get => _colorLight;
            set
            {
                _colorLight = value;
                OnPropertyChanged(nameof(ColorLight));
            }
        }

        private string _colorDark = Black;

        public string ColorDark
        {
            get => _colorDark;
            set
            {
                _colorDark = value;
                OnPropertyChanged(nameof(ColorDark));
            }
        }

        private string _wood = BurlyWood;


        public string Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                OnPropertyChanged(nameof(Wood));
            }
        }

        private string _header = BurlyWood;

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



        private string _lightMode = "Visible";

        public string LightMode
        {
            get => _lightMode;
            set
            {
                _lightMode = value;
                OnPropertyChanged(nameof(LightMode));

            }
        }

        private string _darkMode = "Hidden";



        public string DarkMode
        {
            get => _darkMode;
            set
            {
                _darkMode = value;
                OnPropertyChanged(nameof(DarkMode));
            }
        }

        public DarkModeViewModel()
        {
            DarkModeToggle = new RelayCommand(o => SetDarkMode());
        }

        public RelayCommand DarkModeToggle { get; }


        private bool _darkModeBoolean;


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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
