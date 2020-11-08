using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeSchedule.Service
{
    public interface IScheduleMeetingService
    {
        public dynamic MRScheduleMeet(string startDate);
    }
}
