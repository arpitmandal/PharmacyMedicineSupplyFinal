using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupplyPortal.Models;
using PharmacyMedicineSupplyPortal.Repository;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class DemandSupplyController : Controller
    {
        private IDemands repo;
        private ISupplies supplyrepo;
        public DemandSupplyController(IDemands repo, ISupplies supplyrepo)
        {
            this.repo = repo;
            this.supplyrepo = supplyrepo;
        }
        public async Task<IActionResult> Index()
        {

            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                var stock = new List<MedicineStock>();

                using (var httpclient = new HttpClient())
                {
                    //httpclient.BaseAddress = new Uri("https://localhost:44366/");


                    //himanshu
                    httpclient.BaseAddress = new Uri("http://20.195.98.109/");
                    HttpResponseMessage res = await httpclient.GetAsync("MedicineStockInformation");
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        stock = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                    }
                }
                if(stock.Count==0)
                {
                    return RedirectToAction("Index","DemandSupply");
                }
                var list = new List<MedicineDemand>();
                foreach (var med in stock)
                {
                    list.Add(new MedicineDemand { Medicine = med.Name, Demand = 0 });
                }
                ViewBag.Demands = list;
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Message="Error message"+e;
                return RedirectToAction("Index","DemandSupply");
            }
        }

        [HttpPost]
        public IActionResult Add(MedicineDemand meds)
        {
            // string s = meds.Medicine + meds.Demand.ToString();


            try
            {
                if (meds == null)
                {
                    return RedirectToAction("Index", "DemandSupply");
                }
                Demands newdemand = new Demands()
                {
                    Medicine = meds.Medicine,
                    Demand = meds.Demand
                };
                int res = repo.AddDemand(newdemand);
                if (res > 0)
                {
                    return RedirectToAction("AddSupply", newdemand);
                }
                return RedirectToAction("Index", "DemandSupply");
            }
            catch(Exception e)
            {
                ViewBag.ex = e;
                return RedirectToAction("Index", "DemandSupply");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddSupply(Demands med)
        {

            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (med==null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var distributionOfStock = new List<PharmacyMedicineSupply>();

                using (var httpclient = new HttpClient())
                {
                // httpclient.BaseAddress = new Uri("https://localhost:44358/");

                //arhan
                    httpclient.BaseAddress = new Uri("http://20.43.177.52/");

                    HttpResponseMessage res = await httpclient.GetAsync("api/MedicineSupply/GetSupplies/" + med.Medicine + "/" + med.Demand);
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        distributionOfStock = JsonConvert.DeserializeObject<List<PharmacyMedicineSupply>>(result);
                    }
                }
                if (distributionOfStock.Count == 0)
                {
                    return RedirectToAction("Index", "DemandSupply");
                }
                foreach (var supply in distributionOfStock)
                {
                    supplyrepo.AddSupply(new Supplies { PharmacyName = supply.PharmacyName, MedicineName = supply.MedicineName, SupplyCount = supply.SupplyCount });
                }
                return View(distributionOfStock);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "DemandSupply");
            }
        }
    }

}

