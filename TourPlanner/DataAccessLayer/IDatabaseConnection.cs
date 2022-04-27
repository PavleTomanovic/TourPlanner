using System;
using System.Collections.Generic;
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
        void ExecuteInsertRoute(string query, HttpResponseDTO HttpResponseDTO, string imageUrl);
        public void ExecuteUpdateRoute(string query, HttpResponseDTO HttpResponseDTO, string imageUrl, string routeId);
        void ExecuteDeleteRoute(string query, string tourId);
    }
}
