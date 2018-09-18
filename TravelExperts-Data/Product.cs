using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{
    public class Product
    {
        public Product() { }
        public int ProductId { get; set; }
        public string ProdName { get; set; }

        //need these getters and setters for Products.DBGetProductsBySupplier() query -- Chris
        public int ProductSupplierID { get; set; }

        public int SupplierID { get; set; }

        public string SupName { get; set; }


        // copy a product
        public Product CopyProduct()
        {
            Product copy = new Product();
            copy.ProductId = ProductId;
            return copy;
        }

    }
}
