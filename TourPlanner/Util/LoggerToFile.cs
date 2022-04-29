using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BussinesLayer;

namespace TourPlanner.Util
{
    public class LoggerToFile
    {
        public static void LogError(string message)
        {
            string fileName = BussinessFactory.Instance.DirectoryDTO.LogPath + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
            StreamWriter file = new StreamWriter(fileName, true);
            file.Write(DateTime.Now + "\t");
            file.WriteLine(message);
            file.Close();
        }
    }
}
