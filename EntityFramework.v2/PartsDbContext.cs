using Microsoft.EntityFrameworkCore;

namespace EntityFramework.v2
{
    public class PartsDbContext : DbContext
    {
        public PartsDbContext(DbContextOptions<PartsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.Part_ID);
                entity.Property(e => e.Part_Name).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Part_Number).HasMaxLength(100);
                entity.Property(e => e.Descriptions).HasMaxLength(250);
            });
        }
    }
}
