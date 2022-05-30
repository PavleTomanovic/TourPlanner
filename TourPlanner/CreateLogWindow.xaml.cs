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
        public CreateLogWindow(string id, ViewModel vm)
        {
            InitializeComponent();
            var lcv = new LogChangesView();
            lcv.viewModel = vm;
            lcv.TourID = id;
            lcv.Date = DateTime.Now.Date;
            lcv.Time = DateTime.Now.ToString("HH:mm");
            this.DataContext = lcv;
            if (lcv.CloseAction == null)
                lcv.CloseAction = new Action(this.Close);
        }
    }
}
