using BusinessLogic.BusinessObjects.Converters;
using BusinessLogic.BusinessObjects.Dto;
using BusinessLogic.Controllers;
using Database.Entities;
using NSubstitute;
using VacatureApis.Interfaces;

namespace BusinessLogic.Tests.Services
{
    [TestClass]
    public class CompanyServiceTests
    {
        private CompanyService service;
        private IController<Bedrijf> bedrijfController = Substitute.For<IController<Bedrijf>>();
        private IController<Vacature> vacatureController = Substitute.For<IController<Vacature>>();

        private List<Bedrijf> storedBedrijven;
        private List<Vacature> storedVacatures;

        [TestInitialize]
        public void Initialize()
        {
            service = new CompanyService(vacatureController, bedrijfController);
            storedBedrijven = new List<Bedrijf>();
            storedVacatures = new List<Vacature>();

            bedrijfController.FetchAllAsync().Returns(storedBedrijven);
            vacatureController.FetchAllAsync().Returns(storedVacatures);
        }

        [TestMethod]
        public async Task ShouldFetchCompanyWithSingleVacancy()
        {
            var company = AddCompany(21, "Alpha");
            var vacancy = AddVacancy(31, "A job", company);

            var foundCompanies = await service.FetchCompaniesWithVacanciesAsync();

            Assert.IsTrue(foundCompanies.Any());
        }

        [TestMethod]
        public async Task ShouldReturnEmptyListWhenNoDataPresent()
        {
            var foundCompanies = await service.FetchCompaniesWithVacanciesAsync();

            Assert.IsFalse(foundCompanies.Any());
        }

        [TestMethod]
        public async Task ShouldReturnEmptyListWhenNoCompaniesFound()
        {
            var companyA = AddCompany(21, "Alpha");

            var foundCompanies = await service.FetchCompaniesWithVacanciesAsync();

            Assert.IsFalse(foundCompanies.Any());
        }

        [TestMethod]
        public async Task ShouldFetchCompanyWithMultipleVacancies()
        {
            var companyA = AddCompany(21, "Alpha");
            var vacancy1 = AddVacancy(31, "1st job", companyA);
            var vacancy2 = AddVacancy(32, "2nd job", companyA);

            var foundCompanies = await service.FetchCompaniesWithVacanciesAsync();

            Assert.IsTrue(foundCompanies.Any());
        }

        [TestMethod]
        public async Task ShouldFilterOutCompaniesWith_No_Vacancies()
        {
            var companyA = AddCompany(21, "Alpha");
            var companyB = AddCompany(22, "Bravo");
            var vacancy1 = AddVacancy(31, "1st job", companyA);

            var foundCompanies = await service.FetchCompaniesWithVacanciesAsync();

            Assert.AreEqual(21, foundCompanies.Single().Id);
        }


        // It is often questioned why there are so many hard coded values in my unit tests.
        // The answer is simple: they stand out more than constants and thus make the code
        // easier to read. A disadvantage of asserting on properties is that they can accidentally
        // be altered, resulting in a false positive result. (e.g. when all objects have Guid.Empty as Id)
        [TestMethod]
        public async Task ShouldSortFoundCompaniesInAlphabeticalOrder()
        {
            var companyB = AddCompany(21, "Bravo");
            var companyA = AddCompany(22, "Alpha");
            var companyD = AddCompany(23, "Delta");
            var companyC = AddCompany(24, "Carly");
            var vacancy1 = AddVacancy(31, "99th job", companyA);
            var vacancy2 = AddVacancy(32, "77th job", companyB);
            var vacancy3 = AddVacancy(33, "88th job", companyD);

            var foundCompanies = await service.FetchCompaniesWithVacanciesAsync();

            Assert.AreEqual("Alpha", foundCompanies[0].Name);
            Assert.AreEqual("Bravo", foundCompanies[1].Name);
            Assert.AreEqual("Delta", foundCompanies[2].Name);
        }


        private Company AddCompany(int id, string name, string address = "Samestreet 42")
        {
            var item = new Company
            {
                Id = id,
                Name = name,
                Address = address
            };

            storedBedrijven.Add(CompanyConverter.ToBo(item));
            return item;
        }

        private Vacancy AddVacancy(int id, string title, Company company)
        {
            var item = new Vacancy
            {
                Title = title,
                Id = id,
                Description = "A description",
                CompanyId = company.Id
            };
            company.Vacancies.Add(item);

            storedVacatures.Add(VacancyConverter.ToBo(item));
            return item;
        }
    }
}