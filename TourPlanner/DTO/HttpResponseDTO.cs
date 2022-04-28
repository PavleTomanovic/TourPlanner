using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DTO
{
    public class HttpResponseDTO
    {
        [JsonProperty("route")]
        public RouteDTO Route { get; set; }
    }

    public class RouteDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Transport { get; set; }
        [JsonProperty("distance")]
        public string Distance { get; set; }
        [JsonProperty("formattedTime")]
        public string FormattedTime { get; set; }
        public string ImageUrl { get; set; }
    }
}
