using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineStockApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MedicineStockApi.Controllers
{
    public class MedicineStockController : Controller
    {
        private readonly IMedicineStockService service;
        public MedicineStockController(IMedicineStockService service)
        {
            this.service = service;
        }
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineStockController));
        [HttpGet]
        [Route("MedicineStockInformation")]
        public IActionResult MedicineStockInformation()
        {
            _log4net.Info("Get Api Initiated");
            try
            {
                var MedicineData = service.MedicineStockInformation1();
                if (MedicineData == null)
                {
                    _log4net.Info("Medicine Data Null");
                    return BadRequest();
                }
                _log4net.Info("Medicine Data Returned");
                return Ok(MedicineData);
            }
            catch (Exception E)
            {
                _log4net.Error(" Http MedicineStockInformation encountered an Exception :" + E.Message);
                return BadRequest();
            }
        }
    }
}
