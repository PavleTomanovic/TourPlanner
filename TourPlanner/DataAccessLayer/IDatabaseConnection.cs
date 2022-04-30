using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DTO;

namespace TourPlanner.DataAccessLayer
{
    public interface IDatabaseConnection
    {
        SqlConnection Connect();
        SqlConnection Connection();
        void closeConnection();
        void ExecuteInsertRoute(string query, HttpResponseDTO HttpResponseDTO);
        void ExecuteUpdateRoute(string query, HttpResponseDTO HttpResponseDTO);
        void ExecuteDeleteRoute(string query, string tourId);
        void ExecuteInsertLog(string query, TourLogDTO tourLogDTO, string routeId);
        void ExecuteDeleteLog(string query, string logId);
        void ExecuteModifyLog(string query, TourLogDTO tourLogDTO);
        DataTable ExecuteSelect(string query, string id);
        void ExecuteFavorite(string query, string routeId);
    }
}
