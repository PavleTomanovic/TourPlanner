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
        [JsonProperty("distance")]
        public float Distance { get; set; }
        [JsonProperty("formattedTime")]
        public string FormattedTime { get; set; }
        [JsonProperty("fuelUsed")]
        public float FuelUsed { get; set; }
    }
}
