namespace BusinessLogic.BusinessObjects.Dto
{
    public class Vacancy
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int CompanyId { get; set; }
    }

}
