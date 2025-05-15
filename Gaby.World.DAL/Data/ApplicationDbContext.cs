using Gaby.World.DAL.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gaby.World.DAL.Data
{
    /// <summary>
    /// The database model for the application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SettingsDataModel> Settings { get; set; }
        
        /// <summary>
        /// Default constractor, expects DbContextOptions to be passed in
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EntityFramework;User ID=testUser;Password=home1974");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API
            //set index to SettingsDataModel.Name property
            modelBuilder.Entity<SettingsDataModel>().HasIndex(a => a.Name);
        }
    }
}
