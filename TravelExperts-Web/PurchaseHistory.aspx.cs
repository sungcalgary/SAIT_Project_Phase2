using TravelExperts_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelExperts_Web
{
    //displaying purchase history of logged in user
    public partial class WebForm2 : System.Web.UI.Page
    {
        int FEE_AMT_COLUMN = 10;
        DataTable dt = new DataTable();

        List<Purchases> purchases;
        //displaying purchase history of logged in user to a datagrid
        protected void Page_Load(object sender, EventArgs e)
        {
            int CustomerID = (int)Session["CustomerID"];    //Customer ID of logged on user

            purchases = PurchasesDB.GetPurchases(CustomerID);
            string currentBooking;  //for storing Booking ID of current input record
            string lastBooking = "";    //for storing Booking ID of previous input record
            //Build grid view
            
            dt.Columns.Add("BookingNo", typeof(string));
            dt.Columns.Add("TravelerCount", typeof(int));
            dt.Columns.Add("BookingDate", typeof(string));  // dates as strings not DateTime for formatting
            dt.Columns.Add("ItineraryNo", typeof(double));
            dt.Columns.Add("TripStart", typeof(string));
            dt.Columns.Add("TripEnd", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Destination", typeof(string));
            dt.Columns.Add("BasePrice", typeof(string));   //commmission included in this from query

            dt.Columns.Add("FeeName", typeof(string));
            dt.Columns.Add("FeeAmt", typeof(string));
            DataRow dr = dt.NewRow();
            int row = 0;        //for counting row of data table, needed for identifying firs row, and
                                //for looping through datagrid after binding for formating indicated rows
            decimal totalCost = 0;  //keep a total cost per booking ID (base cost + total fees)
            decimal totalFees = 0;  //keep total fees cost per booking ID
            decimal grandTotal = 0; //keep total cost for all Bookings for one Customer
            int chargesCount = purchases.Count();   //count all purchase records for customer
            List<int> indicatedRows = new List<int>();  //list for tracking indicated rows to format

            if (chargesCount == 0)  //if no purchase display message
            {
                dt.Rows.Add("No Purchases Made");
            }

            foreach (Purchases p in purchases)
            {
                    currentBooking = (p.BookingNo).ToString();  //current bookingID recorder
                
                //if current booking is different from last booking and not first row this is where 
                //bookingID changes this creates rows for totalFees and totalCost as well as row for
                //next bookingID
                if ((currentBooking != lastBooking && row > 0))                                          
                {                                                           
                    //track total cost per bookingID
                    totalCost = totalFees + totalCost;

                    //add total fees under fees columns
                    //add total below total fees (base cost + total fees)
                    FeesAndTotalCostRows(totalFees, totalCost);

                    //keep total cost for all Bookings for one Customer
                    grandTotal = grandTotal + totalCost;

                    //add indicated row numbers to list for formating, these are for TotalFees and TotalCost
                    indicatedRows.Add(row);
                    indicatedRows.Add(row + 1);

                    //first row of next BookingID 
                    dt.Rows.Add((p.BookingNo).ToString(), (p.TravelerCount), (p.BookingDate.Date.ToString("MM/dd/yyyy")), (p.ItineraryNo),
                        (p.TripStart.Date.ToString("MM/dd/yyyy")), (p.TripEnd.Date.ToString("MM/dd/yyyy")), (p.Description), (p.Destination), (p.BasePrice).ToString("c"),
                        (p.FeeName), (p.FeeAmt).ToString("c"));

                    //set totalFees and TotalCost, need for grand total Calculation
                    totalFees = p.FeeAmt;
                    totalCost = p.BasePrice;
                    //incriment row count by 2
                    row = row + 2;
                }
                //if current booking is same as last booking then all columns besides FeeName and Total Fees are
                //duplicate so only display last two columns as having cell contents
                else if (currentBooking == lastBooking) 
                {
                    dt.Rows.Add("", null, null, null,
                    null, null, "", "", null,
                    (p.FeeName), (p.FeeAmt).ToString("c"));
                    //track total fees per bookingID
                    totalFees = p.FeeAmt + totalFees;
                }
                else if (row == 0)  //if first row
                {
                    //everything will display on first row
                    dt.Rows.Add((p.BookingNo).ToString(), (p.TravelerCount), (p.BookingDate.Date.ToString("MM/dd/yyyy")), (p.ItineraryNo),
                    (p.TripStart.Date.ToString("MM/dd/yyyy")), (p.TripEnd.Date.ToString("MM/dd/yyyy")), (p.Description), (p.Destination), (p.BasePrice).ToString("c"),
                    (p.FeeName), (p.FeeAmt).ToString("c"));
                    totalCost = p.BasePrice;    //starting value of totalCost set to Base Price
                    totalFees = p.FeeAmt;       //starting value of totalFees set to first FeeAmt
                }
                //set the current booking to lastBooking for comparing to the next booking when
                //cycling through the loop again
                lastBooking = currentBooking;
                //incriment by one row count at end of loop
                row++;
            }

            //after the loop need to calculate and display totalCost for last record
            totalCost = totalFees + totalCost;
            grandTotal = grandTotal + totalCost;

            FeesAndTotalCostRows(totalFees, totalCost);

            //for formatting the Total Fees and Total Cost Rows
            indicatedRows.Add(row);
            indicatedRows.Add(row + 1);

            //add blank rows
            dt.Rows.Add("");
            dt.Rows.Add("");

            //for formatting grand toal row
            indicatedRows.Add(row + 4);

            //row to display grand total
            dt.Rows.Add("", null, null, null,
            null, null, "", "", null,
            ("Grand Total"), (grandTotal).ToString("c"));

            //bind dt created above to the gridv view
            gv.DataSource = dt;
            gv.DataBind();

            //formatting cells as bold based on indicated row counts
            foreach (int i in indicatedRows)
            {
                gv.Rows[i].Cells[9].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                gv.Rows[i].Cells[10].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
            }
        }

        //method for adding total cost and total fees rows to minimize repeated code
        private void FeesAndTotalCostRows(decimal totalFees, decimal totalCost)
        {
            dt.Rows.Add("", null, null, null,
            null, null, "", "", null,
            ("Total Fees"), (totalFees).ToString("c"));

            dt.Rows.Add("", null, null, null,
            null, null, "", "", null,
            ("Total"), (totalCost).ToString("c"));
        }
    }
}
