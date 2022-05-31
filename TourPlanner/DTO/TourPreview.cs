using System;

namespace TourPlanner.DTO
{
    public class TourPreviewDTO
    {
        public string TourName { get; set; } = string.Empty;
        public string TourId { get; set; } = string.Empty;
        public override string ToString()
        {
            //.FormatString(this string myString) is an extension.
            string returnString = string.Empty;
            if (TourName != string.Empty)
                returnString = string.Format(TourName);
            return returnString;
        }
    }
}
