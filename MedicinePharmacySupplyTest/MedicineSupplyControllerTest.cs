using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PharmacyMedicineSupplyApi.Controllers;
using PharmacyMedicineSupplyApi.Models;
using PharmacyMedicineSupplyApi.Respository;
using PharmacyMedicineSupplyApi.Service;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyMedicineSupplyTest
{
    public class Tests
    {
        List<PharmacyMedicineSupply> supplyList;
        Mock<IMedicineSupplyService> supplyService;
        Mock<IMapper> mapper;
        [SetUp]
        public void Setup()
        {

            supplyService = new Mock<IMedicineSupplyService>();
            supplyList = new List<PharmacyMedicineSupply>() {
            new PharmacyMedicineSupply{ MedicineName="Aspirin",PharmacyName="Pharmacy1",SupplyCount=50},
            new PharmacyMedicineSupply{ MedicineName="Aspirin",PharmacyName="Pharmacy2",SupplyCount=50}
            };

            mapper = new Mock<IMapper>();
           
        }
        [Test]
        public void GetSupplies_ValidInput_OkResult()
        {
            MedicineDemand medicineDemand = new MedicineDemand()
            {
                Medicine = "Aspirin",
                Demand = 100
            };
            supplyService.Setup(s => s.MedcineSupply(medicineDemand.Medicine, medicineDemand.Demand)).ReturnsAsync(supplyList);
            var controller = new MedicineSupplyController(supplyService.Object, mapper.Object);
            var data = controller.GetSupplies(medicineDemand.Medicine,medicineDemand.Demand).Result;
            var res = data as OkObjectResult;
            Assert.AreEqual(200,res.StatusCode);
        }

        [Test]
        public void GetSupplies_InValidInput_BadRequest()
        {
            MedicineDemand medicineDemand = new MedicineDemand()
            {
                Medicine = "Aspirin",
                Demand = 0
            };
            supplyService.Setup(s => s.MedcineSupply(medicineDemand.Medicine, medicineDemand.Demand)).ReturnsAsync(supplyList);
            var controller = new MedicineSupplyController(supplyService.Object, mapper.Object);
            var data = controller.GetSupplies(medicineDemand.Medicine,medicineDemand.Demand).Result;
            var s = data as BadRequestObjectResult;
            Assert.AreEqual(400,s.StatusCode);
        }


    }
}