using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Luke, Sung

namespace TravelExperts_Data
{
    public static class SupplierDB
    {
        // find all the suppliers in the database
        public static List<Supplier> GetSupplier()
        {
            List<Supplier> supplierList = new List<Supplier>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Suppliers ORDER BY SupName";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Supplier sup;
                while (reader.Read()) // while there is another record
                {
                    sup = new Supplier();
                    sup.SupplierId = (int)reader["SupplierId"];
                    sup.SupName = reader["SupName"].ToString();
                    supplierList.Add(sup);
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
            return supplierList;
        }

        // --start Sung--
        public static List<Supplier> GetProductSupplier(int prodid)
        {
            List<Supplier> supplierList = new List<Supplier>();
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "select s.SupplierId ,supName , ProductID " +
                                     "from suppliers s inner join Products_Suppliers ps on s.SupplierId = ps.SupplierId " +
                                      "where ProductID=@ProductID";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductID", prodid);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Supplier s;
                while (reader.Read()) // while there is another record
                {
                    s = new Supplier();
                    s.SupplierId = (int)reader["SupplierId"];
                    s.SupName = reader["SupName"].ToString();
                    supplierList.Add(s);
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
            return supplierList;
        }
        // --end Sung--

        //method to get new supplierID, find max exist ID#, get new supplierID# = max# + 1
        public static int GetNewSupplierId()
        {
            int supplierid = 0; // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();            

            string selectStatement = "select max(SupplierID) as SupplierId from Suppliers";            

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) // while there is another record
                {
                    supplierid = (int)reader["SupplierId"]+1;              
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
            return supplierid;
        }

        //  Update Supplier
        public static bool UpdateSupplier(Supplier oldsup, Supplier newsup)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string updateStatement = "UPDATE Suppliers " +
                                     "SET SupName = @NewSupName, " +
                                     "WHERE SupplierId = @OldSupplierId ";
            SqlCommand cmd = new SqlCommand(updateStatement, con);
            cmd.Parameters.AddWithValue("@NewSupName", newsup.SupName);
            cmd.Parameters.AddWithValue("@OldSupplierId", oldsup.SupplierId);

            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0) return true;
                else return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        
        // Add new supplier
        public static void AddSupplier(Supplier sup)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectStatement = "INSERT INTO Suppliers(SupplierId,SupName) " +
                                     "VALUES(@SupplierId, @SupName)";
            
            SqlCommand cmd = new SqlCommand(selectStatement, con);

            cmd.Parameters.AddWithValue("@SupplierId", sup.SupplierId);
            cmd.Parameters.AddWithValue("@SupName", sup.SupName);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery(); // run the insert command
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }        

        // Add products to supplier
        public static void AddProductsToSupplier(string supName, string prodName)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "INSERT INTO Products_Suppliers(ProductId, SupplierId) " +
                                     "VALUES((SELECT ProductId from Products WHERE ProdName = '" + prodName + "'), (SELECT SupplierId FROM Suppliers WHERE SupName = '" + supName + "'))";
            
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

        // find all the Products joined with products_suppliers in the database
        // get exsiting products from supplier
        public static List<Product> GetProductsFromSupplier(int prodID)
        {
            List<Product> productList = new List<Product>(); // empty
            Product p = null;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT prodName FROM Products p " +
                                     "Inner join Products_Suppliers ps on p.ProductId = ps.ProductId " +
                                     "Inner join Suppliers s on s.SupplierId = ps.SupplierId " +
                                     "WHERE s.SupplierId = @SupplierId " +
                                     "ORDER BY prodName";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductId", prodID);
            cmd.Parameters.AddWithValue("@SupplierId", prodID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // while there is another record
                {
                    p = new Product();
                    
                    p.ProdName = reader["ProdName"].ToString();                    

                    productList.Add(p);
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
            return productList;
        }

        // get all the products NOT having for a supplier
        public static List<Product> GetProductsNotFromSupplier(int prodID)
        {
            List<Product> productList = new List<Product>(); // empty
            Product p = null;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT ProdName from Products p WHERE ProdName NOT IN " +
                                     "(SELECT ProdName FROM Products p " +
                                     "INNER JOIN Products_Suppliers ps ON p.ProductId = ps.ProductId " +
                                     "INNER JOIN Suppliers s ON s.SupplierId = ps.SupplierId " +
                                     "WHERE s.SupplierId = @SupplierId) " +
                                     "ORDER BY ProdName";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductId", prodID);
            cmd.Parameters.AddWithValue("@SupplierId", prodID);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // while there is another record
                {
                    p = new Product();
                    
                    p.ProdName = reader["ProdName"].ToString();                    

                    productList.Add(p);
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
            return productList;
        }
    }
}
