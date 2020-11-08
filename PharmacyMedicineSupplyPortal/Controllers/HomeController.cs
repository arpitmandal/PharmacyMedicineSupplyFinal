using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PharmacyMedicineSupplyPortal.Models;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Getting the token a Validating the User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(Users user)
        {
            string token = GetToken("https://localhost:44342/api/Token/AuthenticateUser", user);



            if (token != null)
            {

                TokenInfo.token = token;
                return RedirectToAction("EnterDate", "MRScheduleMeet", new { name = token });

                //return RedirectToAction("Dashboard", "Home", new { name = token });
            }
            else
            {
                ViewBag.invalid = "UserId or Password invalid";
                return View();
            }
        }

        static string GetToken(string url, Users user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(name);
                return details.tokenString;
            }
        }

        public async Task<IActionResult> Index()
        {
            var stock = new List<MedicineStock>();

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44366/");
                HttpResponseMessage res = await httpclient.GetAsync("MedicineStockInformation");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    stock = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                }
            }

            var list = new List<MedicineDemand>();
            foreach (var med in stock)
            {
                list.Add(new MedicineDemand { Medicine = med.Name, Demand = 0 });
            }
            ViewBag.Demands = list;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult LogOut()
        {
            TokenInfo.token = null;
            //TempData["token"] = "";
            return RedirectToAction("Login", "Home", new { name = "" });
        }
    }
}
