using Microsoft.EntityFrameworkCore;

namespace Database.Setup
{
    // Using the InMemoryDatabase is not a recommended practice.
    // The main reason I chose to use it anyway is that my
    // laptop was already complaining about memory and disc space
    // before installing the latest Visual Studio.
    public class DbBuilder
    {
        public WebApplication BuildWithInMemoryDatabase(string dbName)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddDbContext<CorporateDatabase>(opt => opt.UseInMemoryDatabase(dbName));

            return builder.Build();
        }
    }
}