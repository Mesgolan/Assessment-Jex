using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public interface IEntity // TODO: extract to it's own file :-)
    {
        int Id { get; set; }
    }

    public class Vacature : IEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Titel { get; set; }
        public required string Omschrijving { get; set; }
        public int BedrijfId { get; set; }
    }

}
