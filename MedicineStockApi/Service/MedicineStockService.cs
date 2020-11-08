using MedicineStockApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineStockApi.Service
{
    public class MedicineStockService : IMedicineStockService
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineStockService));
        public readonly IMedicineStockRepository repo;
        public MedicineStockService(IMedicineStockRepository repo)
        {
            this.repo = repo;
        }
        public dynamic MedicineStockInformation1()
        {
            try
            {
                var Result = repo.MedicineStockInformation();

                if (Result == null)
                { return null; }
                _log4net.Info("Medicine Information fetched");
                return Result;
            }
            catch (Exception E)
            { _log4net.Error(" MedicineStockInformation  encountered an Exception :" + E.Message);
                return "MedicineStock Returned null While fetching data";
            }
          
        }
    }
}
