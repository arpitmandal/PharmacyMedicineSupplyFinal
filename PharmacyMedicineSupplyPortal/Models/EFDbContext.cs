using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Models
{
    public class EFDbContext:DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Demands> Demands { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        

    }
}
