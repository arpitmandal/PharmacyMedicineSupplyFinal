using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRepresentativeSchedule.Repository;

namespace MedicalRepresentativeSchedule.Service
{
    public class ScheduleMeetingService : IScheduleMeetingService
    {

        public readonly IScheduleMeeting repo;
        public ScheduleMeetingService(IScheduleMeeting repo)
        {
            this.repo = repo;
        }


        public dynamic MRScheduleMeet(string startDate)
        {
            var Result = repo.ScheduleMeet(startDate);
            if (Result == null)
            { 
                return null; 
            }
            return Result;
        
        }
            
        
    }
}
