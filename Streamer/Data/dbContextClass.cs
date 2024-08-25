using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Streamer.Models;

namespace Streamer.Data
{
    public class dbContextClass : DbContext
    {
        protected readonly IConfiguration configuration;
        public dbContextClass(IConfiguration configuration_) {
            configuration = configuration_;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Data Source=DESKTOP-TVDNQ4N;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        }
        public DbSet<productClass> Product { get; set; }
    }
}
    