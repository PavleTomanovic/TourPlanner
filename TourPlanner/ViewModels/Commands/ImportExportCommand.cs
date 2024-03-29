﻿using Microsoft.Win32;
using System.Windows;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class ImportCommand : CommandBase
    {
        private ViewModel viewModel { get; set; }
        public ImportCommand(ViewModel vm)
        {
            this.viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = @"C:\Temp\TourPlanner\TemplateInsert\";
            if (openFileDialog.ShowDialog() == true)
            {
                UIServices.SetBusyState();
                string importRoute = BussinessLogic.LogicInstance.ImportRouteFromFile(openFileDialog.FileName);

                switch (importRoute)
                {
                    case "badFile":
                        MessageBox.Show("Tour should be in .xml format!", "Tour Import", MessageBoxButton.OK, MessageBoxImage.Stop);
                        break;
                    case "nameExists":
                        MessageBox.Show("Route with this name already exsits!", "Tour Import", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case "done":
                        MessageBox.Show("Route successfully imported!", "Tour Import", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.viewModel.updateTourList();
                        break;
                }
            }
        }
    }
    public class ExportCommand : CommandBaseOnChange
    {
        public ExportCommand(ViewModel viewModel) : base(viewModel) { }
        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = @"C:\Temp\TourPlanner\Export\";
            if (saveFileDialog.ShowDialog() == true)
            {
                bool exportRoute = BussinessLogic.LogicInstance.ExportRouteToFile(saveFileDialog.FileName, parameter.ToString());
                if (exportRoute)
                    MessageBox.Show("Tour successfully exported!", "Tour Export", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Route is not exported, check log file for more!", "Tour Export", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
