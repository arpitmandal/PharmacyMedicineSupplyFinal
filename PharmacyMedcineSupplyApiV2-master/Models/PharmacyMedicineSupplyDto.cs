using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyApi.Models
{
    public class PharmacyMedicineSupplyDto
    {
        public string PharmacyName { get; set; }
        public string MedicineName { get; set; }
        public int SupplyCount { get; set; }
    }
}
