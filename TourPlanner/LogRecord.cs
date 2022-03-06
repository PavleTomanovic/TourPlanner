using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner
{
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
