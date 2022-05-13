using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für NewTourWindow.xaml
    /// </summary>
    public partial class NewTourWindow : Window
    {
        public NewTourWindow()
        {
            InitializeComponent();
            this.DataContext = new TourChangesView();
        }

    }
}
