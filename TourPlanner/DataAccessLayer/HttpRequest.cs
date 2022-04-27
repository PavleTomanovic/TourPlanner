using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.DataAccessLayer
{
    public class HttpRequest : IHttpRequest
    {
        public HttpResponseDTO GetRoutes(HttpDTO httpDTO)
        {
            HttpResponseDTO responseDTO = new HttpResponseDTO();

            try
            {
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    client.BaseAddress = new Uri(httpDTO.Url);
                    HttpResponseMessage response = client.GetAsync("?from=" + httpDTO.From + "&to=" + httpDTO.To + "&key=" + httpDTO.Key).Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseDTO = JsonConverter.ConvertFromJson<HttpResponseDTO>(result);
                    return responseDTO;
                }
            }
            catch 
            {
                throw new Exception("GetRoutes function not working");
                return responseDTO;
            }
        }

        public string GetRouteImage(HttpDTO httpDTO)
        {
            try
            {
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    client.BaseAddress = new Uri(httpDTO.MapUrl);
                    HttpResponseMessage response = client.GetAsync("?start=" + httpDTO.From + "&end=" + httpDTO.To + "&routeArc=true&size=600,400@2x&key=" + httpDTO.Key).Result;
                    response.EnsureSuccessStatusCode();

                    byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                    Console.WriteLine(result);
                    string image = @"C:\Temp\TourPlanner\TourImages\image" + DateTime.Now.ToString("hhmmssffffff") + ".jpg";
                    File.WriteAllBytes(image, result);

                    return image;
                }
            }
            catch
            {
                throw new Exception("GetRouteImage function not working");
            }
        }
    }
}
