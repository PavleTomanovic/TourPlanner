using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace TourPlanner
{
    public class MainWindowViewModel
    {
        //Dont bind the GUI elements directly with the ViewModel
        public ObservableCollection<LogRecord> record { get; set; }
        public MainWindowViewModel()
        {
            //Create a new Task Record
            //Instatiate a new Task object and give it information
            record = new ObservableCollection<LogRecord>()
            {
                new LogRecord()
                {
                    recordID = "1",
                    date = new DateTime(2022, 12, 20, 12, 45, 00),
                    duration = "6",
                    difficulty = "1",
                    rating = "5",
                    comment = "I have to go there again!"
                }
            };

        }
        /*private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            record.Add(new LogRecord() { comment = "yess", date = DateTime.Now });
        }*/

    }
}
