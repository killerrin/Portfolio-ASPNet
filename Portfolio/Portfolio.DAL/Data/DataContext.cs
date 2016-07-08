using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Data
{
    public class DataContext : DbContext
    {
        //public static DataContext Instance { get; private set; }
        //public static void BeginTransaction() { Instance = new DataContext(); }
        //public static void EndTransaction()
        //{
        //    //Instance.Dispose();
        //    Instance = null;
        //}

        /// <summary>
        /// You can either pass the NAME of a connection string (e.g. from a web.config), and explicitly declare it.
        /// </summary>
        public DataContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Any entity to be persisted must me declared here.
        /// 
        /// EX: public DbSet<Product> Products { get; set; }
        /// </summary>
        
        /// Migration 001 
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        /// Migration 002
        public DbSet<Category> Categories { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PortfolioEntry> PortfolioEntries { get; set; }
    }
}
