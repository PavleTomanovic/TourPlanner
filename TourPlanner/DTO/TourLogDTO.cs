using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DTO
{
    public class TourLogDTO
    {
        public string? LogId { get; set; }
        public string? DateTime { get; set; }
        public string? Comment { get; set; }
        public string? Difficulty { get; set; }
        public string? TotalTime { get; set; }
        public string? Rating { get; set; }
    }
}
