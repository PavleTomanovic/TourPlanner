using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DTO
{
    public class SqlDTO
    {
        public string? Insert { get; set; }
        public string? SelectAll { get; set; }
        public string? Delete { get; set; }
        public string? Update { get; set; }
        public string? UpdateFavorite { get; set; }
        public string? InsertLog { get; set; }
        public string? DeleteLog { get; set; }
        public string? UpdateLog { get; set; }
        public string? SelectRoute { get; set; }
        public string? SelectLogReport { get; set; }
    }
}
