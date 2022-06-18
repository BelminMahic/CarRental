using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.CarBrowsingSteps
{
    [Binding]
    public sealed class DisplayingAvailableCars
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public DisplayingAvailableCars(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a logged in customer '(.*)'")]
        public void GivenALoggedInCustomer(string userId)
        {
            // Arrange
            _scenarioContext.Add("userId", userId);
        }

        [Given(@"a list of available cars")]
        public void GivenAListOfAvailableCars(Table table)
        {
            // Arrange
            var cars = table.CreateSet<Car>();

            _scenarioContext.Add("availableCars", cars);
        }

        [When(@"the customer requests available cars")]
        public void WhenTheCustomerRequestsAvailableCars()
        {
            // Arrange
            var userId = _scenarioContext.Get<string>("userId"); 
            var cars = _scenarioContext.Get<IEnumerable<Car>>("availableCars");
            var repoMock = new Mock<IReadOnlyRepository<Car>>();
            repoMock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(cars));

            // Act
            var controller = new CarBrowsingController(repoMock.Object);
            var results = controller.Get();

            _scenarioContext.Add("results", results.Result);
        }

        [Then(@"the result should be")]
        public void ThenTheResultShouldBe(Table table)
        {
            // Assert
            var expectedResults = table.CreateSet<Car>();
            var actualResults = _scenarioContext.Get<IEnumerable<Car>>("results");

            Assert.AreEqual(expectedResults.Count(), actualResults.Count());

            foreach (var car in actualResults)
            {
                var match = expectedResults.SingleOrDefault(c =>
                c.Id == car.Id &&
                c.Location == car.Location &&
                c.Model == car.Model);

                Assert.IsNotNull(match);
            }
        }


    }
}
