using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class Demands
    {
        [Key]
        public int DemandId { get; set; }
        public string Medicine { get; set; }
        public int Demand { get; set; }
    }
}
