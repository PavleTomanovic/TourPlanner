using System;
using System.Windows;
using System.Windows.Controls;
using TourPlanner.Models;
using TourPlanner.ViewModels;
using TourPlanner.BussinesLayer;
using System.Collections;
using Microsoft.Win32;
using System.IO;

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
        //Wir können das später in ein Command machen
        private void export_File(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = @"C:\Temp\TourPlanner\Export\";
            if (saveFileDialog.ShowDialog() == true)
                BussinessLogic.LogicInstance.ExportRouteToFile(saveFileDialog.FileName, "1");
        }
        private void import_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = @"C:\Temp\TourPlanner\TemplateInsert\";
            if (openFileDialog.ShowDialog() == true)
            {
                string importRoute = BussinessLogic.LogicInstance.ImportRouteFromFile(openFileDialog.FileName);

                switch(importRoute)
                {
                    case "badFile":
                        MessageBox.Show("Tour should be in .xml format", "Tour Import", MessageBoxButton.OK, MessageBoxImage.Stop);
                        break;
                    case "nameExists":
                        MessageBox.Show("Route with this name already exsits", "Tour Import", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case "done":
                        MessageBox.Show("Route successfully imported", "Tour Import", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
                
        }
        private void delete_Tour(object sender, RoutedEventArgs e)
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
        private void exit_Program(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
