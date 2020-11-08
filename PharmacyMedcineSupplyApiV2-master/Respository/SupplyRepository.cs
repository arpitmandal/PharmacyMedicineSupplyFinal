using Newtonsoft.Json;
using PharmacyMedicineSupplyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyApi.Respository
{
    public class SupplyRepository : ISupply
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SupplyRepository));

        public async Task<List<PharmacyMedicineSupply>> GetSupplies(string medicineName,int demand)
        {
            _log4net.Info("GetSupplies request Initiated in MedicineSupplyRepository for " + medicineName + " with demand count " + demand);
            //List of Supply to be sent
            List<PharmacyMedicineSupply> supplies = new List<PharmacyMedicineSupply>();
            var stock = new List<MedicineStock>();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44366/");
                    HttpResponseMessage res = await httpclient.GetAsync("MedicineStockInformation");
                    _log4net.Info("GetSupplies request Initiated for the Medicine Api");
                    if (res.IsSuccessStatusCode)
                    {
                        _log4net.Info("GetSupplies data recieved from the Stock Api");
                        var result = res.Content.ReadAsStringAsync().Result;
                        stock = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                    }
                    else
                    {
                        _log4net.Error("GetSupplies no data recieved from the Stock Api");
                        return null;
                    }
                }



                //Dictionary to store the Name of med and stock value as key value pair
                Dictionary<string, int> meds = new Dictionary<string, int>();
                foreach (var medicine in stock)
                {
                    meds.Add(medicine.Name, medicine.NumberOfTabletsInStock);
                }

                //List of Pharmacy the company does business with
                List<string> Pharmacies = new List<string>() { "Max Pharmacy", "Appolo Pharmacy", "Synergy Pharmacy", "GoodWill Pharmacy" };
                int totalPharmacies = Pharmacies.Count;
                PharmacyMedicineSupply medSupply;

                //Equal distribution of Medicine in Demand
                int inStock = meds[medicineName];
                int demandStock = demand;
                if (inStock > demandStock)
                {
                    int medCount = demandStock / totalPharmacies;
                    for (int i = 0; i < totalPharmacies - 1; i++)
                    {
                        medSupply = new PharmacyMedicineSupply() { MedicineName = medicineName, PharmacyName = Pharmacies[i], SupplyCount = medCount };
                        supplies.Add(medSupply);
                        demandStock = demandStock - medCount;
                    }
                    medSupply = new PharmacyMedicineSupply() { MedicineName = medicineName, PharmacyName = Pharmacies[totalPharmacies - 1], SupplyCount = demandStock };
                    supplies.Add(medSupply);
                }
                else
                {
                    int medCount = inStock / totalPharmacies;
                    for (int i = 0; i < totalPharmacies - 1; i++)
                    {
                        medSupply = new PharmacyMedicineSupply() { MedicineName = medicineName, PharmacyName = Pharmacies[i], SupplyCount = medCount };
                        supplies.Add(medSupply);
                        inStock = inStock - medCount;
                    }
                    medSupply = new PharmacyMedicineSupply() { MedicineName = medicineName, PharmacyName = Pharmacies[totalPharmacies - 1], SupplyCount = inStock };
                    supplies.Add(medSupply);
                }

                return supplies;
            }
            catch(Exception ex)
            {
                _log4net.Error("GetSupplies Could not Connect to the Medicine Stock Api"+ex.Message);
            }
            return supplies;
        }
    }
}
