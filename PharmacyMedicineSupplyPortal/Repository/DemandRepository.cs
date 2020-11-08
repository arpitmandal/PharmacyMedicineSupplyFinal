using PharmacyMedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Repository
{
    public class DemandRepository : IDemands
    {
        private EFDbContext context;
        public DemandRepository(EFDbContext context)
        {
            this.context = context;
        }
        public int AddDemand(Demands demands)
        {
            context.Demands.Add(demands);
            return context.SaveChanges();
        }
    }
}
