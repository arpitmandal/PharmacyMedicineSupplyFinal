using PharmacyMedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Repository
{
    public interface ISupplies
    {
        public int AddSupply(Supplies supply);
    }
}
