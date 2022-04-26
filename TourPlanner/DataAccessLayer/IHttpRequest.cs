using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DTO;

namespace TourPlanner.DataAccessLayer
{
    public interface IHttpRequest
    {
        HttpResponseDTO GetRoutes(HttpDTO httpDTO);
        FileInfo GetRouteImage(HttpDTO httpDTO);
    }
}
