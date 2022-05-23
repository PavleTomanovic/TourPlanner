using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
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
