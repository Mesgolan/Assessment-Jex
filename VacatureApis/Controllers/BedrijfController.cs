using Database.Entities;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using VacatureApis.Interfaces;

namespace VacatureApis.Controllers
{
    public class BedrijfController : IController<Bedrijf>
    {
        private IContextRepository _db;

        public BedrijfController(IContextRepository db)
        {
            _db = db;
        }

        public async Task<List<Bedrijf>> FetchAllAsync()
        {
            return await _db.Bedrijven.ToListAsync();
        }

        public async Task<Bedrijf?> GetByIdAsync(int bedrijfId)
        {
            return await _db.Bedrijven.FindAsync(bedrijfId);
        }

        public async Task<Bedrijf?> AddAsync(Bedrijf item)
        {
            if (_db.Bedrijven.Any(v => v.Id == item.Id))
                return null;

            _db.Bedrijven.Add(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<Bedrijf?> UpdateAsync(Bedrijf item)
        {
            var existingBedrijf = await GetByIdAsync(item.Id);

            if (existingBedrijf == null)
                return null;

            existingBedrijf.Naam = item.Naam;
            existingBedrijf.Adres= item.Adres;

            await _db.SaveChangesAsync();

            return existingBedrijf;
        }

        public async Task<IResult> DeleteAsync(int bedrijfId)
        {
            if (await _db.Bedrijven.FindAsync(bedrijfId) is Bedrijf bedrijfToRemove)
            {
                if (HasVacatures(bedrijfToRemove))
                    return CanNotDeleteMessage();

                _db.Bedrijven.Remove(bedrijfToRemove);
                await _db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }

        private bool HasVacatures(Bedrijf item)
        {
            return _db.Vacatures.Any(v => v.BedrijfId == item.Id);
        }

        private static IResult CanNotDeleteMessage()
        {
            return Results.ValidationProblem(errors: new Dictionary<string, string[]> { { "Company can not be deleted", new string[] { "One or more vacancies are attached." } } });
        }
    }
}