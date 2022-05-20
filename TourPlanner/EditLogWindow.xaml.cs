using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for EditLogWindow.xaml
    /// </summary>
    public partial class EditLogWindow : Window
    {
        public EditLogWindow(string id)
        {
            InitializeComponent();
            var tourChangesView = new TourChangesView();
            tourChangesView.LogID = id;
            this.DataContext = tourChangesView;
        }
    }
}
