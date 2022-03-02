using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlanner
{
    public class TaskOverviewViewModel
    {

        public TaskOverviewViewModel()
        {
            //Create a new Task Record
            //Instatiate a new Task object and give it information
            LogRecord taskone = new LogRecord();

            taskone.date = new DateTime(2022, 12, 20, 12, 45, 00);
            taskone.duration = "6";
            taskone.difficulty = "1";
            taskone.rating = "5";
            taskone.comment = "I have to go there again!";

            TourLogXAML.Items.Add(taskone);
        }
        public class LogRecord
        {
            public string recordID { get; set; }
            public DateTime date { get; set; }
            public string duration { get; set; }        //days
            public string difficulty { get; set; }      //from one one to five
            public string rating { get; set; }          //five star rating
            public string comment { get; set; }

        }
    }
}
