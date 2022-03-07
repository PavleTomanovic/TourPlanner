using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LogRecord> LogRecords { get; set; }
    }
    public class LogRecord
    {
        public LogRecord(DateTime date, string durration, string difficulty, string rating, string comment)
        {
            Date = date;
            Duration = durration;
            Difficulty = difficulty;
            Rating = rating;
            Comment = comment;
        }
        // public string recordID { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }        //days
        public string Difficulty { get; set; }      //from one one to five
        public string Rating { get; set; }          //five star rating
        public string Comment { get; set; }


    }
}
