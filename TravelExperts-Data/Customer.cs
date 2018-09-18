//Threaded Project 2
//OOSD Spring August 2018
//By: Marc Javier

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelExperts_Data
{
    public class Customer
    {
        public Customer() { }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string BusPhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public int AgentID { get; set; }
    }
}