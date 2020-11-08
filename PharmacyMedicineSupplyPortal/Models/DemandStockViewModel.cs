using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class DemandStockViewModel
    {
        public List<MedicineStock> MedicineStock { get; set; }
        public MedicineDemand MedicineDemand { get; set; }
    }
}
