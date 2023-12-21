using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orion.DataAccess.Setting;
using Orion.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.DataAccess
{
    public class AppDbContext: IdentityDbContext<User, Roles, int>
    {
        public AppDbContext()
        {
                
        }
        public AppDbContext(DbContextOptions op):base(op)
        {
                
        }

    
        public DbSet<Contact> Contacts { get; set; }
  
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(DbSetting.DbConnectionString);
        }
       
    }
}
