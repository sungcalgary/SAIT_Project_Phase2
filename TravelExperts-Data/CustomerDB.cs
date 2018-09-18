using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelExperts_Data;

namespace TravelExperts_Data
{
    /*
     * This file has many authors
     * Author: Marc, Sunghyun Lee
     * Create: 2018-07
     */
    public class CustomerDB
    {
        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();            // empty list
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // prepare the statement
            string selectString = "SELECT CustFirstName, CustLastName, CustAddress, CustCity, CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail " +
                "FROM Customers " +
                "ORDER BY CustomerId";

            SqlCommand selectCommand = new SqlCommand(selectString, connection);

            try
            {
                // open connection
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Customer cust;
                while (reader.Read())
                {
                    cust = new Customer();
                    cust.Email = reader["CustEmail"].ToString();

                    customers.Add(cust);
                }
            }
            catch (Exception ex)
            {
                throw ex; //
            }
            finally
            {
                connection.Close();
            }
            return customers;

        }

        // inserts new customer record
        public static void AddCustomer(Customer cust)
        {

            SqlConnection connection = TravelExpertsDB.GetConnection();

            // prepare the statement
            string insertString = "INSERT into Customers " +
                                  "(CustFirstName, CustLastName, CustAddress, CustCity, CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail) " +
                                  "VALUES(@FirstName, @LastName, @Adress, @City, @Province, @PostalCode, @Country, @HomePhone, @BusPhone, @Email)";

            SqlCommand insertCommand = new SqlCommand(insertString, connection);
            insertCommand.Parameters.AddWithValue("@FirstName", cust.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", cust.LastName);
            insertCommand.Parameters.AddWithValue("@Adress", cust.Address);
            insertCommand.Parameters.AddWithValue("@City", cust.City);
            insertCommand.Parameters.AddWithValue("@Province", cust.Province);
            insertCommand.Parameters.AddWithValue("@PostalCode", cust.PostalCode);
            insertCommand.Parameters.AddWithValue("@Country", cust.Country);
            insertCommand.Parameters.AddWithValue("@HomePhone", cust.HomePhone);
            insertCommand.Parameters.AddWithValue("@BusPhone", cust.BusPhone);
            insertCommand.Parameters.AddWithValue("@Email", cust.Email);

            try
            {
                // open connection
                connection.Open();

                // execute the statement
                insertCommand.ExecuteNonQuery();

                //grab new customerid
                string selectQuery = "SELECT IDENT_CURRENT('Customers') FROM Customers";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                cust.CustomerID = Convert.ToInt32(selectCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex; //
            }
            finally
            {
                connection.Close();
            }

            string insertPassword = "INSERT INTO Customers_Account (CustomerId, CustPassword) VALUES(@CustomerId, @Password)";

            SqlCommand insertPass = new SqlCommand(insertPassword, connection);
            insertPass.Parameters.AddWithValue("@CustomerId", cust.CustomerID);
            insertPass.Parameters.AddWithValue("@Password", cust.Password);

            try
            {
                // open connection
                connection.Open();

                // execute the statement
                insertPass.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex; //
            }
            finally
            {
                connection.Close();
            }
        }

        // returns true if customer id and password matches -- Sunghyun Lee
        public static bool VerifyLogin(string email, string pw, ref string response)
        {//Sunghyun Lee
            string DBpassword;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT ca.CustPassword FROM Customers_Account ca " +
                                     "inner join Customers c on ca.CustomerId=c.CustomerId " +
                                     "WHERE c.CustEmail=@CustEmail";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            cmd.Parameters.AddWithValue("@CustEmail", email);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) // if there is a record
                {
                    DBpassword=reader["CustPassword"].ToString(); 
                }
                else
                {
                    return false;
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
            if (pw == DBpassword)
            {
                
                return true;
            }
            else
            {
                response = "Your Email Address and Password do not match";
                return false;
            }

        }
        // returns list of all customer ids from DB -- sunghyun lee
        public static SortedList<string, int> GetCustomerEmails()
        {// Sunghyun Lee
            SortedList<string,int> emailList = new SortedList<string, int>(); // empty

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT c.CustEmail, c.CustomerId FROM Customers_Account ca " +
                                     "inner join Customers c on ca.CustomerId = c.CustomerId";
            SqlCommand cmd = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string email;
                int id;
                while (reader.Read()) // while there is another record
                {
                    email= reader["CustEmail"].ToString();
                    id = (int)reader["CustomerId"];

                    emailList.Add(email,id);
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

            return emailList;
        }
        // finds the customer with given id -- Sunghyun Lee
        public static Customer GetCustomerById(int custid)
        {// Sunghyun
            Customer cust = new Customer();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // prepare the statement
            string selectString = "SELECT CustomerId,CustFirstName, CustLastName, CustAddress, CustCity, CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail " +
                "FROM Customers " +
                "where CustomerId= @CustomerId";

            SqlCommand cmd = new SqlCommand(selectString, connection);
            

            cmd.Parameters.AddWithValue("@CustomerId", custid);


            try
            {
                // open connection
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    cust.CustomerID = custid;
                    cust.Email = reader["CustEmail"].ToString();
                    cust.FirstName= reader["CustFirstName"].ToString();
                    cust.LastName = reader["CustLastName"].ToString();

                    cust.Address = reader["CustAddress"].ToString();

                    cust.City = reader["CustCity"].ToString();
                    cust.Province = reader["CustProv"].ToString();
                    cust.PostalCode = reader["CustPostal"].ToString();
                    cust.Country = reader["CustCountry"].ToString();
                    cust.HomePhone = reader["CustHomePhone"].ToString();
                    cust.BusPhone = reader["CustBusPhone"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex; //
            }
            finally
            {
                connection.Close();
            }
            return cust;

        }
        // update customer -- Sunghyun Lee
        public static bool UpdateCustomer(Customer oldc, Customer newc)
        {//Sunghyun Lee
            SqlConnection con = TravelExpertsDB.GetConnection();
            string updateStatement = "UPDATE Customers " +
                                    // "SET CustEmail = @NewCustEmail, " +
                                     "SET    CustFirstName = @NewCustFirstName, " +
                                     "    CustLastName = @NewCustLastName, " +
                                     "    CustAddress = @NewCustAddress, " +
                                     "    CustCity = @NewCustCity, " +
                                     "    CustProv = @NewCustProv, " +
                                     "CustPostal =@NewCustPostal, " +
                                     "CustCountry=@NewCustCountry, " +
                                     "CustHomePhone=@NewCustHomePhone, " +
                                     "CustBusPhone=@NewCustBusPhone " +
                                     
                                     "WHERE CustomerId = @CustomerId ";
            SqlCommand cmd = new SqlCommand(updateStatement, con);
            //cmd.Parameters.AddWithValue("@NewCustEmail", newc.Email);
            cmd.Parameters.AddWithValue("@NewCustFirstName", newc.FirstName);
            string temp = newc.FirstName;
            cmd.Parameters.AddWithValue("@NewCustLastName", newc.LastName);
            cmd.Parameters.AddWithValue("@NewCustAddress", newc.Address);
            cmd.Parameters.AddWithValue("@NewCustCity", newc.City);
            cmd.Parameters.AddWithValue("@NewCustProv", newc.Province);
            cmd.Parameters.AddWithValue("@NewCustPostal", newc.PostalCode);
            cmd.Parameters.AddWithValue("@NewCustCountry", newc.Country);
            cmd.Parameters.AddWithValue("@NewCustHomePhone", newc.HomePhone);
            cmd.Parameters.AddWithValue("@NewCustBusPhone", newc.BusPhone);

            cmd.Parameters.AddWithValue("@CustomerId", newc.CustomerID);
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
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

        // changes password for customer in Customers_Account table. Returns true if query has been processed
        public static bool ChangePassword(int CustomerId, string newPassword)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string updateStatement = "UPDATE Customers_Account " +
                                     
                                     "SET    CustPassword = @newPassword " +
                                     

                                     "WHERE CustomerId = @CustomerId ";
            SqlCommand cmd = new SqlCommand(updateStatement, con);
            cmd.Parameters.AddWithValue("@newPassword", newPassword);
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
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