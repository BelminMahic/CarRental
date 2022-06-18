using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Acme.CarRentalService.MessagingServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.CarRentalService.CarRentalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly IRepository<CarReservation> _reserverationRepo;
        private readonly SalesChannel _salesChannel;

        public CarRentalController(IRepository<CarReservation> reservationsRepository,
            SalesChannel channel)

        {
            _reserverationRepo = reservationsRepository;

            _salesChannel = channel;
        }

        // GET: api/<CarRentalController>
        [HttpGet]
        public async Task<IEnumerable<CarReservation>> Get()
        {
            Task<IEnumerable<CarReservation>> cars = _reserverationRepo.GetAll();
            
            return await cars;
        }

        public async Task<IEnumerable<CarReservation>> GetCurrent()
        {
            Task<IEnumerable<CarReservation>> carReservations = _reserverationRepo.GetAll();
            var currentReservations = new List<CarReservation>();
            foreach (var r in carReservations.Result)
            {
                if (r.RentFrom <= DateTime.Now && r.RentTo >= DateTime.Now)
                {
                    currentReservations.Add(r);
                }
            }
            return await Task.FromResult(currentReservations);
        }

        [HttpPut]
        public async Task ReturnRented(string carId)
        {
            var command = new Message("ReturnCar");
            command.AddParameter("carId", new Tuple<object, Type>(carId, typeof(string)));

            await _salesChannel.Broadcast(command);
        }
       

        // POST api/<CarRentalControllers>
        [HttpPost]
        public async Task Post(CarReservation carReservation)
        {
            var command = new Message("ReserveCar");
            command.AddParameter("carId", new Tuple<object, Type>(carReservation.CarId, typeof(string)));
            command.AddParameter("customerId", new Tuple<object, Type>(carReservation.CustomerId, typeof(string)));
            command.AddParameter("rentFrom", new Tuple<object, Type>(carReservation.RentFrom, typeof(DateTime)));
            command.AddParameter("rentTo", new Tuple<object, Type>(carReservation.RentTo, typeof(DateTime)));

            await _salesChannel.Broadcast(command);
        }
    }
}
