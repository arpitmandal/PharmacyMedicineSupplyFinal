using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LINQtoCSV;
using MedicalRepresentativeSchedule.Models;
using Newtonsoft.Json;

namespace MedicalRepresentativeSchedule.Repository
{
    public class ScheduleMeetingRepository : IScheduleMeeting
    {
        List<MedicalRepresentative> MRList = new List<MedicalRepresentative>()
        {
            new MedicalRepresentative(){  MRName = "Jessica" },
            new MedicalRepresentative(){  MRName = "Mandy" },
            new MedicalRepresentative(){  MRName = "Sara" }
        };

        List<string> Dates = new List<string>();
        

        List<Doctor> DoctorList = new List<Doctor>();


        List<MedicineStock> Stockdata = new List<MedicineStock>();

        List<RepSchedule> Meeting = new List<RepSchedule>();
        public dynamic ScheduleMeet(string startDate)
        {
            Dates.Clear();
            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(startDate, culture);
            DateTime start = tempDate.Date;

            int workDays = 0;

            DateTime end = start.AddDays(6);

            while (start != end)
            {
                if (start.DayOfWeek != DayOfWeek.Sunday)
                {

                    Dates.Add(start.ToString().Split(' ')[0]);
                   
                    
                    workDays++;
                }

                start = start.AddDays(1);

                if (workDays == 6)
                { break; }
            }

            var CSVFile = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            try
            {

                var CSV = new CsvContext();
                var doctors = from values in CSV.Read<Doctor>(@"./DoctorsList.csv", CSVFile)
                              select new
                              {
                                  DoctorName = values.DoctorName,
                                  ContactNumber = values.ContactNumber,
                                  TreatingAilment = values.TreatingAilment
                              };

                var Name = doctors.Select(c => c.DoctorName).ToList();
                var Number = doctors.Select(c => c.ContactNumber).ToList();
                var TA = doctors.Select(c => c.TreatingAilment).ToList();


                for (int i = 0; i < Name.Count; i++)
                {
                    Doctor[] doc = new Doctor[Name.Count];
                    doc[i] = new Doctor();
                    doc[i].DoctorName = Name[i];
                    doc[i].ContactNumber = Number[i];
                    doc[i].TreatingAilment = TA[i];
                    DoctorList.Add(doc[i]);


                }
            }
            catch (Exception exception)
            {
                return exception;
               // return BadRequest("Exception occurred " + exception);
            }

            using (HttpClient client = new HttpClient())
            {


                client.BaseAddress = new Uri(" https://localhost:44366/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.GetAsync("MedicineStockInformation").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                Stockdata = JsonConvert.DeserializeObject<List<MedicineStock>>(stringData);

            }


            for (int i = 0; i < Dates.Count; i++)
            {
                RepSchedule rs = new RepSchedule();
                rs.MRName = MRList[(i % MRList.Count)].MRName;
                rs.DoctorName = DoctorList[i].DoctorName;
                rs.TreatingAilment = DoctorList[i].TreatingAilment;
                IList<string> meds = (from s in Stockdata
                                      where s.TargetAilment.Contains(DoctorList[i].TreatingAilment)
                                      select s.Name).ToList();
                string medss = string.Join(",", meds);
                rs.Medicine = medss;
                rs.MeetingSlot = "1 to 2 PM";
                rs.DateofMeeting = Dates[i];
                rs.DoctorContactNumber = DoctorList[i].ContactNumber;
                Meeting.Add(rs);
                meds.Clear();
            }

            return Meeting;
        }

        
    }
}
