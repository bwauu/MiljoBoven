using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiljoBoven.Models
{
    public class ApplicationDbContext: DbContext
    {
        // To use the DbContext class we need a constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // DBBS1
        // Db-set attributes (One for each table)
        // So basically, we implement getters & setters behaviors to create and access each POCO class.
        // To set/fill a DbContext with data, we need a dataseeder/DBInitializer to fill these DbSets.
        // The next step is therfore to create a DbInitializer.
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<ErrandStatus> ErrandStatuses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
    }
}
