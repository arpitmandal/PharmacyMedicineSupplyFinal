using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Charts;
using MedicalRepresentativeSchedule.Controllers;
using MedicalRepresentativeSchedule.Models;
using MedicalRepresentativeSchedule.Repository;
using MedicalRepresentativeSchedule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace NUnitTestMRSchedule
{
    public class Tests
    {
        List<RepSchedule> MRList;
        List<RepSchedule> MRList2;
        List<MedicineStock> StockList;
        
        ScheduleMeetingService serv;

        Mock<IScheduleMeetingService> service;
       
        ScheduleMeetingController controller;
       
        Mock<IScheduleMeeting> repo;
        
        ScheduleMeetingRepository repos;
        
        Mock<IConfiguration> config = new Mock<IConfiguration>();
        
        
        [SetUp]
        public void Setup()
        {

            repo = new Mock<IScheduleMeeting>();
            service = new Mock<IScheduleMeetingService>();

            MRList = new List<RepSchedule>() {
            new RepSchedule{ MRName="Jessica",DoctorName="Dr. Ashutosh",TreatingAilment="Orthopaedics",Medicine="Tibect",MeetingSlot="1 to 2 PM",DateofMeeting="11-11-2020", DoctorContactNumber=9897931910 },
            new RepSchedule{ MRName="Manddy",DoctorName="Dr. Anupam",TreatingAilment="General",Medicine="Aspirin",MeetingSlot="1 to 2 PM",DateofMeeting="13-11-2020", DoctorContactNumber=9568144111}
            };
            MRList2 = new List<RepSchedule>();
            StockList = new List<MedicineStock> { new MedicineStock{ Name = "Aspirin",
            ChemicalComposition = "Acetylsalicyclic acid",
            TargetAilment = "General",
            DateOfExpiry = "22-10-2021",
            NumberOfTabletsInStock = 250
            },new MedicineStock{    Name = "Mifepristone",
            ChemicalComposition = "methotrexate",
            TargetAilment = "Gynaecology",
            DateOfExpiry= "1-1-2022",
            NumberOfTabletsInStock= 300
             } };
            var date = "2020/11/9";
            repo.Setup(s => s.ScheduleMeet(date)).Returns(MRList2);


            serv = new ScheduleMeetingService(repo.Object);
            service.Setup(s => s.MRScheduleMeet(date)).Returns(MRList);

            List<MedicineStock> StockList1 = new List<MedicineStock>();
            repos = new ScheduleMeetingRepository(config.Object);
            repo.Setup(s => s.getStockApiData()).Returns(StockList1);
 
            List<Doctor> doc = new List<Doctor>();
            repo.Setup(s => s.ReadDoctorsCsv()).Returns(doc);
            
            controller = new ScheduleMeetingController(config.Object, service.Object);

        }

        
        [TestCase("2020/11/9")]
        [TestCase("2020/11/10")]
        [TestCase("2020/11/11")]
        public void ScheduleMeetingController_GetMeetingStartDate(string date1)
        {
            
    
            var data = controller.GetMeetingStartDate(date1);
            Assert.IsNotNull(data);
            
        }
       
        [Test]
        public void ScheduleMeetingRepository_ReadDoctorsCsv()
        {
            var data = repos.ReadDoctorsCsv();
            Assert.IsNotNull(data);
         }
        [Test]
        public void ScheduleMeetingRepository_getStockApiData()
        {
            var data = repos.getStockApiData();
            Assert.IsNotNull(data);

        }
        [TestCase("2020/11/90")]
        [TestCase("2020/110/10")]
        [TestCase("220/11/11")]
        public void ScheduleMeetingController_InvalidInput(string date1)
        {

           
            var data = controller.GetMeetingStartDate(date1);
            var s = data as BadRequestObjectResult;
            //Assert.AreEqual(400, s.StatusCode);
            Assert.IsNull(s);
        }

        [TestCase("2020/12/19")]
        [TestCase("2020/11/10")]
        [TestCase("2020/10/1")]
        public void ScheduleMeetingRepository_ScheduleMeet(string date1)
        {
            var data = repos.ScheduleMeet(date1);
            var c = data.Count;
            Assert.AreEqual(5, c);
           

        }
       

    }
}