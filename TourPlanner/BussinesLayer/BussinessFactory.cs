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

        public BussinessFactory()
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string file = new Uri(path + @"\Resource\" + "Settings.xml").LocalPath;
                xmlReader = new XMLReader(file);
                readValues();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim Initialisieren.\n" + e.Message);
                Environment.Exit(-1);
            }
        }

        private void readValues()
        {
            readDatabase();
            readHttp();
            readSql();
        }
        private void readDatabase()
        {
            DatabaseDTO = new DatabaseDTO();

            DatabaseDTO.Source = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database/Source"));
            DatabaseDTO.InitialCatalog = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database/InitialCatalog"));
            DatabaseDTO.UserName = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database/UserName"));
            DatabaseDTO.Password = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/Database/Password"));
        }

        private void readHttp()
        {
            HttpDTO = new HttpDTO();

            HttpDTO.Url = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/HttpRequest/Url"));
            HttpDTO.MapUrl = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/HttpRequest/MapUrl"));
            HttpDTO.Key = Crypto.decrypt(xmlReader.getSingleNodeString("SWEN/HttpRequest/Key"));
        }

        private void readSql()
        {
            SqlDTO = new SqlDTO();

            SqlDTO.Insert = xmlReader.getSingleNodeString("SWEN/Sql/Insert");
            SqlDTO.SelectAll = xmlReader.getSingleNodeString("SWEN/Sql/SelectAll");
            SqlDTO.Delete = xmlReader.getSingleNodeString("SWEN/Sql/Delete");
            SqlDTO.Update = xmlReader.getSingleNodeString("SWEN/Sql/Update");
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
