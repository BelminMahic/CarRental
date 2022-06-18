using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.ReservationSteps
{
    [Binding]
    public sealed class DisplayingPersonalReservations
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public DisplayingPersonalReservations(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the customer is currently renting")]
        public void GivenTheCustomerIsCurrentlyRenting(Table table)
        {
            //Arrange
            var userReservation = table.CreateSet<CarReservation>();
            _scenarioContext.Add("userReservation", userReservation);
        }

        [When(@"the customer requests overview of current reservations")]
        public void WhenTheCustomerRequestsOverviewOfCurrentReservations()
        {
            //Arrange
            var userId = _scenarioContext.Get<string>("userId");
            var userReservations = _scenarioContext.Get<IEnumerable<CarReservation>>("userReservation");

            var reservationsRepoMock = new Mock<IRepository<CarReservation>>();
            reservationsRepoMock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(userReservations));

            var controller = new CarRentalController(reservationsRepoMock.Object, null);
            var currentReservations = controller.GetCurrent();

            _scenarioContext.Add("currentReservations", currentReservations);
        }

        [Then(@"the customer should see a list of current reservations")]
        public void ThenTheCustomerShouldSeeAListOfCurrentReservations()
        {
            // Assert
            var expectedResults = _scenarioContext.Get<IEnumerable<CarReservation>>("userReservation");

            var actualResults = _scenarioContext.Get<Task<IEnumerable<CarReservation>>>("currentReservations");

            Assert.AreEqual(expectedResults.Count(), actualResults.Result.Count());

            foreach (var car in actualResults.Result)
            {
                var match = expectedResults.SingleOrDefault(c =>
                c.CarId == car.CarId &&
                c.CustomerId == car.CustomerId &&
                c.Id == car.Id &&
                c.RentFrom == car.RentFrom &&
                c.RentTo == car.RentTo);

                Assert.IsNotNull(match);
            }

        }

    }
}
