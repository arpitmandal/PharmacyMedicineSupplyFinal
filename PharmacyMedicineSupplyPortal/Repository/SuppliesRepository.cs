using PharmacyMedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Repository
{
    public class SuppliesRepository : ISupplies
    {
        private EFDbContext context;
        public SuppliesRepository(EFDbContext context)
        {
            this.context = context;
        }
        public int AddSupply(Supplies supply)
        {
            context.Supplies.Add(supply);
            return context.SaveChanges();
        }
    }
}
