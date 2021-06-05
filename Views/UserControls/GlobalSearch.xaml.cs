using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner.Annotations;

namespace TourPlanner.Views.UserControls
{
    /// <summary>
    /// Interaction logic for GlobalSearch.xaml
    /// </summary>
    public partial class GlobalSearch : UserControl
    {
        public GlobalSearch()
        {
            InitializeComponent();

        }

        public string SearchText
        {
            get => (string) GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(GlobalSearch), new PropertyMetadata(""));

    }
}
