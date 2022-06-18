using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Acme.CarRentalService.DAL.MessageHandlers;
using Acme.CarRentalService.MessagingServices;
using CarRentalServices.Specs.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.RentalStatistic
{
    [Binding]
    public sealed class MostPopularCar
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public MostPopularCar(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            #region Mock setup
            var carBrowseRepoMock = new Mock<IRepository<Car>>();
            Setup(carBrowseRepoMock);

            var carReservationMock = new Mock<IRepository<CarReservation>>();
            var rentalStatsRepoMock = new RentalStatisticsRepository();
            #endregion

            #region Mediator and handlers
            var channel = new SalesChannel();
            var availabilityHandler = new CarAvailabilityHandler(carBrowseRepoMock.Object, channel);
            var reservationHandler = new CarReservationCommandHandler(carReservationMock.Object, channel);
            var reservationStatisticsHandler = new CarReservationStatisticsHandler(rentalStatsRepoMock, carBrowseRepoMock.Object, channel);
            channel.Register(availabilityHandler);
            channel.Register(reservationHandler);
            channel.Register(reservationStatisticsHandler);
            #endregion

            #region Controllers
            var rentalController = new CarRentalController(carReservationMock.Object, channel);
            #endregion

            _scenarioContext.Add("reservationMock", carReservationMock);
            _scenarioContext.Add("statsRepoMock", rentalStatsRepoMock);
            _scenarioContext.Add("messageChannel", channel);
            _scenarioContext.Add("carRentalController", rentalController);
        }

        [Given(@"rental history")]
        public void GivenRentalHistory(Table table)
        {
            var rentalController = _scenarioContext.Get<CarRentalController>("carRentalController");

            var rentalHistory = table.CreateSet<CarReservation>();
            foreach (var reservation in rentalHistory)
            {
                rentalController.Post(reservation).Wait();
            }
        }

        [When(@"displaying most popular cars")]
        public void WhenDisplayingMostPopularCars()
        {
            var channel = _scenarioContext.Get<SalesChannel>("messageChannel");
            var statsRepo = _scenarioContext.Get<RentalStatisticsRepository>("statsRepoMock");
            var statsController = new RentalStatisticsController(statsRepo, channel);

            var results = statsController.Get().Result;

            _scenarioContext.Add("results", results);
        }

        [Then(@"the list should be weighted towards brands most rented")]
        public void ThenTheListShouldBeWeightedTowardsBrandsMostRented()
        {
            var results = _scenarioContext.Get<IEnumerable<CarRentalStatistics>>("results");

            CarRentalStatistics prevResult = null;
            for (int i = 0; i < results.Count(); i++)
            {
                if(i == 0)
                {
                    prevResult = results.ElementAt(i);
                    
                    continue;
                }

                Assert.IsTrue(prevResult.NumberOfRentals >= results.ElementAt(i).NumberOfRentals);
            }
        }

        private void Setup(Mock<IRepository<Car>> carBrowseRepoMock)
        {
            carBrowseRepoMock.Setup(m => m.GetByID("4")).Returns(Task.FromResult(
                new Car
                {
                    Id = "4",
                    Brand = "Toyota",
                    Model = "Auris",
                    ImageUrl = "https://localhost/",
                    IsAvailable = true,
                    Location = "Tokyo",
                    PricePerDay = 5
                }
                ));

            carBrowseRepoMock.Setup(m => m.GetByID("1")).Returns(Task.FromResult(
                new Car
                {
                    Id = "1",
                    Brand = "Audi",
                    Model = "A1",
                    ImageUrl = "https://localhost/",
                    IsAvailable = true,
                    Location = "Berlin",
                    PricePerDay = 4
                }
                ));

            carBrowseRepoMock.Setup(m => m.GetByID("2")).Returns(Task.FromResult(
                new Car
                {
                    Id = "2",
                    Brand = "Nissan",
                    Model = "X-Trail",
                    ImageUrl = "https://localhost/",
                    IsAvailable = true,
                    Location = "Kobe",
                    PricePerDay = 10
                }
                ));
        }

    }
}
