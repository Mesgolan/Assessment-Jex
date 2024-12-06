using BusinessLogic.BusinessObjects.Converters;
using BusinessLogic.BusinessObjects.Dto;
using BusinessLogic.Services;
using Database.Entities;
using NSubstitute;
using VacatureApis.Interfaces;
using Xunit.Sdk;

namespace BusinessLogic.Tests.Services
{
    [TestClass]
    public sealed class VacancyServiceTests
    {
        private VacancyService service;
        private IController<Vacature> vacatureController = Substitute.For<IController<Vacature>>();

        private List<Vacature> storedVacatures;

        [TestInitialize]
        public void Initialize()
        {
            service = new VacancyService(vacatureController);
            storedVacatures = new List<Vacature>();
        }

        [TestMethod]
        public async Task ShouldAddVacancy()
        {
            var item = CreateVacancy(11, "Janitor");

            _ = await service.AddVacancyAsync(item);

            _ = vacatureController.Received().AddAsync(Arg.Any<Vacature>());
        }

        [TestMethod]
        public async Task Should_Not_AddVacancyWithDuplicateIdentifier()
        {
            var item1 = CreateVacancy(11, "Janitor");
            var item2 = CreateVacancy(11, "Duplicate Key");
            AddExistingVacancyToMockDatabase(item1);

            var isAdded2 = await service.AddVacancyAsync(item2);

            _ = vacatureController.DidNotReceive().AddAsync(Arg.Any<Vacature>());
        }

        [TestMethod]
        public async Task ShouldAdd_Multiple_Vacancies()
        {
            var item1 = CreateVacancy(11, "Janitor");
            var item2 = CreateVacancy(12, "Janitor");

            var isAdded1 = await service.AddVacancyAsync(item1);
            var isAdded2 = await service.AddVacancyAsync(item2);

            _ = vacatureController.Received().AddAsync(Arg.Is<Vacature>(v => v.Id == item1.Id));
            _ = vacatureController.Received().AddAsync(Arg.Is<Vacature>(v => v.Id == item2.Id));
        }

        [TestMethod]
        public async Task ShouldFetchVacanciesInAlphabeticalOrder()
        {
            AddExistingVacancyToMockDatabase(CreateVacancy(11, "Bob    "));
            AddExistingVacancyToMockDatabase(CreateVacancy(12, "Alice  "));
            AddExistingVacancyToMockDatabase(CreateVacancy(13, "Zaphod "));
            AddExistingVacancyToMockDatabase(CreateVacancy(14, "Charley"));

            var results = await service.FetchAllVacanciesAsync();

            Assert.AreEqual("Alice  ", results[0].Title);
            Assert.AreEqual("Bob    ", results[1].Title);
            Assert.AreEqual("Charley", results[2].Title);
            Assert.AreEqual("Zaphod ", results[3].Title);
        }

        private Vacancy CreateVacancy(int id, string title)
        {
            return new Vacancy
            {
                Title = title,
                Id = id,
                Description = "A description"
            };
        }

        private void AddExistingVacancyToMockDatabase(Vacancy vacancy)
        {
            vacatureController.GetByIdAsync(vacancy.Id).Returns(VacancyConverter.ToBo(vacancy));

            storedVacatures.Add(VacancyConverter.ToBo(vacancy));
            vacatureController.FetchAllAsync().Returns(storedVacatures);
        }
    }
}