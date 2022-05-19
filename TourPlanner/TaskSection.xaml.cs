using System.Windows;
using System.Windows.Controls;

namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für Task_Overview.xaml
    /// </summary>
    public partial class TaskSection : Page
    {
        public TaskSection()
        {
            InitializeComponent();
        }
        private void exit_Program(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //Command
        }
    }
}
