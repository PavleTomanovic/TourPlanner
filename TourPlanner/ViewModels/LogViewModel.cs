using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        public ObservableCollection<TourLogDTO> LogGrid { get; set; }
        private List<TourLogDTO> tourLogDTO { get; set; }
        private ViewModel vm { get; set; }

        public LogViewModel()
        {
            //Create a new Task Record
            //Instatiate a new Task object and give it information


            tourLogDTO = BussinessLogic.LogicInstance.SelectLogForSearch();
            LogGrid = new ObservableCollection<TourLogDTO>();
            tourLogDTO.ToList().ForEach(LogGrid.Add);
            /*    LogGrid = new ObservableCollection<LogRecord>()
                  {
                      new LogRecord(new DateTime(2022, 12, 20, 12, 45, 00),"6","1","5","I have to go there again!"),
                      new LogRecord(new DateTime(2022, 6, 14, 06, 30, 00),"4","2","4","It's alright")
                  };*/

        }
    }
}
