using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Model;

namespace SalesAPI.Data
{
    public class SalesDBContext : DbContext
    {
        public SalesDBContext(DbContextOptions<SalesDBContext> options):base(options){}
        

        public DbSet<Sale> Sales { get; set; }
    }
}
