using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DTO
{
    public class HttpDTO
    {
        public string ?Url { get; set; }
        public string ?MapUrl { get; set; }
        public string ?Key { get; set; }
        public string ?From { get; set; }
        public string ?To { get; set; }
    }
}
