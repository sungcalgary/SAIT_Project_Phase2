using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts_Data
{   /*
    * this file has 1 author
    * Author: Sunghyun Lee
    * Created: 2018-07
    */
    public class Package
    {
        public Package() { }
        public int PackageId { get; set; }
        public string PkgName { get; set; }
        public DateTime PkgStartDate { get; set; }
        public DateTime PkgEndDate { get; set; }
        public List<int> ProductIds { get; set; }
        public string PkgDesc { get; set; }
        public double PkgBasePrice { get; set; }
        public double PkgAgencyCommission { get; set; }

        // copy a package
        public Package CopyPackage()
        {
            Package copy = new Package();
            copy.PackageId = PackageId; ; // this customer's ID
            copy.PkgName = PkgName;
            copy.PkgStartDate = PkgStartDate;
            copy.PkgEndDate = PkgEndDate;
            copy.ProductIds = ProductIds;
            copy.PkgDesc = PkgDesc;
            copy.PkgBasePrice = PkgBasePrice;
            copy.PkgAgencyCommission = PkgAgencyCommission;
            return copy;
        }
    }

    
}
