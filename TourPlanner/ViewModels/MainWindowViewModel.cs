using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public void OnProbertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /* public ObservableCollection<LogRecord> Record { get; set; }

         public MainWindowViewModel()
         {
             //Create a new Task Record
             //Instatiate a new Task object and give it information

             Record = new ObservableCollection<LogRecord>()
              {
                  new LogRecord(new DateTime(2022, 12, 20, 12, 45, 00),"6","1","5","I have to go there again!"),
                  new LogRecord(new DateTime(2022, 6, 14, 06, 30, 00),"4","2","4","It's alright")

              };

         }*/
    }
}
