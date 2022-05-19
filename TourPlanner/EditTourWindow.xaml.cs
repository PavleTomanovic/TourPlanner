using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for EditTourWindow.xaml
    /// </summary>
    public partial class EditTourWindow : Window
    {
        public EditTourWindow(string id)
        {
            InitializeComponent();
            var tourChangesView = new TourChangesView();
            tourChangesView.TourId = id;
            this.DataContext = tourChangesView;
        }
    }
}
