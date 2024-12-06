using BusinessLogic.BusinessObjects.Converters;
using BusinessLogic.BusinessObjects.Dto;
using Database.Entities;
using VacatureApis.Interfaces;

namespace BusinessLogic.Controllers
{
    // This CompanyService covers the user story about getting companies that have vacancies associated to them.
    //
    // Fetching all records from the database, as list, is not very efficient of course. In this assessment it is
    // done to easily create data to be returned from the mock controllers. In a real situation, and with a more
    // elaborate Repository pattern in place, it is a nice trick to 'get all AsQuerable' somewhere in
    // the repository structure. This has little effect on performance but allows for easy data mocking in  unit tests.
    // 
    // Other crud actions are omitted because they are more of the same, and already done in the other service.

    public class CompanyService
    {
        private IController<Bedrijf> bedrijfController;
        private IController<Vacature> vacatureController;

        public CompanyService(IController<Vacature> vacatureController, IController<Bedrijf> bedrijfController)
        {
            this.vacatureController = vacatureController;
            this.bedrijfController = bedrijfController;
        }

        public async Task<List<Company>> FetchCompaniesWithVacanciesAsync()
        {
            var allVacatures = await vacatureController.FetchAllAsync();
            var allbedrijven = await bedrijfController.FetchAllAsync();

            var companiesWithVacancies = allbedrijven.Where(b => allVacatures.Any(v => v.BedrijfId == b.Id))
                .Select(b => CompanyConverter.ToDto(b).LoadVacancies(allVacatures.Select(VacancyConverter.ToDto)));

            return companiesWithVacancies
                .OrderBy(item => item.Name).ToList();
        }
    }
}
