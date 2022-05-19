using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.BussinesLayer
{
    public class BussinessFactory
    {
        private XMLReader xmlReader;

        private static BussinessFactory instance = new BussinessFactory();
        public DatabaseDTO DatabaseDTO { get; set; }
        public HttpDTO HttpDTO { get; set; }
        public SqlDTO SqlDTO { get; set; }
        public DirectoryDTO DirectoryDTO { get; set; }

        public BussinessFactory()
        {
            try
            {
                string? path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string file = new Uri(path + @"\Resources\" + "Settings.xml").LocalPath;
                xmlReader = new XMLReader(file);
                ReadValues();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim Initialisieren.\n" + e.Message);
                Environment.Exit(-1);
            }
        }

        private void ReadValues()
        {
            ReadDatabase();
            ReadHttp();
            ReadSql();
            ReadDirectory();
        }
        private void ReadDatabase()
        {
            DatabaseDTO = new DatabaseDTO();
            /*
            DatabaseDTO.Source = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Pavle/Source"));
            DatabaseDTO.InitialCatalog = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Pavle/InitialCatalog"));
            DatabaseDTO.UserName = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Pavle/UserName"));
            DatabaseDTO.Password = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Pavle/Password"));
            */
            DatabaseDTO.Source = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Taha/Source"));
            DatabaseDTO.InitialCatalog = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Taha/InitialCatalog"));
            DatabaseDTO.UserName = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Taha/UserName"));
            DatabaseDTO.Password = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database_Taha/Password"));
        }

        private void ReadHttp()
        {
            HttpDTO = new HttpDTO();

            HttpDTO.Url = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/HttpRequest/Url"));
            HttpDTO.MapUrl = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/HttpRequest/MapUrl"));
            HttpDTO.Key = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/HttpRequest/Key"));
        }

        private void ReadSql()
        {
            SqlDTO = new SqlDTO();

            SqlDTO.Insert = xmlReader.getSingleNodeString("SWEN/Sql/Insert");
            SqlDTO.SelectAll = xmlReader.getSingleNodeString("SWEN/Sql/SelectAll");
            SqlDTO.Delete = xmlReader.getSingleNodeString("SWEN/Sql/Delete");
            SqlDTO.Update = xmlReader.getSingleNodeString("SWEN/Sql/Update");
            SqlDTO.UpdateFavorite = xmlReader.getSingleNodeString("SWEN/Sql/UpdateFavorite");
            SqlDTO.InsertLog = xmlReader.getSingleNodeString("SWEN/Sql/InsertLog");
            SqlDTO.DeleteLog = xmlReader.getSingleNodeString("SWEN/Sql/DeleteLog");
            SqlDTO.UpdateLog = xmlReader.getSingleNodeString("SWEN/Sql/UpdateLog");
            SqlDTO.SelectRoute = xmlReader.getSingleNodeString("SWEN/Sql/SelectRoute");
            SqlDTO.SelectLogReport = xmlReader.getSingleNodeString("SWEN/Sql/SelectLogReport");
            SqlDTO.SelectAllRoutes = xmlReader.getSingleNodeString("SWEN/Sql/SelectAllRoutes");
            SqlDTO.SelectAllLogs = xmlReader.getSingleNodeString("SWEN/Sql/SelectAllLogs");
        }

        private void ReadDirectory()
        {
            DirectoryDTO = new DirectoryDTO();

            DirectoryDTO.LogPath = xmlReader.getSingleNodeString("SWEN/Directory/LogPath");
            DirectoryDTO.ReportPath = xmlReader.getSingleNodeString("SWEN/Directory/ReportPath");
            DirectoryDTO.ImagesPath = xmlReader.getSingleNodeString("SWEN/Directory/ImagesPath");
            DirectoryDTO.ExportPath = xmlReader.getSingleNodeString("SWEN/Directory/ExportPath");
        }

        public static BussinessFactory Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
