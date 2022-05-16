using System;
using System.Windows;
using System.Windows.Controls;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für Task_Overview.xaml
    /// </summary>
    public partial class TaskSection : Page
    {
        public static NewTourWindow popup = new NewTourWindow();

        public TaskSection()
        {
            InitializeComponent();
        }

        private void New_Tour(object sender, RoutedEventArgs e)
        {
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
        private void itemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var name = item.SelectedItem as string;
            MessageBox.Show(name + "\n");
        }
    }
}
