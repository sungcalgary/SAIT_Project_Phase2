//Threaded Project 2
//OOSD Spring August 2018
//By: Marc Javier

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelExperts_Web
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", "2;URL=login.aspx");
        }
    }
}