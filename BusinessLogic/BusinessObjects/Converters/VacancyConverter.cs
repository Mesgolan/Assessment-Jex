using BusinessLogic.BusinessObjects.Dto;
using Database.Entities;

namespace BusinessLogic.BusinessObjects.Converters
{
    public class VacancyConverter
    {
        public static Vacancy ToDto(Vacature bo)
        {
            return new Vacancy
            {
                Id = bo.Id,
                Description = bo.Omschrijving,
                Title = bo.Titel,
                CompanyId = bo.BedrijfId
            };
        }

        public static Vacature ToBo(Vacancy dto)
        {
            return new Vacature
            {
                Id = dto.Id,
                Omschrijving = dto.Description,
                Titel = dto.Title,
                BedrijfId = dto.CompanyId
            };
        }
    }
}
