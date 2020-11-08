using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class MedicineStock
    {
        public string Name { get; set; }
        public string ChemicalComposition { get; set; }
        public string TargetAilment { get; set; }
        public string DateOfExpiry { get; set; }
        public int NumberOfTabletsInStock { get; set; }
    }
}
