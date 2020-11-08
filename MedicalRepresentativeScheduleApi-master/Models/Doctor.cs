using System;
using System.Collections.Generic;
using LINQtoCSV; 
using System.Linq;
using System.Threading.Tasks;


namespace MedicalRepresentativeSchedule.Models
{
    public class Doctor
    {
        [CsvColumn(FieldIndex = 1)]
        public string DoctorName { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public long ContactNumber { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public string TreatingAilment { get; set; }
    }
}
