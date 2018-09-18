using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelExperts_Data;

/*
 * Author: Sunghyun Lee
 */
namespace TravelExperts_Web
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        public Customer cust;
        public Customer newCust; // new cust
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerId"] != null)
            {
                if (!IsPostBack)
                {
                    // finds the customer using customerid from session
                    cust = new Customer();
                    int custid = Convert.ToInt32(Session["CustomerId"]);
                    cust = CustomerDB.GetCustomerById(custid);
                    // filling customer information
                    txtId.Text = cust.CustomerID.ToString();
                    txtFname.Text = cust.FirstName;
                    txtLname.Text = cust.LastName;
                    txtEmail.Text = cust.Email;
                    txtCity.Text = cust.City;
                    txtAddress.Text = cust.Address;
                    txtCountry.Text = cust.Country;
                    ddlProvince.Text = cust.Province;
                    txtPostalCode.Text = cust.PostalCode;
                    txtBusPhone.Text = cust.BusPhone;
                    txtHomePhone.Text = cust.HomePhone;
                }
            }
            else // if not logged in, redirects to login page
            {
                Response.Redirect("login.aspx");
            }

        }
        //========== edit buttons for each textboxes. When clicked, enables its text box so that customer can edit
        protected void btnFname_Click(object sender, EventArgs e)
        {
            txtFname.Enabled = true;
            btnSave.Enabled = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtLname.Enabled = true;
            btnSave.Enabled = true;

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            txtPostalCode.Enabled = true;
            btnSave.Enabled = true;

        }

        protected void btnAddress_Click(object sender, EventArgs e)
        {
            txtAddress.Enabled = true;
            btnSave.Enabled = true;

        }

        protected void btnProvince_Click(object sender, EventArgs e)
        {
            ddlProvince.Enabled = true;
            btnSave.Enabled = true;
        }

        protected void btnCountry_Click(object sender, EventArgs e)
        {
            txtCountry.Enabled = true;
            btnSave.Enabled = true;

        }

        protected void btnHomePhone_Click(object sender, EventArgs e)
        {
            txtHomePhone.Enabled = true;
            btnSave.Enabled = true;

        }

        protected void btnBusPhone_Click(object sender, EventArgs e)
        {
            txtBusPhone.Enabled = true;
            btnSave.Enabled = true;

        }
        protected void btnCity_Click(object sender, EventArgs e)
        {
            txtCity.Enabled = true;
            btnSave.Enabled = true;
        }
        //========== end ===============



        protected void btnSave_Click(object sender, EventArgs e)
        {
            newCust = new Customer();
            newCust.CustomerID = Convert.ToInt32(txtId.Text);
            newCust.FirstName = txtFname.Text;
            newCust.LastName = txtLname.Text;
            newCust.Email = txtEmail.Text;
            newCust.Address = txtAddress.Text;
            newCust.Country = txtCountry.Text;
            newCust.Province = ddlProvince.Text;
            newCust.City = txtCity.Text;
            newCust.PostalCode = txtPostalCode.Text;
            newCust.BusPhone = txtBusPhone.Text;
            newCust.HomePhone = txtHomePhone.Text;

            bool success = CustomerDB.UpdateCustomer(cust, newCust);
            if (success)
                Response.Redirect("customerPage.aspx");
        }

        

        protected void txtCountry_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtFname_TextChanged(object sender, EventArgs e)
        {

        }

        // redirects to changePassword.aspx
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("changePassword.aspx");
        }
    }
}