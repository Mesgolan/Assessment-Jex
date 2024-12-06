using BusinessLogic.BusinessObjects.Converters;
using BusinessLogic.BusinessObjects.Dto;
using Database.Entities;
using VacatureApis.Interfaces;

namespace BusinessLogic.Services
{
    // There is no actual UI in this project. I added these services in a businessLogic layer that 
    // is situated between a potential UI layer and the API/data layer.
    // Instead of manual testing, there are unit tests to verify, consolidate and document the working of these functionalities.
    // For this assessment only Create and Read functionality is implemented and under unit tests. Update and Delete would be 
    // more of the same. (They are present and functional in the API layer though.)

    public class VacancyService
    {
        private IController<Vacature> controller;

        public VacancyService(IController<Vacature> controller)
        {
            this.controller = controller;
        }

        public async Task<List<Vacancy>> FetchAllVacanciesAsync()
        {
            var existingItems = await controller.FetchAllAsync();

            var sortedVacancies = existingItems
                .Select(VacancyConverter.ToDto)
                .OrderBy(item => item.Title);

            return sortedVacancies.ToList();
        }

        // In a production environment you might want to catch an exception
        // and return an object to enable the UI display a more user friendly result.
        // For this exercise a simple bool suffices.
        public async Task<bool> AddVacancyAsync(Vacancy item)
        {
            var existingItem = await controller.GetByIdAsync(item.Id);

            if (existingItem != null)
                return false;

            var addedItem = await controller.AddAsync(VacancyConverter.ToBo(item));

            return addedItem != null;
        }
    }
}