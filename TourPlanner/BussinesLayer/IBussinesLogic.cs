using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BussinesLayer
{
    public interface IBussinesLogic
    {
        void CreateRoute(string from, string to, string name, string description, string transport);
        void ModifyRoute(string from, string to, string name, string description, string transport, string routeId);
        void DeleteRoute(string routeId);
        void CreateLog(string comment, string difficulty, string totalTime, string rating, string routeId);
        void ModifyLog(string comment, string difficulty, string totalTime, string rating, string logId);
        void Deletelog(string logId);
        List<String> SelectAllRoutes();
        void CreateRouteReport(string routeId);
        void CreateSummarizeReport(string routeId);
        void ImportRouteFromFile(string filename);
        void ExportRouteToFile(string filename, string routeId);
        void MakeRouteFavorite(string routeId);
        string CheckRoutePopularity(string routeId);
        string CheckRouteChildFriendliness(string routeId);
    }
}
