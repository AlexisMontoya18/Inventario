using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;
using SystemaVidanta.Models;
using SystemaVidanta.Helppers;
using System.Data.Entity.Infrastructure;

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
        public virtual DbSet<Bitacora> Bitacoras {get; set;}
        public virtual DbSet<UserRolesMapping> UserRolesMapping { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Resguardo> Resguardos { get; set; }
        public virtual DbSet<ResguardoDetalle> DetallesResguardo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }


        public override int SaveChanges()
        {
            #region modificaciones a la bitacora

            string currentValue = "";
            var modifiedChanges = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            foreach (var change in modifiedChanges)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.GetDatabaseValues().GetValue<object>(prop);
                    if (change.CurrentValues[prop] != null)
                    {
                        currentValue = change.CurrentValues[prop].ToString();
                    }
                    else
                    {
                        currentValue = null;
                    }
                    if (originalValue == null)
                    {
                        originalValue = "";

                        if (currentValue == null)
                        {
                            currentValue = "";
                            if (originalValue.ToString() != currentValue)
                            {
                                Bitacora log = new Bitacora()
                                {
                                    Accion = "Modificación",
                                    Tabla = entityName,
                                    PrimaryKeyValue = primaryKey.ToString(),
                                    PropertyModified = prop,
                                    OldValue = originalValue.ToString(),
                                    NewValue = currentValue,
                                    Fecha = now,
                                    IdUsuario = keysGet.keygeyuser()
                                };
                                Bitacoras.Add(log);
                            }
                        }
                        else
                        {
                            if (originalValue.ToString() != currentValue)
                            {
                                Bitacora log = new Bitacora()
                                {
                                    Accion = "Modificación",
                                    Tabla = entityName,
                                    PrimaryKeyValue = primaryKey.ToString(),
                                    PropertyModified = prop,
                                    OldValue = originalValue.ToString(),
                                    NewValue = currentValue,
                                    Fecha = now,
                                    IdUsuario = keysGet.keygeyuser()
                                };
                                Bitacoras.Add(log);
                            }
                        }

                    }
                    else if (currentValue == null)
                    {
                        currentValue = "";
                        if (originalValue.ToString() != currentValue)
                        {
                            Bitacora log = new Bitacora()
                            {
                                Accion = "Modificación",
                                Tabla = entityName,
                                PrimaryKeyValue = primaryKey.ToString(),
                                PropertyModified = prop,
                                OldValue = originalValue.ToString(),
                                NewValue = currentValue,
                                Fecha = now,
                                IdUsuario = keysGet.keygeyuser()
                            };
                            Bitacoras.Add(log);
                        }
                    }
                    else
                    {
                        if (originalValue.ToString() != currentValue)
                        {
                            Bitacora log = new Bitacora()
                            {
                                Accion = "Modificación",
                                Tabla = entityName,
                                PrimaryKeyValue = primaryKey.ToString(),
                                PropertyModified = prop,
                                OldValue = originalValue.ToString(),
                                NewValue = currentValue,
                                Fecha = now,
                                IdUsuario = keysGet.keygeyuser()
                            };
                            Bitacoras.Add(log);
                        }
                    }
                }
            }
            #endregion



            return base.SaveChanges();
        }


    }
}