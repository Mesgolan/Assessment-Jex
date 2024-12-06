using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    // I took the liberty to assume that database objects and properties mentioned 
    // in the assessment are required to be in the customer's native language.
    // When using an edmx, everything can of course be mapped to fit the development
    // domain naming convention.
    // For this assessment the Dutch vs English names make it easy to differentiate between 
    // the database entities and the Dto's. (Admitted: It hurts the eyes)
    public class Bedrijf : IEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Naam { get; set; }
        public required string Adres { get; set; }
    }

}