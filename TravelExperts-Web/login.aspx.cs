using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelExperts_Data;

namespace TravelExperts_Web
{
    /*
     * This file has 1 author
     * Author: Sunghyun Lee
     * Created: 2018-07
     */
    public partial class WebForm4 : System.Web.UI.Page
    {
        SortedList<string,int> emailList; // Key: customer email. Value: customer id
        protected void Page_Load(object sender, EventArgs e)
        {
            
            emailList = CustomerDB.GetCustomerEmails();
            // if not logged in, displays input elements for login
            if (Session["CustomerID"] == null)
            {
                lblId.Visible = true;
                txtEmail.Visible = true;
                txtPassword.Visible = true;
                lblPassword.Visible = true; 
                btnLogIn.Visible = true;               
                btnSignUp.Visible = true;

                btnLogOff.Visible = false;
                lblAsk.Text = "Not a member?";

            }
            //if logged in, hides all input elements and displays log off button
            else
            {
                lblId.Visible = false;
                txtEmail.Visible = false;
                txtPassword.Visible = false;
                lblPassword.Visible = false;
                btnLogIn.Visible = false;
                btnSignUp.Visible = false;

                btnLogOff.Visible = true;
                lblAsk.Text = "You are logged in as " + Session["CustEmail"] + "\nDo you want to log off?";

            }

        }
        // if all inputs are validated, store customer id and email in session. Then redirects to the home page
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                string response = "";
                if (CustomerDB.VerifyLogin(txtEmail.Text, txtPassword.Text, ref response))
                {
                    Session["CustomerID"] = Convert.ToInt32(emailList[txtEmail.Text]);
                    Session["CustEmail"] = txtEmail.Text;
                    Response.Redirect("home.aspx");
                }
                else
                {
                    
                    lblError.Text = response;
                }
            }
        }

        // returns true if all inputs are in correct format. Otherwise displays an error message
        private bool ValidateLogin()
        {
            if (txtEmail.Text=="")
            {
                lblError.Text = "Please type your Email Address";
                return false;
            }
            else if (txtPassword.Text == "")
            {
                lblError.Text = "Please type your password";
                return false;
            }

            else if (!emailList.ContainsKey(txtEmail.Text))
            {
                lblError.Text = "There is no such Email Address";
                return false;
            }
            return true;
        }

        // deletes data from session and redirects to login.aspx
        protected void btnLogOff_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}