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
using TourPlanner.BussinesLayer;

namespace TourPlanner.DataAccessLayer
{
    public class HttpRequest
    {
        private static HttpRequest instance = new HttpRequest();
        public static HttpRequest Instance
        {
            get
            {
                return instance;
            }
        }
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
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
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
                    HttpResponseMessage response = client.GetAsync("?start=" + httpDTO.From + "&end=" + httpDTO.To + "&size=600,400&key=" + httpDTO.Key).Result;
                    response.EnsureSuccessStatusCode();

                    byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                    string image = BussinessFactory.Instance.DirectoryDTO.ImagesPath + DateTime.Now.ToString("hhmmssffffff") + ".jpg";
                    File.WriteAllBytes(image, result);

                    return image;
                }
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return null;
            }
        }
    }
}
