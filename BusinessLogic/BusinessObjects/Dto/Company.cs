namespace BusinessLogic.BusinessObjects.Dto
{
    // I created these data transfer objects as a separation from the entities that are tightly coupled
    // to the database. EF usually generates the data model in code, but it is fruitful to have data objects
    // that are independent from the database and better tailored and trimmed down for their functionality.
    // (Code first vs model first, and auto mappers are an interesting topic for discussion.)


    public class Company
    {
        public Company()
        {
            Vacancies = new List<Vacancy>();
        }

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public IList<Vacancy> Vacancies { get; set; }
    }

}
