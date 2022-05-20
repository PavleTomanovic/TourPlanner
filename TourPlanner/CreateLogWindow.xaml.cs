using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for CreateLogWindow.xaml
    /// </summary>
    public partial class CreateLogWindow : Window
    {
        public CreateLogWindow(string id)
        {
            InitializeComponent();
            var tourChangesView = new TourChangesView();
            tourChangesView.TourId = id;
            this.DataContext = tourChangesView;
        }
    }
}
