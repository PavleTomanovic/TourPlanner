using System;

namespace TourPlanner.Models
{
    public class TourPreview
    {
        public string tourName { get; set; } = string.Empty;
        public string tourId { get; set; } = string.Empty;
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
