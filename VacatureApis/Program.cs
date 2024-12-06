using Database.Setup;
using VacatureApis;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new DbBuilder().BuildWithInMemoryDatabase("ProductionDb");
        new EndpointCreator(app).InitializeAllEndPoints();

        app.MapGet("/", () => "Hello, Thank you for reviewing this rest api project.\r\nThere is no actual UI to use the API, but the VacatureApis.http file " +
        "contains all requests needed to verify the endpoints.\r\n" +
        "Also interesting might be the unit tests for the services in the BusinessLogic layer.");

        app.Run();
    }
}