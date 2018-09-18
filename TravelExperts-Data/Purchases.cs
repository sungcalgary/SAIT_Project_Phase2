using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{
        /*
     * Author: Chris
     * Created: 2018-07
     */

    public class Purchases
    {
        public Purchases() { }
        public int BookingId { get; set; }

        public string BookingNo { get; set; }

        public double TravelerCount { get; set; }

        public DateTime BookingDate { get; set; }

        public double ItineraryNo { get; set; }

        public DateTime TripStart { get; set; }

        public DateTime TripEnd { get; set; }

        public string Description { get; set; }

        public string Destination { get; set; }

        public decimal BasePrice { get; set; }

        public string FeeName { get; set; }

        public decimal FeeAmt { get; set; }
    }
}
