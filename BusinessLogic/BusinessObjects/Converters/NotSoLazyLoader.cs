using BusinessLogic.BusinessObjects.Dto;

namespace BusinessLogic.BusinessObjects.Converters
{
    // Normally an edmx in the data layer takes care of mapping and loading entities into their parents.
    // Since there is only one such relation in the project, I decided to do it manually. 
    // Extracted the code to this extension method to improve readability of the CompanyService that uses it.

    public static class NotSoLazyLoader
    {
        public static Company LoadVacancies(this Company company, IEnumerable<Vacancy> vacancies )
        {
            company.Vacancies = vacancies.Where(v => v.CompanyId == company.Id).ToList();
            return company;
        }
    }
}