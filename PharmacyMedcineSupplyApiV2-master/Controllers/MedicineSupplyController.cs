using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupplyApi.Models;
using PharmacyMedicineSupplyApi.Respository;
using PharmacyMedicineSupplyApi.Service;

namespace PharmacyMedicineSupplyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class MedicineSupplyController : ControllerBase
    {

        private IMedicineSupplyService _medicineRepo;
        private readonly IMapper _mapper;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineSupplyController));
        public MedicineSupplyController(IMedicineSupplyService medcineRepo,IMapper mapper)
        {
            _medicineRepo = medcineRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("GetSupplies/{medicineName}/{count}")]
        public async Task<IActionResult> GetSupplies(string medicineName,int count)
        {
            _log4net.Info(" Http GetSupplies request Initiated ");
            if (medicineName==""||count==0)
            {
                _log4net.Error("Http GetSupplies Null values passed to GetSupplies Method");
                return BadRequest("Please provide some values");
            }
            try
            {
                _log4net.Info("Http GetSupplies request for"+medicineName +" with a demand count of "+count);

                List<PharmacyMedicineSupply> supplylist = await _medicineRepo.MedcineSupply(medicineName, count);
                if (supplylist != null)
                {
                    _log4net.Info(" Http GetSupplies response returned");
                    List<PharmacyMedicineSupplyDto> medincinesupply = new List<PharmacyMedicineSupplyDto>();
                    foreach(var supply in supplylist)
                    {
                        PharmacyMedicineSupplyDto medSupply = _mapper.Map<PharmacyMedicineSupplyDto>(supply);
                        medincinesupply.Add(medSupply);

                        _log4net.Info(" Http GetSupplies Distribution : "+medSupply.PharmacyName+" Has to be supplied "+medSupply.SupplyCount+" of "+medSupply.MedicineName);
                    }
                    return Ok(medincinesupply);
                }
                _log4net.Error("Http GetSupplies No values returned");
                return BadRequest("Some Error While fetching request");
            }
            catch (Exception e)
            {
                _log4net.Error(" Http GetSupplies encountered an Excpetion :"+e.Message);
                return NotFound(e.Message);
            }
        }
    }
}