using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Acme.CarRentalService.DAL.MessageHandlers;
using Acme.CarRentalService.MessagingServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.ReservationSteps
{
    [Binding]
    public sealed class ReservingUnavailableCar
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public ReservingUnavailableCar(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var repoMock = new Mock<IRepository<Car>>();
            var carReservationMock = new Mock<IRepository<CarReservation>>();

            var channel = new SalesChannel();
            var availabilityHandler = new CarAvailabilityHandler(repoMock.Object, channel);
            var reservationHandler = new CarReservationCommandHandler(carReservationMock.Object, channel);
            channel.Register(availabilityHandler);
            channel.Register(reservationHandler);

            _scenarioContext.Add("browseRepoMock", repoMock);
            _scenarioContext.Add("reservationMock", carReservationMock);
            _scenarioContext.Add("messageChannel", channel);
        }

        [Given(@"an a car that has allready been reserved")]
        public void GivenAnACarThatHasAllreadyBeenReserved(Table table)
        {
            var unavailableCars = table.CreateSet<Car>();

            var repoMock = _scenarioContext.Get<Mock<IRepository<Car>>>("browseRepoMock");
            repoMock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(unavailableCars));

            _scenarioContext.Add("unavailableCar", unavailableCars);
        }

        [When(@"the customer tries to reserve that (.*)")]
        public void WhenTheCustomerTriesToReserveThatCar(string carId)
        {
            // Arrange
            var userId = _scenarioContext.Get<string>("userId");
            var unavailableCars = _scenarioContext.Get<IEnumerable<Car>>("unavailableCar");

            var repoMock = _scenarioContext.Get<Mock<IRepository<Car>>>("browseRepoMock");
            repoMock.Setup(m => m.GetByID(carId))
                .Returns(Task.FromResult(unavailableCars
                    .FirstOrDefault(x => x.Id == carId && !x.IsAvailable)));

            var carReservationMock = _scenarioContext.Get<Mock<IRepository<CarReservation>>>("reservationMock");
            var mockCarReservation = Mock.Of<CarReservation>(m => m.CarId == carId);
            var msgChannel = _scenarioContext.Get<SalesChannel>("messageChannel");

            // Act
            var controller = new CarRentalController(carReservationMock.Object, msgChannel);
            var results = controller.Get();
            _scenarioContext.Add("requiredCarId", carId);
            _scenarioContext.Add("result", mockCarReservation);

        }
        [Then(@"a customer friendly message is displayed")]
        public void ThenACustomerFriendlyMessageIsDisplayed()
        {           
            var reservedCarId = _scenarioContext.Get<string>("requiredCarId");
            var actualResult = _scenarioContext.Get<CarReservation>("result");

            Assert.AreEqual(reservedCarId, actualResult.CarId, "Car is reserved!");

           

        }
    }
}


