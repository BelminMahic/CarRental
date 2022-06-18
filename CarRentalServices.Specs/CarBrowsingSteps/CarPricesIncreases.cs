using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.CarBrowsingSteps
{
    [Binding]
    public class CarBrowsingSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CarBrowsingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"a list of all cars")]
        public void GivenAListOfAllCars(Table table)
        {
            var allCars = table.CreateSet<Car>();
            _scenarioContext.Add("allCars", allCars);
        }

        [When(@"today '(.*)' is friday, saturday or sunday")]
        public void WhenTodayIsSaturdayOrSunday(DateTime date)
        {
            DateTime givenDate = date;
            _scenarioContext.Add("givenDate", givenDate);
        }
        [Then(@"the prices for all cars are increased for 15%")]
        public void ThenThePricesForAllCarsAreIncreasedFor(Table table)
        {
            DateTime givenDate = _scenarioContext.Get<DateTime>("givenDate");
            var expectedResults = table.CreateSet<Car>();
            var cars = _scenarioContext.Get<IEnumerable<Car>>("allCars");
            var repoMock = new Mock<IRepository<Car>>();
            repoMock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(cars));
            var controller = new CarBrowsingController(repoMock.Object);
            var carsWithNewPrice = controller.PriceIncreasing(givenDate);
            _scenarioContext.Add("carsWithNewPrice", carsWithNewPrice);
            var actualResults = _scenarioContext.Get<Task<IEnumerable<Car>>>("carsWithNewPrice");
            Assert.AreEqual(expectedResults.Count(), actualResults.Result.Count());
            foreach (var car in actualResults.Result)
            {
                var match = cars.SingleOrDefault(c =>
                c.Id == car.Id &&
                c.PricePerDay == car.PricePerDay);
                Assert.IsNotNull(match);
            }
        }
    }
}
