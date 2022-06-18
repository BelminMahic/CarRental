using Acme.CarRentalService.CarRentalServices.Controllers;
using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CarRentalServices.Specs
{
    [Binding]
    public class CarWishListSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CarWishListSteps(ScenarioContext context)
        {
            _scenarioContext = context;
        }

        [Given(@"a user '(.*)' is logged in")]
        public void GivenAUserIsLoggedIn(string userId)
        {
            _scenarioContext.Add("userId", userId);
        }
        
        [Given(@"car is selected")]
        public void GivenCarIsSelected(Table table)
        {
            var car = table.CreateInstance<Car>();

            _scenarioContext.Add("selectedCar", car);
        }
        
        [When(@"the customer adds car '(.*)' to wish list")]
        public void WhenTheCustomerAddsCarToWishList(string carId)
        {
            var userId = _scenarioContext.Get<string>("userId");

            var wishlist = new WishList
            {
                Id="1",
                CustomerId=userId,
                CarId=carId
            };

            var wishListMock = new Mock<IRepository<WishList>>();
            WishList wishMockInsert = null;
            wishListMock.Setup(x => x.Insert(It.IsAny<WishList>()))
                    .Callback<WishList>(wishlist => wishMockInsert = wishlist);

            var controller = new CarWishListController(wishListMock.Object);
            controller.Post(wishlist).Wait();

            _scenarioContext.Add("wishlistedCar", wishMockInsert);
            _scenarioContext.Add("carId", carId);
        }
        
        [Then(@"a selected car is added to wish list")]
        public void ThenASelectedCarIsAddedToWishList()
        {
            var carWish = _scenarioContext.Get<string>("carId");
            var actualWishlist = _scenarioContext.Get<WishList>("wishlistedCar");

            Assert.AreEqual(carWish, actualWishlist.CarId);
        }
    }
}
