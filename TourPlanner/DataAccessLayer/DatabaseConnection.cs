using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TourPlanner.BussinesLayer;
using System.Data;

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
    }
}
