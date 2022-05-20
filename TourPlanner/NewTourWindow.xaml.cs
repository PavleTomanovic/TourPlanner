using System.ComponentModel;
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
            this.Closing += NewTourWindow_Closing; ;
        }

        private void NewTourWindow_Closing(object? sender, CancelEventArgs e)
        {
            this.Hide();
        }


    }
}
