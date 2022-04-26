using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccessLayer
{
    public interface IDatabaseConnection
    {
        SqlConnection Connect();
        SqlConnection Connection();
        void closeConnection();
    }
}
