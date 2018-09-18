using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{
    /*
     * Author: Sunghyun Lee
     * Created: 2018-07
     */
    
    public static class TravelExpertsDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source = localhost\sqlexpress; Initial Catalog = TravelExperts; Integrated Security = True";
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}
