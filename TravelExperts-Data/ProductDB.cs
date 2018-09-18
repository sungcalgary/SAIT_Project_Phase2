using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{
    /*
     * This file has many authors
     * Author: Chris Earle, Sung, Luke
     * Created: 2018-07
     */
    public static class ProductDB
    {
        // find all the products in the database  --Luke
        public static List<Product> GetProducts()
        {
            List<Product> productList = new List<Product>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Products ORDER BY 1";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Product p;
                while (reader.Read()) // while there is another record
                {
                    p = new Product();
                    p.ProductId = (int)reader["ProductId"];
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

        //Add new product -- Chris
        public static void AddProducts(string prodName)  //prodName specified in frmUpdateProduct
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "INSERT into Products(ProdName) " +  //ProdId auto incriments
                                     "VALUES('" + prodName + "')";
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

        //add suppliers to a product when editing product in frmUpdateProduct --Chris
        //This may make more sense in Products_SuppliersDB but for now will stay here --Chris
        public static void AddSuppliersToProduct(string prodName, string supName)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "INSERT INTO Products_Suppliers(ProductId, SupplierId) " +
                                     "VALUES(" +
                                        "(SELECT ProductId FROM Products WHERE ProdName='" + prodName + "'), " +
                                        "(SELECT SupplierID from Suppliers " +
                                        "WHERE SupName = '" + supName + "'))"
                                     ;

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


        // find all the Suppliers in the database for a specific product
        //pass Product ID to this  --Chris
        public static List<Product> GetProductsBySupplier(int prodID)
        {
            List<Product> productList = new List<Product>(); // empty
            Product p = null;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT SupName FROM Products prd " +
                                     "Inner join Products_Suppliers ps on prd.ProductId = ps.ProductId " +
                                     "Inner join Suppliers s on s.SupplierId = ps.SupplierId " +
                                     "WHERE prd.ProductId = @ProductId " +
                                     "ORDER BY SupName";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductId", prodID);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // while there is a record
                {
                    //populate product list
                    p = new Product();
                    p.SupName = reader["SupName"].ToString();

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


        //will get all the suppliers NOT having a specfic product  --Chris
        //pass productId to this
        public static List<Product> GetSuppliersNotForProduct(int prodID)
        {
            List<Product> productList = new List<Product>(); // empty
            Product p = null;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT SupName from Suppliers WHERE SupName NOT IN " +
                                     "(SELECT SupName FROM Products prd " +
                                        "INNER JOIN Products_Suppliers ps ON prd.ProductId = ps.ProductId " +
                                        "INNER JOIN Suppliers s ON s.SupplierId = ps.SupplierId " +
                                        "WHERE prd.ProductId = "+prodID+") " +
                                     "ORDER BY SupName";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductId", prodID);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // while there is another record
                {
                    //populate product list
                    p = new Product();
                    p.SupName = reader["SupName"].ToString();

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

        // returns a list of ids of products in the package -- Sung
        
        public static List<int> GetProductIds(int pkgId)
        {//Sunghyun Lee
            List<int> productIdList = new List<int>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT pkg.PackageId, ProductId " +
                                     "FROM Packages pkg " +
                                     "INNER JOIN Packages_Products_Suppliers pps ON pkg.PackageId = pps.PackageId " +
                                     "INNER JOIN Products_Suppliers ps ON pps.ProductSupplierId = ps.ProductSupplierId " +
                                     "WHERE pkg.PackageId=@PackageId";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@PackageId", pkgId);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) // while there is another record
                {

                    int ProductId = (int)reader["ProductId"];


                    productIdList.Add(ProductId);
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

            return productIdList;
        }
    }
}
