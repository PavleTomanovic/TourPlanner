using System;
using System.Globalization;
using System.Windows;
using TourPlanner.DTO;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for EditLogWindow.xaml
    /// </summary>
    public partial class EditLogWindow : Window
    {
        public EditLogWindow(TourLogDTO tourLogDTO)
        {
            InitializeComponent();
            var lcv = new LogChangesView();
            lcv.TourID = tourLogDTO?.RouteId;
            lcv.LogID = tourLogDTO?.LogId;
            lcv.Date = DateTime.Parse(tourLogDTO?.DateTime);
            lcv.Time = DateTime.Parse(tourLogDTO?.DateTime).ToString("HH:mm");
            lcv.LogComment = tourLogDTO?.Comment;
            lcv.Difficulty = tourLogDTO?.Difficulty;
            lcv.TotalTime = tourLogDTO?.TotalTime;
            lcv.Rating = tourLogDTO?.Rating;
            this.DataContext = lcv;
            if (lcv.CloseAction == null)
                lcv.CloseAction = new Action(this.Close);
        }
    }
}
