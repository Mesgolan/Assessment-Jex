using BusinessLogic.BusinessObjects.Dto;
using Database.Entities;

namespace BusinessLogic.BusinessObjects.Converters
{
    public class CompanyConverter
    {
        public static Company ToDto(Bedrijf bo)
        {
            return new Company
            {
                Id = bo.Id,
                Name = bo.Naam,
                Address = bo.Adres
            };
        }

        public static Bedrijf ToBo(Company dto)
        {
            return new Bedrijf
            {
                Id = dto.Id,
                Naam = dto.Name,
                Adres = dto.Address
            };
        }
    }
}
