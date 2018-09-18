using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{
    /*
     * This file has 1 author
     * Author: Sunghyun Lee
     * Created: 2018-07
     */
    public static class PackageDB
    {

        public static List<Package> GetPackages()
        {
            List<Package> packageList = new List<Package>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Packages ORDER BY 1";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Package pkg;
                while (reader.Read()) // while there is another record
                {
                    pkg = new Package();
                    pkg.PackageId = (int)reader["PackageID"];
                    pkg.PkgName = reader["PkgName"].ToString();
                    pkg.PkgStartDate = (DateTime)reader["PkgStartDate"];
                    pkg.PkgEndDate = (DateTime)reader["PkgEndDate"];
                    pkg.PkgDesc = reader["PkgDesc"].ToString();
                    pkg.PkgBasePrice = Convert.ToDouble(reader["PkgBasePrice"]);
                    pkg.PkgAgencyCommission = Convert.ToDouble(reader["PkgAgencyCommission"]);
                    pkg.ProductIds = ProductDB.GetProductIds(pkg.PackageId);

                    packageList.Add(pkg);
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

            return packageList;
        }
        // add a product into a package in DB
        public static void AddProductToPackage(int pkgId, int prodId, int supid)
        {
            
            int ProductSupplierId = 0;

            // finds ProductSupplierId

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Products_Suppliers " +
                                     "where ProductId=@ProductId and SupplierId=@SupplierId";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductId", prodId);
            cmd.Parameters.AddWithValue("@SupplierId", supid);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read()) // if there is a record
                {                   
                    ProductSupplierId = (int)reader["ProductSupplierId"];
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

            // inserts a new record into Packages_Product_Suppliers

            string insertStatement = "INSERT INTO Packages_Products_Suppliers (PackageID, ProductSupplierId) " +
                                     "VALUES(@PackageID, @ProductSupplierId)";
            cmd = new SqlCommand(insertStatement, connection);

            cmd.Parameters.AddWithValue("@PackageID", pkgId);
            cmd.Parameters.AddWithValue("@ProductSupplierId", ProductSupplierId);
            

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
        public static void RemoveProductFromPackage(int pkgId, int prodId)
        {
            List<int> psIds = new List<int>(); // productsupplierid's that belongs to the package


            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Packages_Products_Suppliers " +
                                     "where PackageId=@PackageId";

            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@PackageID", pkgId);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int psid; // temporary productsupplierid that will store a productsupplierid from DB
                while (reader.Read()) // while there is another record
                {

                    psid = (int)reader["ProductSupplierId"];


                    psIds.Add(psid);
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
            // find psid from products_suppliers
            int ProductSupplierId = 0; // Real productSupplierId that will be deleted from Packages_Products_Suppliers table
            connection = TravelExpertsDB.GetConnection();
            selectStatement = "SELECT * FROM Products_Suppliers " +
                                     "where ProductId=@ProductId";
            cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@ProductId", prodId);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int psid; // temporary productsupplierid that will store a productsupplierid from DB
                while (reader.Read()) // while there is another record
                {

                    psid = (int)reader["ProductSupplierId"];
                    if (psIds.Contains(psid))
                        ProductSupplierId = psid;
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

            // finally delete the row from packages_products_suppliers table using packageid and productsupplierid
            string deleteStatement = "delete from Packages_Products_Suppliers  " +
                                     "where ProductSupplierId= @ProductSupplierId";
            cmd = new SqlCommand(deleteStatement, connection);


            cmd.Parameters.AddWithValue("@ProductSupplierId", ProductSupplierId);

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

        // update package columns
        public static bool UpdatePackage(Package oldp, Package newp)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string updateStatement = "UPDATE Packages " +
                                     "SET PkgName = @NewPkgName, " +
                                     "    PkgStartDate = @NewPkgStartDate, " +
                                     "    PkgEndDate = @NewPkgEndDate, " +
                                     "    PkgDesc = @NewPkgDesc, " +
                                     "    PkgBasePrice = @NewPkgBasePrice, " +
                                     "    PkgAgencyCommission = @NewPkgAgencyCommission " +
                                     "WHERE PackageId = @OldPackageId ";
            SqlCommand cmd = new SqlCommand(updateStatement, con);
            cmd.Parameters.AddWithValue("@NewPkgName", newp.PkgName);
            cmd.Parameters.AddWithValue("@NewPkgStartDate", newp.PkgStartDate);
            cmd.Parameters.AddWithValue("@NewPkgEndDate", newp.PkgEndDate);
            cmd.Parameters.AddWithValue("@NewPkgDesc", newp.PkgDesc);
            cmd.Parameters.AddWithValue("@NewPkgBasePrice", newp.PkgBasePrice);
            cmd.Parameters.AddWithValue("@NewPkgAgencyCommission", newp.PkgAgencyCommission);
            cmd.Parameters.AddWithValue("@OldPackageId", oldp.PackageId);

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

        public static void AddPackage(Package pkg)
        {
            
            SqlConnection con = TravelExpertsDB.GetConnection();
            string insertStatement = "INSERT INTO Packages (PkgName, PkgStartDate, PkgEndDate ,PkgDesc, PkgBasePrice ,PkgAgencyCommission) " +
                                     "VALUES(@PkgName, @PkgStartDate, @PkgEndDate, @PkgDesc, @PkgBasePrice, @PkgAgencyCommission)";
            SqlCommand cmd = new SqlCommand(insertStatement, con);
            
            cmd.Parameters.AddWithValue("@PkgName", pkg.PkgName);
            cmd.Parameters.AddWithValue("@PkgStartDate", pkg.PkgStartDate);
            cmd.Parameters.AddWithValue("@PkgEndDate", pkg.PkgEndDate);
            cmd.Parameters.AddWithValue("@PkgDesc", pkg.PkgDesc);
            cmd.Parameters.AddWithValue("@PkgBasePrice", pkg.PkgBasePrice);
            cmd.Parameters.AddWithValue("@PkgAgencyCommission", pkg.PkgAgencyCommission);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery(); // run the insert command
                // get the generated ID - current identity value for  Customers table
                string selectQuery = "SELECT IDENT_CURRENT('Packages') FROM Packages";
                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                pkg.PackageId = Convert.ToInt32(selectCmd.ExecuteScalar());
                
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

    }
}