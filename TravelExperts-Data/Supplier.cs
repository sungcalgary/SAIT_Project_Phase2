using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Luke

namespace TravelExperts_Data
{
    public class Supplier
    {
        public Supplier() { }
        public int SupplierId { get; set; }        
        public string SupName { get; set; }
        public int ProductSupplierID { get; set; }
        public int ProductId { get; set; }
        public string ProdName { get; set; }

        // copy a supplier
        public Supplier CopySupplier()
        {
            Supplier copy = new Supplier();
            copy.SupplierId = SupplierId; 
            copy.SupName = SupName;
            //copy.SupplierIds = SupplierIds;
            return copy;
        }
    }
}
