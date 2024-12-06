using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository
{
    public interface IContextRepository
    {
        DbSet<Vacature> Vacatures { get; }
        DbSet<Bedrijf> Bedrijven { get; }
        Task SaveChangesAsync();
    }
}
