using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TourPlanner.BussinesLayer;
using System.Data;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.DataAccessLayer
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private static DatabaseConnection instance = new DatabaseConnection();
        private SqlConnection connection;

        public static DatabaseConnection Instance
        {
            get
            {
                return instance;
            }
        }

        //CONNECTION
        public SqlConnection Connect()
        {
            try
            {
                string dataSource = BussinessFactory.Instance.DatabaseDTO.Source;
                string catalog = BussinessFactory.Instance.DatabaseDTO.InitialCatalog;
                string user = BussinessFactory.Instance.DatabaseDTO.UserName;
                string password = BussinessFactory.Instance.DatabaseDTO.Password;

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    return connection;
                }
                else
                {
                    string connetionString = "Data Source=" + dataSource + ";Initial Catalog=" + catalog + ";User ID=" + user + ";Password=" + password + ";";
                    connection = new SqlConnection(connetionString);
                    connection.Open();

                    return connection;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Datenbankverbindung konnte nicht erstellt werden: \n" + ex.Message);
            }
        }
        public SqlConnection Connection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                return connection;
            }
            else
            {
                return Connect();
            }
        }
        public void closeConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception) { }
            }
        }

        //FUNCTIONS


        public void ExecuteInsertRoute(string query, HttpResponseDTO HttpResponseDTO, string imageUrl)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", HttpResponseDTO.Route.Name);
                    command.Parameters.AddWithValue("@P2", HttpResponseDTO.Route.Description);
                    command.Parameters.AddWithValue("@P3", HttpResponseDTO.Route.From);
                    command.Parameters.AddWithValue("@P4", HttpResponseDTO.Route.To);
                    command.Parameters.AddWithValue("@P5", HttpResponseDTO.Route.Transport);
                    command.Parameters.AddWithValue("@P6", HttpResponseDTO.Route.Distance);
                    command.Parameters.AddWithValue("@P7", HttpResponseDTO.Route.FormattedTime.ToString());
                    command.Parameters.AddWithValue("@P8", imageUrl);

                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                try { command.Dispose(); }
                catch { }
            }
        }

        public void ExecuteUpdateRoute(string query, HttpResponseDTO HttpResponseDTO, string imageUrl, string routeId)
        {

            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", HttpResponseDTO.Route.Name);
                    command.Parameters.AddWithValue("@P2", HttpResponseDTO.Route.Description);
                    command.Parameters.AddWithValue("@P3", HttpResponseDTO.Route.From);
                    command.Parameters.AddWithValue("@P4", HttpResponseDTO.Route.To);
                    command.Parameters.AddWithValue("@P5", HttpResponseDTO.Route.Transport);
                    command.Parameters.AddWithValue("@P6", HttpResponseDTO.Route.Distance);
                    command.Parameters.AddWithValue("@P7", HttpResponseDTO.Route.FormattedTime.ToString());
                    command.Parameters.AddWithValue("@P8", imageUrl);
                    command.Parameters.AddWithValue("@P9", routeId);

                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                try { command.Dispose(); }
                catch { }
            }
        }

        public void ExecuteDeleteRoute(string query, string tourId)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", tourId);

                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                try { command.Dispose(); }
                catch { }
            }
        }
    }
}
