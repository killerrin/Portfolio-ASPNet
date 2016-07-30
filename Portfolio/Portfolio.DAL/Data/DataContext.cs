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
        public DbSet<PortfolioEntry> PortfolioEntries { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<PortfolioEntryCategory> PortfolioEntryCategories { get; set; }
        public DbSet<PortfolioEntryFramework> PortfolioEntryFrameworks { get; set; }
        public DbSet<PortfolioEntryPlatform> PortfolioEntryPlatforms { get; set; }
        public DbSet<PortfolioEntryProgrammingLanguage> PortfolioEntryProgrammingLanguages { get; set; }
        public DbSet<PortfolioEntryTag> PortfolioEntryTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
