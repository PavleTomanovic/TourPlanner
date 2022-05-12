using System;
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

        private void New_Tour(object sender, RoutedEventArgs e)
        {
            NewTourWindow popup = new NewTourWindow();
            popup.Show();
        }
        private void Edit_Tour(object sender, RoutedEventArgs e)
        {
        }
        private void Delete_Tour(object sender, RoutedEventArgs e)
        {
        }
        private void Exit_Program(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
