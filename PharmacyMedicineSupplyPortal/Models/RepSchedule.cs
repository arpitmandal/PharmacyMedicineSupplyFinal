using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class RepSchedule
    {
            public string MRName { get; set; }
            public string DoctorName { get; set; }
            public string TreatingAilment { get; set; }
            public string Medicine { get; set; }
            public string MeetingSlot { get; set; }
            public string DateofMeeting { get; set; }
            public long DoctorContactNumber { get; set; }
        
    }
}
