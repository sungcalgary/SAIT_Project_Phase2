using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{   
     /*
     * Author: Chris Earle
     * Created: 2018-07
     */

    [DataObject(true)]
        public static class PurchasesDB
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        // find all the purchases and required details for PurchaseHistory table in the database by Specified CustomerID --Chris
        public static List<Purchases> GetPurchases(int CustomerID)  //customerID is from the logged in session
        {
            List<Purchases> purchaseList = new List<Purchases>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT b.BookingNo, b.TravelerCount, bd.ItineraryNo, " +
                                     "b.BookingDate, bd.TripStart, bd.TripEnd, bd.Description, bd.Destination, " +
                                     "bd.BasePrice, bd.AgencyCommission, f.FeeName, f.FeeAmt " +
                                     "FROM Bookings b " +
                                     "INNER JOIN BookingDetails bd ON bd.BookingId = b.BookingId " +
                                     "INNER JOIN Fees f ON bd.FeeId = bd.FeeId " +
                                     "Where b.CustomerId = "+ CustomerID + " " +
                                     "Order BY b.BookingNo";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Purchases p;
                while (reader.Read()) // while there is a record
                {
                    //purchaseList population
                    p = new Purchases();
                    p.BookingNo = reader["BookingNo"].ToString();
                    p.TravelerCount = (double)reader["TravelerCount"];
                    p.ItineraryNo = (double)reader["ItineraryNo"];
                    p.BookingDate = (DateTime)reader["BookingDate"];
                    p.TripStart = (DateTime)reader["TripStart"];
                    p.TripEnd = (DateTime)reader["TripEnd"];
                    p.Description = reader["Description"].ToString();
                    p.Destination = reader["Destination"].ToString();
                    //add commission to Base Price so customer will not see commission value
                    p.BasePrice = (decimal)reader["BasePrice"] + (decimal)reader["AgencyCommission"];  
                    p.FeeName = reader["FeeName"].ToString();
                    p.FeeAmt = (decimal)reader["FeeAmt"];

                    purchaseList.Add(p);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return purchaseList;
        }
    }
}
