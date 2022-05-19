using System;
using System.Windows;
using System.Windows.Controls;
using TourPlanner.Models;
using TourPlanner.ViewModels;
using TourPlanner.BussinesLayer;
using System.Collections;

namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für Task_Overview.xaml
    /// </summary>
    public partial class TaskSection : Page
    {
        public static EditTourWindow editPopup = new EditTourWindow();

        public TaskSection()
        {
            InitializeComponent();
        }

        private void Edit_Tour(object sender, RoutedEventArgs e)
        {
            editPopup.Show();
        }
        private void Delete_Tour(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.DeleteRoute("6");
                    MessageBox.Show("Route successfully deleted", "Delete Route", MessageBoxButton.OK);
                    break;
            }
        }
        private void Exit_Program(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
