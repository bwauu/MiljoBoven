using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
/*
 * ------------------------------------- GENERAL INFORMATION OF THIS EFC ---------------------------------------------------------
 * The context class in Entity Framework is a class which derives from System.Data.Entity.DbContextDbContext in EF 6 and EF Core both.
 * An instance of the context class represents Unit Of Work and Repository patterns wherein it can combine multiple changes under a single database transaction.
 */
namespace MiljoBoven.Models
{

    /*
     * The context class is used to query or save data to the database. 
     * It is also used to configure domain classes, database related mappings, change tracking settings, caching, transaction etc.
     * This following ApplicationDbContext class is an example of a context class
     */
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {
        }

        // To use the DbContext class we need a constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // DBBS1
        // Db-set attributes (One for each table)
        // So basically, we implement getters & setters behaviors to create and access each POCO class.
        // To set/fill a DbContext with data, we need a dataseeder/DBInitializer to fill these DbSets.
        // The next step is therfore to create a DbInitializer.

        // Entities == {Department, Employees, Errands, ErrandStatus, Picture, Samples, Sequences}
        /*
         * An entity in Entity Framework is a class that maps to a database table. 
         * This class must be included as a DbSet<TEntity> type property in the DbContext class. 
         * EF API maps each entity to a table and each property of an entity to a column in the database.*/

        /************************************************ALL TABLES IN DB************************************* */
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<ErrandStatus> ErrandStatuses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
    }
}
