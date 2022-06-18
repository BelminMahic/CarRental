using Acme.CarRentalService.Core;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.CarBrowsingSteps
{
    [Binding]
    public sealed class NotDisplayingUnavailableCars
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public NotDisplayingUnavailableCars(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"these cars are taken")]
        public void GivenTheseCarsAreTaken(Table table)
        {
            var availableCars = _scenarioContext.Get<IEnumerable<Car>>("availableCars");
            var unavailableCars = table.CreateSet<Car>();
            var totalCars = new List<Car>(availableCars);
            totalCars.AddRange(unavailableCars);

            _scenarioContext.Remove("availableCars");
            _scenarioContext.Add("availableCars", totalCars);
        }

    }
}
