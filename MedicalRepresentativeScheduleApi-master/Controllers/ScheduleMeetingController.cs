using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LINQtoCSV;
using MedicalRepresentativeSchedule.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using MedicalRepresentativeSchedule.Service;

namespace MedicalRepresentativeSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleMeetingController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ScheduleMeetingController));
        private IConfiguration configuration;
        private readonly IScheduleMeetingService service;
        public ScheduleMeetingController(IConfiguration config, IScheduleMeetingService service)
        {
            configuration = config;
            this.service = service;
        }


//[HttpPost]
        [HttpGet]
        [ActionName("ScheduleMeeting")]
        public IActionResult GetMeetingStartDate(string startDate)
        {
            _log4net.Info(" Http Post request");
            var MeetingSchedule = service.MRScheduleMeet(startDate);
            if (MeetingSchedule == null)
            {
                return BadRequest();
            }
            return Ok(MeetingSchedule);

        }
    }
}