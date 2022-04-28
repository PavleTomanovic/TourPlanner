using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DTO;

namespace TourPlanner.BussinesLayer
{
    public interface IDocumentCreation
    {
        void RouteReportCreation(HttpResponseDTO HttpResponseDTO);
        void RouteSummarizeReportCreation(double time, double rating, string distance);
    }
}
