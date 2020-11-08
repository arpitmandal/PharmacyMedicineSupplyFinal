using PharmacyMedicineSupplyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyApi.Respository
{
    public interface ISupply
    {
        public Task<List<PharmacyMedicineSupply>> GetSupplies(string medicineName, int demand);
    }
}
