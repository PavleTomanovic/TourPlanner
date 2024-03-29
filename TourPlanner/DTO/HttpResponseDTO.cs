﻿using Newtonsoft.Json;

namespace TourPlanner.DTO
{
    public class HttpResponseDTO
    {
        [JsonProperty("route")]
        public RouteDTO Route { get; set; } = new RouteDTO();
    }

    public class RouteDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Transport { get; set; } = string.Empty;
        [JsonProperty("distance")]
        public string Distance { get; set; } = string.Empty;
        [JsonProperty("time")]
        public string Time { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Favorite { get; set; } = string.Empty;
    }
}
