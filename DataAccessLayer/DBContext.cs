using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    /// <summary>
    /// Контекст Entity Framework для работы с базой данных работников предприятия.
    /// </summary>
    public class DBContext : DbContext
    {
        public DBContext() : base("name=DBContext")
        {
           
        }

        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>()
                .HasKey(w => w.ID);


            modelBuilder.Entity<Worker>()
                .Property(w => w.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Worker>()
                .ToTable("Workers");

            base.OnModelCreating(modelBuilder);
        }
    }
}
