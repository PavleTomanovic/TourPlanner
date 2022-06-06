using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.DataAccessLayer
{
    public class DatabaseConnection
    {
        private static DatabaseConnection instance = new DatabaseConnection();
        private SqlConnection connection;
        ILog log = LogManager.GetLogger(typeof(App));

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
                log.Error("Database Connection Error" + ex.Message + " - " + ex.StackTrace);
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


        public void ExecuteInsertRoute(string query, HttpResponseDTO HttpResponseDTO)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", HttpResponseDTO.Route.Name);
                    command.Parameters.AddWithValue("@P2", HttpResponseDTO.Route.Comment);
                    command.Parameters.AddWithValue("@P3", HttpResponseDTO.Route.From);
                    command.Parameters.AddWithValue("@P4", HttpResponseDTO.Route.To);
                    command.Parameters.AddWithValue("@P5", HttpResponseDTO.Route.Transport);
                    command.Parameters.AddWithValue("@P6", HttpResponseDTO.Route.Distance);
                    command.Parameters.AddWithValue("@P7", HttpResponseDTO.Route.Time.ToString());
                    command.Parameters.AddWithValue("@P8", HttpResponseDTO.Route.ImageUrl);

                    int result = command.ExecuteNonQuery();
                }
                log.Debug("ExecuteInsertRoute done");
            }
            catch (Exception e)
            {
                log.Error("ExecuteInsertRoute Error" + e.Message + " - " + e.StackTrace);
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
                catch { }
            }
        }

        public void ExecuteUpdateRoute(string query, HttpResponseDTO HttpResponseDTO)
        {

            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", HttpResponseDTO.Route.Name);
                    command.Parameters.AddWithValue("@P2", HttpResponseDTO.Route.Comment);
                    command.Parameters.AddWithValue("@P3", HttpResponseDTO.Route.From);
                    command.Parameters.AddWithValue("@P4", HttpResponseDTO.Route.To);
                    command.Parameters.AddWithValue("@P5", HttpResponseDTO.Route.Transport);
                    command.Parameters.AddWithValue("@P6", HttpResponseDTO.Route.Distance);
                    command.Parameters.AddWithValue("@P7", HttpResponseDTO.Route.Time);
                    command.Parameters.AddWithValue("@P8", HttpResponseDTO.Route.ImageUrl);
                    command.Parameters.AddWithValue("@P9", HttpResponseDTO.Route.Id);

                    int result = command.ExecuteNonQuery();
                }
                log.Debug("ExecuteUpdateRoute done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteUpdateRoute Error" + e.Message + " - " + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
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
                log.Debug("ExecuteDeleteRoute done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteDeleteRoute Error" + e.Message + " - " + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
                catch { }
            }
        }

        public void ExecuteInsertLog(string query, TourLogDTO tourLogDTO, string routeId)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", tourLogDTO.DateTime);
                    command.Parameters.AddWithValue("@P2", tourLogDTO.Comment);
                    command.Parameters.AddWithValue("@P3", tourLogDTO.Difficulty);
                    command.Parameters.AddWithValue("@P4", tourLogDTO.TotalTime);
                    command.Parameters.AddWithValue("@P5", tourLogDTO.Rating);
                    command.Parameters.AddWithValue("@P6", routeId);

                    int result = command.ExecuteNonQuery();
                }
                log.Debug("ExecuteInsertLog done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteInsertLog Error" + e.Message + " - " + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
                catch { }
            }
        }

        public void ExecuteModifyLog(string query, TourLogDTO tourLogDTO)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", tourLogDTO.Comment);
                    command.Parameters.AddWithValue("@P2", tourLogDTO.Difficulty);
                    command.Parameters.AddWithValue("@P3", tourLogDTO.TotalTime);
                    command.Parameters.AddWithValue("@P4", tourLogDTO.Rating);
                    command.Parameters.AddWithValue("@P5", tourLogDTO.LogId);
                    command.Parameters.AddWithValue("@P6", tourLogDTO.RouteId);
                    command.Parameters.AddWithValue("@P7", tourLogDTO.DateTime);

                    int result = command.ExecuteNonQuery();
                }
                log.Debug("ExecuteModifyLog done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteModifyLog Error" + e.Message + " - " + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
                catch { }
            }
        }

        public void ExecuteDeleteLog(string query, string routeId, string logId)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", routeId);
                    command.Parameters.AddWithValue("@P2", logId);

                    int result = command.ExecuteNonQuery();
                }
                log.Debug("ExecuteDeleteLog done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteDeleteLog Error" + e.Message + " - " + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
                catch { }
            }
        }

        public DataTable ExecuteSelectAllRoutes(string query)
        {
            SqlDataReader reader = null;
            SqlCommand command = null;
            DataTable results = null;

            try
            {
                results = new DataTable("Results");
                command = new SqlCommand(query, Instance.Connection());
                reader = command.ExecuteReader();
                results.Load(reader);

                log.Debug("ExecuteSelectAllRoutes done");

                return results;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteSelectAllRoutes Error" + e.Message + " - " + e.StackTrace);
                return results;
            }
            finally
            {
                try { reader?.Close(); }
                catch { }

                try { command?.Dispose(); }
                catch { }
            }
        }

        public DataTable ExecuteSelect(string query, string id)
        {
            SqlDataReader reader = null;
            SqlCommand command = null;
            DataTable results = null;

            try
            {
                results = new DataTable("Results");
                command = new SqlCommand(query, Instance.Connection());
                command.Parameters.AddWithValue("@P1", id);
                reader = command.ExecuteReader();
                results.Load(reader);

                log.Debug("ExecuteSelect done");

                return results;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);

                log.Error("ExecuteSelect Error" + e.Message + " - " + e.StackTrace);

                return results;
            }
            finally
            {
                try { reader?.Close(); }
                catch { }

                try { command?.Dispose(); }
                catch { }
            }
        }

        public void ExecuteFavorite(string query, string routeId)
        {
            SqlCommand command = null;

            try
            {
                using (command = Connection().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@P1", routeId);

                    int result = command.ExecuteNonQuery();
                }
                log.Debug("ExecuteFavorite done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExecuteFavorite Error" + e.Message + " - " + e.StackTrace);
            }
            finally
            {
                try { command?.Dispose(); }
                catch { }
            }
        }
    }
}
