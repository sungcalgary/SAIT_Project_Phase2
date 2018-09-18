//Threaded Project 2
//OOSD Spring August 2018
//By: Marc Javier
//edits by: Sunghyun Lee

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelExperts_Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        // changes display of Nav Bar depending on whether a user is logged in --Sunghyun Lee
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerId"] == null)
            {
                LinkButtonLogin.Visible = true;

                linkLogoff.Visible = false;
                linkHello.Visible = false;
            }
            else
            {
                LinkButtonLogin.Visible = false;

                linkLogoff.Visible = true;
                linkHello.Visible = true;
                linkHello.Text = "You are logged in as " + Session["CustEmail"];
            }
        }

        // check session for customer edit link -- Marc Javier
        protected void LinkButtonManage_Click(object sender, EventArgs e)
        {
            if (Session["CustomerId"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Response.Redirect("PurchaseHistory.aspx");
            }
        }

        protected void linkHello_Click(object sender, EventArgs e)
        {
            Response.Redirect("customerPage.aspx");
        }

        protected void linkLogoff_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}