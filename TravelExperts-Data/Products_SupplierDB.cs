using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{
     /*
     * Authors: Chris Earle, Luke
     * Created: 2018-07
     */
    public static class Products_SupplierDB
    {
        // find all the product_suppliers in the database --Luke
        public static List<Products_Supplier> GetProducts_Supplier()
        {
            List<Products_Supplier> products_supplierList = new List<Products_Supplier>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Products_Suppliers ORDER BY 1";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Products_Supplier ps;
                while (reader.Read()) // while there is another record
                {
                    ps = new Products_Supplier();
                    ps.ProductSupplierId = (int)reader["ProductSupplierId"];
                    ps.ProductId = (int)reader["ProductId"];
                    ps.SupplierId = (int)reader["SupplierId"];

                    products_supplierList.Add(ps);
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

            return products_supplierList;
        }

        //Removes supplier from a specified product this is removed from Product_Suppliers Table--Chris
        public static void RemoveSuppliersFromProduct(int prodID, string supName)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "DELETE FROM Products_Suppliers " +
                                     "WHERE ProductId ='" + prodID + "' " +
                                     "AND SupplierId = (SELECT SupplierId FROM Suppliers " +
                                        "WHERE SupName ='" + supName + "')";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery(); // run the insert command
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // Luke
        public static void RemoveProductsFromSupplier(int supID, string prodName)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "DELETE FROM Products_Suppliers " +
                                     "WHERE SupplierId ='" + supID + "' " +
                                     "AND ProductId = (SELECT ProductId FROM Products " +
                                        "WHERE prodName ='" + prodName + "')";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery(); // run the insert command
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
