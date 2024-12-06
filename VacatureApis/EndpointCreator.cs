using Database.Entities;
using Database.Setup;
using VacatureApis.Controllers;

namespace VacatureApis
{
    public class EndpointCreator
    {
        private readonly WebApplication application;

        public EndpointCreator(WebApplication app)
        {
            application = app;
        }

        public void InitializeAllEndPoints()
        {
            Create_GetAllBedrijven();
            Create_AddBedrijf();
            Create_UpdateBedrijf();
            Create_DeleteBedrijf();

            Create_GetAllVacatures();
            Create_AddVacature();
            Create_UpdateVacature();
            Create_DeleteVacature();
        }

        private void Create_GetAllBedrijven()
        {
            application.MapGet("/allbedrijven", async (CorporateDatabase db) =>
                await new BedrijfController(db).FetchAllAsync());
        }

        private void Create_AddBedrijf()
        {
            application.MapPost("/addbedrijf", async (Bedrijf item, CorporateDatabase db) =>
            {
                var createdItem = await new BedrijfController(db).AddAsync(item);

                return createdItem == null
                    ? Results.ValidationProblem(errors: GenerateError("A company with that identifier already exists."))
                    : Results.Created($"/addbedrijf/{createdItem.Naam}", item);
            });
        }

        private void Create_UpdateBedrijf()
        {
            application.MapPost("/updatebedrijf", async (Bedrijf item, CorporateDatabase db) =>
            {
                var updatedItem = await new BedrijfController(db).UpdateAsync(item);

                return updatedItem == null
                    ? Results.ValidationProblem(errors: GenerateError("The company to update is not found."))
                    : Results.Ok(item);

            });
        }

        private void Create_DeleteBedrijf()
        {
            application.MapDelete("/deletebedrijf/{id}", async (int id, CorporateDatabase db) =>
            {
                var result = await new BedrijfController(db).DeleteAsync(id);

                return result;
            });
        }

        private void Create_GetAllVacatures()
        {
            application.MapGet("allvacatures", (CorporateDatabase db) =>
                new VacatureController(db).FetchAllAsync());
        }

        private void Create_AddVacature()
        {
            application.MapPost("/addvacature", async (Vacature item, CorporateDatabase db) =>
            {
                var createdItem = await new VacatureController(db).AddAsync(item);

                return createdItem == null
                    ? Results.ValidationProblem(errors:GenerateError("A vacancy with that identifier already exists."))
                    : Results.Created($"/addvacature/{createdItem.Titel}", item);
            });
        }

        private void Create_UpdateVacature()
        {
            application.MapPost("/updatevacature", async (Vacature item, CorporateDatabase db) =>
            {
                var updatedItem = await new VacatureController(db).UpdateAsync(item);

                return updatedItem == null
                    ? Results.ValidationProblem(errors: GenerateError("The vacancy to update is not found."))
                    : Results.Ok(item);
            });
        }

        private void Create_DeleteVacature()
        {
            application.MapDelete("/deletevacature/{id}", async (int id, CorporateDatabase db) =>
            {
                var result = await new VacatureController(db).DeleteAsync(id);

                return result;
            });
        }

        private Dictionary<string, string[]> GenerateError(string errorText)
        {
            return new Dictionary<string, string[]>
            {
                { "Simple error collection", new string[] { errorText } }
            };
        }
    }
}