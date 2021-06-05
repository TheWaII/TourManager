using System.Windows;
using System.Windows.Controls;

namespace TourPlanner.Views.UserControls
{
    /// <summary>
    ///     Interaction logic for GlobalSearch.xaml
    /// </summary>
    public partial class GlobalSearch : UserControl
    {
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(GlobalSearch), new PropertyMetadata(""));

        public GlobalSearch()
        {
            InitializeComponent();
        }

        public string SearchText
        {
            get => (string) GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }
    }
}