using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;
using SystemaVidanta.Models;


namespace SystemaVidanta.DAL
{
    public class SystemVidantaContext : DbContext
    {
      
        private const string secretKey = Crypto.Crypto.KEY;
       public SystemVidantaContext() : base(Crypto.Crypto.SimpleDecryptWithPassword(Environment.GetEnvironmentVariable("SISRESCONTA"), secretKey))
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SystemVidantaContext, SystemaVidanta.Migrations.Configuration>());
        }


        //public DbSet<Seller> Sellers { get; set; }
        //public DbSet<Ride> Rides { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //public DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        
        public virtual DbSet<UserRolesMapping> UserRolesMapping { get; set; }
        public virtual DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        

        public System.Data.Entity.DbSet<SystemaVidanta.Models.Guard> Guards { get; set; }
    }
}