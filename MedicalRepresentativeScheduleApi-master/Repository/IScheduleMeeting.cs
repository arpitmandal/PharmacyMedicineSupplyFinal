using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeSchedule.Repository
{
   public interface IScheduleMeeting
    {
        public dynamic ScheduleMeet(string startDate);
        public dynamic ReadDoctorsCsv();
        public dynamic getStockApiData();
    }
}
