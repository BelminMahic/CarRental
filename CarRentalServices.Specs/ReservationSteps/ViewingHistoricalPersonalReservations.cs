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
    public sealed class ViewingHistoricalPersonalReservations
    {
        private readonly ScenarioContext _scenarioContext;
        public ViewingHistoricalPersonalReservations(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"the customer has previously rented")]
        public void GivenTheCustomerHasPreviouslyRented(Table table)
        {
            var allUserReservations = table.CreateSet<CarReservation>();
            _scenarioContext.Add("allUserReservations", allUserReservations);
        }
        [When(@"the customer requests reservations history")]
        public void WhenTheCustomerRequestsReservationsHistory()
        {
            var reservationHistory = _scenarioContext.Get<IEnumerable<CarReservation>>("allUserReservations");
            var repoMock = new Mock<IRepository<CarReservation>>();
            repoMock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(reservationHistory));
            var controller = new CarRentalController(repoMock.Object, null);
            var results = controller.Get();
            _scenarioContext.Add("results", results);
        }
        [Then(@"the customer should see correct historical transactions")]
        public void ThenTheCustomerShouldSeeHistoricalTransactions(Table table)
        {
            var expectedResults = table.CreateSet<CarReservation>();

            var actualResults = _scenarioContext.Get<Task<IEnumerable<CarReservation>>>("results");
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