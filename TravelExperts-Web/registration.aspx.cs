//Threaded Project 2
//OOSD Spring August 2018
//By: Marc Javier
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelExperts_Data;

namespace TravelExperts_Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SortedList<string, int> emails; //empty list
        public Customer customer;
        protected void Page_Load(object sender, EventArgs e)
        {
            emails = CustomerDB.GetCustomerEmails();
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // validates if email already exists
            if (!emails.ContainsKey(tbEmail.Text))
            {
                customer = new Customer();
                PutCustomerData(customer);
                CustomerDB.AddCustomer(customer);
                Response.Redirect("registration-success.aspx");
            }
            else
            {
                validEmail.Visible = true;
                validEmail.Text = "This email already exist. Please login or use a different email address.";
            }
        }

        private void PutCustomerData(Customer customer)
        {
            customer.FirstName = tbFirstName.Text;
            customer.LastName = tbLastName.Text;
            customer.Address = tbAddress.Text;
            customer.City = tbCity.Text;
            customer.Province = ddlProvince.SelectedValue;
            customer.Country = ddlCountry.SelectedValue;
            customer.PostalCode = tbPostalCode.Text;
            customer.HomePhone = tbHomePhone.Text;
            customer.BusPhone = tbBusinessPhone.Text;
            customer.Email = tbEmail.Text;

            customer.Password = tbPassword.Text;
        }
    }
}