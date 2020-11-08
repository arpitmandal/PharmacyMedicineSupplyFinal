using PharmacyMedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Repository
{
    public interface IDemands
    {
        public int AddDemand(Demands demands);
    }
}
