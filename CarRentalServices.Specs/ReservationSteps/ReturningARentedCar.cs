using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Acme.CarRentalService.DAL.MessageHandlers;
using Acme.CarRentalService.MessagingServices;
using Moq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs.ReservationSteps
{
    [Binding]
    public class ReturningARentedCar
    {
        private readonly ScenarioContext _scenarioContext;
        public ReturningARentedCar(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var carBrowseMock = new Mock<IRepository<Car>>();
            var carReservationMock = new Mock<IRepository<CarReservation>>();

            var channel = new SalesChannel();
            var availabilityHandler = new CarAvailabilityHandler(carBrowseMock.Object, channel);
            var reservationHandler = new CarReservationCommandHandler(carReservationMock.Object, channel);
            var carReturnHandler = new CarReturnCommandHandler(carBrowseMock.Object, channel);

            channel.Register(availabilityHandler);
            channel.Register(reservationHandler);
            channel.Register(carReturnHandler);

            _scenarioContext.Add("browseRepoMock", carBrowseMock);
            _scenarioContext.Add("reservationMock", carReservationMock);
            _scenarioContext.Add("messageChannel", channel);
        }
        
        [Given(@"a rented car")]
        public void GivenARentedCar(Table table)
        {
            var rentedCar = table.CreateInstance<Car>();
            _scenarioContext.Add("rentedCar", rentedCar);
        }
        
        [When(@"the car is returned")]
        public void WhenTheCarIsReturned()
        {
            var rentedCar = _scenarioContext.Get<Car>("rentedCar");

            var browsingRepoMock = _scenarioContext.Get<Mock<IRepository<Car>>>("browseRepoMock");
            browsingRepoMock.Setup(m => m.GetByID(rentedCar.Id))
                .Returns(Task.FromResult(rentedCar));

            var rentalRepoMock = _scenarioContext.Get<Mock<IRepository<CarReservation>>>("reservationMock");
            var messageChannel = _scenarioContext.Get<SalesChannel>("messageChannel");

            var controller = new CarRentalController(rentalRepoMock.Object, messageChannel);
            controller.ReturnRented(rentedCar.Id).Wait();

            _scenarioContext.Add("browsingMock", browsingRepoMock);
        }
        
        [Then(@"the car should be available for renting again")]
        public void ThenTheCarShouldBeAvailableForRentingAgain()
        {
            var browsingRepoMock = _scenarioContext.Get<Mock<IRepository<Car>>>("browsingMock");
            var rentedCar = _scenarioContext.Get<Car>("rentedCar");

            browsingRepoMock.Verify(m => m.Update(It.Is<Car>(c => c.Id == rentedCar.Id
                && c.IsAvailable == true)));
        }
    }
}
