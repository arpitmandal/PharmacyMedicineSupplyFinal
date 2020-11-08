using MedicineStockApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MedicineStockApi.Repository
{
    public class MedicineStockRepository : IMedicineStockRepository
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineStockController));
        public static List<Models.MedicineStock> MedicineDetails = new List<Models.MedicineStock>() { new Models.MedicineStock{
            Name = "Aspirin",
            ChemicalComposition = "Acetylsalicyclic acid",
            TargetAilment = "General",
            DateOfExpiry= "22-10-2021",
            NumberOfTabletsInStock= 250
            },
           new Models.MedicineStock {    Name = "Codeine",
            ChemicalComposition = "serotonin",
            TargetAilment = "Orthopaedics",
            DateOfExpiry= "20-8-2021",
            NumberOfTabletsInStock= 100
             },
            new Models.MedicineStock {    Name = "Mifepristone",
            ChemicalComposition = "methotrexate",
            TargetAilment = "Gynaecology",
            DateOfExpiry= "1-1-2022",
            NumberOfTabletsInStock= 300
             },
            new Models.MedicineStock {    Name = "Combiflam",
            ChemicalComposition = "Acetaminophen",
            TargetAilment = "General",
            DateOfExpiry= "30-9-2021",
            NumberOfTabletsInStock= 150
             },
            new Models.MedicineStock {    Name = "Misoprostol",
            ChemicalComposition = "Adenylate cyclase",
            TargetAilment = "Gynaecology",
            DateOfExpiry= "22-10-2021",
            NumberOfTabletsInStock= 200
             },
            new Models.MedicineStock {    Name = "Cytotec",
            ChemicalComposition = "Myo-Inostiol,D-Chiro-Inostiol,L-Methyl Folate",
            TargetAilment = "Gynaecology ",
            DateOfExpiry= "15-5-2021",
            NumberOfTabletsInStock= 200
             }};
        public dynamic MedicineStockInformation()
        {
            try
            {
                if (MedicineDetails.ToList() == null)
                {
                    _log4net.Info("Null List Returned");
                    return null; 
                }
                else
                {
                    _log4net.Info("Medicine List Returned");
                    return MedicineDetails.ToList();
                }
            }
            catch (Exception E)
            {
                _log4net.Error(" Http GetSupplies encountered an Exception :" + E.Message);
                return "Some Error While fetching request";
            }
            
        }

    }
}
