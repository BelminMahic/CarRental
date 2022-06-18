using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acme.CarRentalService.CarRentalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrowsingController : ControllerBase
    {
        private readonly IReadOnlyRepository<Car> _carRepo;

        public CarBrowsingController(IReadOnlyRepository<Car> carRepository)
        {
            _carRepo = carRepository;
        }
        // GET: api/<CarBrowsingController>
        [HttpGet]
        public  async Task<IEnumerable<Car>> Get()
        {
            Task<IEnumerable<Car>> cars = _carRepo.GetAll();
            var availableCars = new List<Car>();
            foreach(var item in cars.Result)
            {
                if (item.IsAvailable)
                {
                    availableCars.Add(item);
                }
            }
            return  await Task.FromResult(availableCars);
        }
        [HttpGet]
        public async Task<IEnumerable<Car>> FilterByLocation(string location)
        {
            Task<IEnumerable<Car>> cars = _carRepo.GetAll();
            var availableCars = new List<Car>();
            foreach (var item in cars.Result)
            {
                if (item.Location == location)

                {
                    availableCars.Add(item);
                }
            }
            return await Task.FromResult(availableCars);
        }
        [HttpGet]
        public async Task<IEnumerable<Car>> FilterByPriceRange(decimal price)
        {
            Task<IEnumerable<Car>> cars = _carRepo.GetAll();
            var availableCars = new List<Car>();
            foreach (var item in cars.Result)
            {
                if (item.PricePerDay <= price)
                {
                    availableCars.Add(item);
                }
            }
            return await Task.FromResult(availableCars);
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> GetUnavailable()
        {
            Task<IEnumerable<Car>> cars = _carRepo.GetAll();
            var unavailableCars = new List<Car>();
            foreach (var item in cars.Result)
            {
                if (!item.IsAvailable)
                {
                    unavailableCars.Add(item);
                }
            }
            return await Task.FromResult(unavailableCars);
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> PriceIncreasing(DateTime date)
        {
            Task<IEnumerable<Car>> cars = _carRepo.GetAll();
            var carsWithNewPrice = new List<Car>();

            string dateToday = date.ToString("d");
            DayOfWeek day = date.DayOfWeek;

            foreach (var c in cars.Result)
            {
                if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday) || (day == DayOfWeek.Friday))
                {
                    c.PricePerDay *= (decimal)1.15;
                    carsWithNewPrice.Add(c);
                }
            }
            return await Task.FromResult(carsWithNewPrice);
        }
    }
}
