using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CarRentalServices.Specs.CarBrowsingSteps
{
    [Binding]
    public sealed class BrowseByLocation
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public BrowseByLocation(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"the customer requests available cars in '(.*)'")]
        public void WhenTheCustomerRequestsAvailableCarsIn(string p0)
        {
            // Arrange
            var userId = _scenarioContext.Get<string>("userId");
            var cars = _scenarioContext.Get<IEnumerable<Car>>("availableCars");
            var repoMock = new Mock<IReadOnlyRepository<Car>>();
            repoMock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(cars));

            // Act
            var controller = new CarBrowsingController(repoMock.Object);
            var results = controller.FilterByLocation(p0);

            _scenarioContext.Add("results", results.Result);
        }
    }
}
