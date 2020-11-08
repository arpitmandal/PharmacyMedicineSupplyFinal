using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class MedicineDemand
    {
        
        public string Medicine { get; set; }
        public int Demand { get; set; }
    }
}
