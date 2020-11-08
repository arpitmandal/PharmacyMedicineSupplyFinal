using MedicineStockApi.Repository;
using MedicineStockApi.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using MedicineStockApi.Models;
using MedicineStockApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MedicalStockModuleTest
{
    public class Tests
    {
        
        private Mock<IMedicineStockRepository> _repo;
        private Mock<IMedicineStockService> _ser;
        public IMedicineStockService ser;
        public IMedicineStockRepository repo;
        List<MedicineStock> medicinestock;
        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IMedicineStockRepository>();
            _ser = new Mock<IMedicineStockService>();

            medicinestock = new List<MedicineStock>
{
new MedicineStock{Name = "Aspirin",ChemicalComposition = "Acetylsalicyclic acid",TargetAilment = "General",DateOfExpiry= "22-10-2021",NumberOfTabletsInStock= 250},
new MedicineStock {Name = "Codeine",ChemicalComposition = "serotonin",TargetAilment = "Orthopaedics",DateOfExpiry= "20-8-2021",NumberOfTabletsInStock= 100},
new MedicineStock {Name = "Mifepristone",ChemicalComposition = "methotrexate",TargetAilment = "Gynaecology",DateOfExpiry= "1-1-2022",NumberOfTabletsInStock= 300},
new MedicineStock {Name = "Combiflam",ChemicalComposition = "Acetaminophen",TargetAilment = "General",DateOfExpiry= "30-9-2021",NumberOfTabletsInStock= 150},
new MedicineStock {Name = "Misoprostol",ChemicalComposition = "Adenylate cyclase",TargetAilment = "Gynaecology",DateOfExpiry= "22-10-2021",NumberOfTabletsInStock= 200},
new MedicineStock {Name = "Cytotec",ChemicalComposition = "Myo-Inostiol,D-Chiro-Inostiol,L-Methyl Folate",TargetAilment = "Gynaecology ",DateOfExpiry= "15-5-2021",NumberOfTabletsInStock= 200}
 };
              _ser.Setup(x => x.MedicineStockInformation1()).Returns(medicinestock);
               ser = _ser.Object;
              _repo.Setup(m => m.MedicineStockInformation()).Returns(medicinestock);
               repo = _repo.Object;
            
        }

        [Test]
        public void MedicineStockInfoTest()
        {
            List<MedicineStock> answer = ser.MedicineStockInformation1();
            Assert.AreEqual(medicinestock, answer);
            Assert.Pass();
        }
       
        [Test]
        public void MedicineStockInfoTest_PassCase_Service()
        {
            List<MedicineStock> result = ser.MedicineStockInformation1();
            Assert.IsNotNull(result);
        }
        [Test]
        public void MedicineStockInfoTest_FailCase_Service()
        {
            medicinestock = null;
            _ser.Setup(x => x.MedicineStockInformation1()).Returns(medicinestock);
            ser = _ser.Object;
            List<MedicineStock> result = ser.MedicineStockInformation1();
            Assert.IsNull(result);
        }
        [Test]
        public void MedicineStockInfoTest_PassCase_Repository()
        {
            List<MedicineStock> result = repo.MedicineStockInformation();
            Assert.IsNotNull(result);
        }
        [Test]
        public void MedicineStockInfoTest_FailCase_Repository()
        {
            medicinestock = null;
            _repo.Setup(m => m.MedicineStockInformation()).Returns(medicinestock);
            repo = _repo.Object;
            List<MedicineStock> result = repo.MedicineStockInformation();
            Assert.IsNull(result);
        }
        [Test]
        public void GetMedicineStockinfo_ValidInput_OkResult()
        {
            _ser.Setup(x => x.MedicineStockInformation1()).Returns(medicinestock);
            var controller = new MedicineStockController(_ser.Object);
            var data = controller.MedicineStockInformation();
            var res = data as OkObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }
      

    }
    }
