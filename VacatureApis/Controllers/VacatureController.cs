using Database.Entities;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using VacatureApis.Interfaces;

namespace VacatureApis.Controllers
{

    public class VacatureController : IController<Vacature>
    {
        private IContextRepository _db;

        public VacatureController(IContextRepository db)
        {
            _db = db;
        }

        public async Task<List<Vacature>> FetchAllAsync()
        {
            return await _db.Vacatures.ToListAsync();
        }

        public async Task<Vacature?> GetByIdAsync(int vacatureId)
        {
            return await _db.Vacatures.FindAsync(vacatureId);
        }

        public async Task<Vacature?> AddAsync(Vacature item)
        {
            if (_db.Vacatures.Any(v => v.Id == item.Id))
                return null;

            _db.Vacatures.Add(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<Vacature?> UpdateAsync(Vacature item)
        {
            var existingVacature = await GetByIdAsync(item.Id);

            if (existingVacature == null)
                return null;

            existingVacature.Titel = item.Titel;
            existingVacature.Omschrijving = item.Omschrijving;

            await _db.SaveChangesAsync();

            return existingVacature;
        }

        public async Task<IResult> DeleteAsync(int vacatureId)
        {
            if (await _db.Vacatures.FindAsync(vacatureId) is Vacature item)
            {
                _db.Vacatures.Remove(item);
                await _db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
    }
}