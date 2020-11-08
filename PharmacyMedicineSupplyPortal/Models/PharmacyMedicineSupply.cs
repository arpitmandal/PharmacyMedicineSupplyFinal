using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class PharmacyMedicineSupply
    {
        public string PharmacyName { get; set; }
        public string MedicineName { get; set; }
        public int SupplyCount { get; set; }
    }
}
