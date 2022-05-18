using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourPreview
    {
        public string tourName { get; set; }
        public int tourId { get; set; }
        public override string ToString()
        {
            //.FormatString(this string myString) is an extension.
            string returnString = string.Empty;
            if (this.tourName != string.Empty)
                returnString = String.Format(this.tourName);
            return returnString;
        }
    }
}
