using AutoMapper;
using Moq;
using NUnit.Framework;
using PharmacyMedicineSupplyApi.Controllers;
using PharmacyMedicineSupplyApi.Models;
using PharmacyMedicineSupplyApi.Respository;
using PharmacyMedicineSupplyApi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyTest
{
    class MedcineSupplyServiceAndRepoTest
    {
        List<PharmacyMedicineSupply> supplyList;
        Mock<IMapper> mapper = new Mock<IMapper>();
        MedicineDemand medicineDemand;
        Mock<ISupply> supplyRepo;
        [SetUp]
        public void Setup()
        {
            supplyList = supplyList = new List<PharmacyMedicineSupply>() {
            new PharmacyMedicineSupply{ MedicineName="Aspirin",PharmacyName="Apollo Pharmacy",SupplyCount=50},
            new PharmacyMedicineSupply{ MedicineName="Aspirin",PharmacyName="Max Pharmacy",SupplyCount=50}
            };

            medicineDemand = new MedicineDemand()
            {
                Medicine = "Aspirin",
                Demand = 100
            };
           supplyRepo = new Mock<ISupply>();
            supplyRepo.Setup(s => s.GetSupplies(medicineDemand.Medicine, medicineDemand.Demand)).ReturnsAsync(supplyList);
            
        }

        [Test]
        public async Task MedicineSupply_ServiceLayer_ValidInput_Pass()
        {
            MedicineSupplyService supplyService = new MedicineSupplyService(supplyRepo.Object);
            List<PharmacyMedicineSupply> serviceSupplyList = await supplyService.MedcineSupply(medicineDemand.Medicine,medicineDemand.Demand);
            Assert.AreEqual(2, serviceSupplyList.Count);
        }

        [Test]
        public async Task MedicineSupply_ServiceLayer_InValidMedicineName_NullOutput()
        {
            MedicineDemand demandOfSupplier = new MedicineDemand() { 
            Medicine="abc",
            Demand=100
            };
            MedicineSupplyService supplyService = new MedicineSupplyService(supplyRepo.Object);
            List<PharmacyMedicineSupply> serviceSupplyList = await supplyService.MedcineSupply(demandOfSupplier.Medicine, demandOfSupplier.Demand);
            Assert.IsNull(serviceSupplyList);
        }
        /*
        [Test]
        public async Task MedicineSupply_InValidMedicineName_NullOutput()
        {
            try
            {
                MedicineDemand demandOfSupplier = new MedicineDemand()
                {
                    Medicine = "abc",
                    Demand = 100
                };
                Mock<ISupply> supplyRepo = new Mock<ISupply>();
                supplyRepo.Setup(s => s.GetSupplies(medicineDemand.Medicine, medicineDemand.Demand)).ReturnsAsync(supplyList);
                MedicineSupplyService supplyService = new MedicineSupplyService(supplyRepo.Object);
                List<PharmacyMedicineSupply> serviceSupplyList = await supplyService.MedcineSupply(demandOfSupplier.Medicine, demandOfSupplier.Demand);
                Assert.AreEqual(0, serviceSupplyList.Count);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.",e.Message);
            }
        }*/
        

        [Test]
        public async Task MedicineSupply_ServiceLayer_InValidDemandCount_NullOutput()
        {
            MedicineDemand demandOfSupplier = new MedicineDemand()
            {
                Medicine = "Aspirin",
                Demand = 0
            };
            MedicineSupplyService supplyService = new MedicineSupplyService(supplyRepo.Object);
            List<PharmacyMedicineSupply> serviceSupplyList = await supplyService.MedcineSupply(demandOfSupplier.Medicine, demandOfSupplier.Demand);
            Assert.IsNull(serviceSupplyList);
        }

        [Test]
        public async Task GetSupplies_RepositoryLayer_ValidInput_Pass()
        {
            MedicineDemand demandOfSupplier = new MedicineDemand()
            {
                Medicine = "Aspirin",
                Demand = 100
            };
            ISupply repo = supplyRepo.Object;
            var result = await repo.GetSupplies(demandOfSupplier.Medicine, demandOfSupplier.Demand);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetSupplies_RepositoryLayer_InValidDemand_Fail()
        {
            MedicineDemand demandOfSupplier = new MedicineDemand()
            {
                Medicine = "Aspirin",
                Demand = 0
            };
            ISupply repo = supplyRepo.Object;
            var result = await repo.GetSupplies(demandOfSupplier.Medicine, demandOfSupplier.Demand);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetSupplies_RepositoryLayer_InValidMedicineName_Fail()
        {
            MedicineDemand demandOfSupplier = new MedicineDemand()
            {
                Medicine = "abc",
                Demand = 100
            };
            ISupply repo = supplyRepo.Object;
            var result = await repo.GetSupplies(demandOfSupplier.Medicine, demandOfSupplier.Demand);
            Assert.IsNull(result);
        }
    }
}
