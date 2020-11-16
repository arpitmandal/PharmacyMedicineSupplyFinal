using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupplyPortal.Models;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class MRScheduleMeetController : Controller
    {
        string startDateTemp = null;
        string startDateBackup = null;
        public IActionResult EnterDate()
        {
            if(TokenInfo.token==null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        public IActionResult Index(IFormCollection form)
        {


            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }

            startDateTemp = form["startDate"].ToString();

            startDateBackup = startDateTemp.Replace('-', '/');
            TempData["Date1"] = startDateBackup;

            return RedirectToAction("MrMeet", "MRScheduleMeet");



        }


        [HttpGet]
        public async Task<IActionResult> MrMeet()
        {

            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string startDate = startDateBackup;
            try
            {

                startDate = TempData["Date1"].ToString();

            }
            catch (Exception e)
            {
                ViewBag.Message = "Exception Encountered : "+e.Message;
                return View("~/Views/Shared/ExceptionAndError.cshtml");

            }

            try
            {
                var MRMeetList = new List<RepSchedule>();
                using (var httpclient = new HttpClient())
                {

                    //httpclient.BaseAddress = new Uri("https://localhost:44372/");


                   
                    httpclient.BaseAddress = new Uri("http://52.224.190.35/");

                    HttpResponseMessage res = await httpclient.GetAsync("api/ScheduleMeeting?startDate=" + startDate);
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        MRMeetList = JsonConvert.DeserializeObject<List<RepSchedule>>(result);
                    }
                }

                return View(MRMeetList);
            }
            catch(Exception e)
            {
                ViewBag.Message = "Exception Encountered : " + e.Message;
                return View("~/Views/Shared/ExceptionAndError.cshtml");
            }
        }
    }
}
