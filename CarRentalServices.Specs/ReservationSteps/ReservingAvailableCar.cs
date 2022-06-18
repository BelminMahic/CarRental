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
    public sealed class ReservingAvailableCar
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public ReservingAvailableCar(ScenarioContext scenarioContext)
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

        [Given(@"and a available car")]
        public void GivenAndAAvailableCar(Table table)
        {
            var cars = table.CreateSet<Car>();

            _scenarioContext.Add("availableCar", cars);
        }

        [When(@"the customer '(.*)' reserves the car '(.*)' from '(.*)' to '(.*)'")]
        public void WhenTheCustomerReservesTheCarFromTo(string customerName, string carId, 
            DateTime fromDate, DateTime toDate)
        {
            // Arrange
            var userId = _scenarioContext.Get<string>("userId");
            var cars = _scenarioContext.Get<IEnumerable<Car>>("availableCar");
            CarReservation carReservation = new CarReservation
            {
                CarId = carId,
                CustomerId = customerName,
                RentFrom = fromDate,
                RentTo = toDate,
                Id = "1"
            };

            var browsingRepoMock = _scenarioContext.Get<Mock<IRepository<Car>>>("browseRepoMock");
            Car carChangeReq = null;
            browsingRepoMock.Setup(m => m.Update(It.IsAny<Car>()))
                .Callback<Car>(carUpdate => carChangeReq = carUpdate);
            
            browsingRepoMock.Setup(m => m.GetByID(carId))
                .Returns(Task.FromResult(cars.SingleOrDefault(c => c.Id.Equals(carId))));

            var reservationsRepoMock = _scenarioContext.Get<Mock<IRepository<CarReservation>>>("reservationMock");
            CarReservation reservationReq = null;
            reservationsRepoMock.Setup(m => m.Insert(It.IsAny<CarReservation>()))
                .Callback<CarReservation>(reservation => reservationReq = reservation);
            var messageChannel = _scenarioContext.Get<SalesChannel>("messageChannel");

            // Act
            var controller = new CarRentalController(reservationsRepoMock.Object, messageChannel);
            controller.Post(carReservation).Wait();

            _scenarioContext.Add("repoMock", reservationsRepoMock);
            _scenarioContext.Add("customerName", customerName);
            _scenarioContext.Add("reservedCar", reservationReq);
            _scenarioContext.Add("requestedCarReservationId", carId);
            _scenarioContext.Add("carUpdateReq", carChangeReq);
        }

        [Then(@"the car should be successfully reserved")]
        public void ThenTheCarShouldBeSuccessfullyReserved()
        {
            var requestedCarReservationId = _scenarioContext.Get<string>("requestedCarReservationId");
            var actualCarReservation = _scenarioContext.Get<CarReservation>("reservedCar");

            // Assert
            Assert.AreEqual(requestedCarReservationId, actualCarReservation.CarId);
        }


        [Then(@"the car should not be available for rental any more")]
        public void ThenTheCarShouldNotBeAvailableForRentalAnyMore(Table table)
        {
            var car = table.CreateInstance<Car>();
            var actualCarUpdate = _scenarioContext.Get<Car>("carUpdateReq");

            Assert.AreEqual(car.Id, actualCarUpdate.Id);
            Assert.IsFalse(actualCarUpdate.IsAvailable);
        }
    }
}
