using System;
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
            var lcv = new LogChangesView();
            lcv.TourID = id;
            this.DataContext = lcv;
            if (lcv.CloseAction == null)
                lcv.CloseAction = new Action(this.Close);
        }
    }
}
