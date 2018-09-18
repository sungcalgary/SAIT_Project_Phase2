using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelExperts_Data;

/*
 * Author: Sunghyun Lee
 * Created: 2018-07
 */

namespace TravelExperts_Web
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerId"] == null)
                Response.Redirect("login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    lblError.Visible = false;
                    lblError.Text = "";
                }
            }
        }
        // redirects to customerPage.aspx
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("customerPage.aspx");
        }
        // updates customer's password and redirects to customerPage.aspx if successful
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string newPassword = tbPassword.Text;
            int customerId = (int) Session["CustomerId"];

            bool success= CustomerDB.ChangePassword(customerId, newPassword);
            if (success)
            {
                Response.Redirect("customerPage.aspx");
            }
            else
            {
                lblError.Text = "Your password has not been updated. Please try again";
                lblError.Visible = true;
            }
        }
    }
}