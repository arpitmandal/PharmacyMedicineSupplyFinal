using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyApi.Models
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<PharmacyMedicineSupply,PharmacyMedicineSupplyDto>();
        }
    }
}
