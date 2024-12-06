namespace Database.Setup
{
    using System.Threading.Tasks;
    using Database.Entities;
    using Database.Repository;
    using Microsoft.EntityFrameworkCore;

    public class CorporateDatabase : DbContext, IContextRepository
    {
        public CorporateDatabase(DbContextOptions<CorporateDatabase> options)
                : base(options) { }

        public DbSet<Vacature> Vacatures => Set<Vacature>();
        public DbSet<Bedrijf> Bedrijven => Set<Bedrijf>();

        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}