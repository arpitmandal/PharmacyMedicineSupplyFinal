
using PharmacyMedicineSupplyApi.Models;
using PharmacyMedicineSupplyApi.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyApi.Service
{
    public class MedicineSupplyService : IMedicineSupplyService
    {
        private ISupply supplyRepo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineSupplyService));
        public MedicineSupplyService(ISupply supplyrepo)
        {
            this.supplyRepo = supplyrepo;
        }
        public async Task<List<PharmacyMedicineSupply>> MedcineSupply(string medicine, int demand)
        {
            _log4net.Info("MedicineSupply Initiated ");
            List<PharmacyMedicineSupply> medList = await supplyRepo.GetSupplies(medicine, demand);
            if(medList==null)
            {
                _log4net.Error("MedicineSupply returned No values ");
                return null;
            }
            _log4net.Info("MedicineSupply returned Supply list for distribution");
            return medList;
        }
    }
}
