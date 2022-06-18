using Acme.CarRentalService.Core;
using System;
using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL.MessageHandlers
{
    public class CarReservationCommandHandler : IConsumeMessage
    {
        private readonly IRepository<CarReservation> _repo;
        private readonly MessageChannel _channel;

        public CarReservationCommandHandler(IRepository<CarReservation> repo, MessageChannel channel)
        {
            _repo = repo;
            _channel = channel;
        }

        public bool CanConsume(Message message)
        {
            return message.Type.Equals("ReserveCar");
        }

        public async Task Consume(Message message)
        {
            var carId = (string) message.Parameters["carId"].Item1;
            var customerId = (string)message.Parameters["customerId"].Item1;
            var rentFrom = (DateTime)message.Parameters["rentFrom"].Item1;
            var rentTo = (DateTime)message.Parameters["rentTo"].Item1;

            var reservation = new CarReservation
            {
                CarId = carId,
                CustomerId = customerId,
                RentFrom = rentFrom,
                RentTo = rentTo
            };

            await _repo.Insert(reservation);

            var @event = new Message("CarReserved");
            @event.AddParameter("carId", new Tuple<object, Type>(carId, typeof(string)));
            
            await _channel.Broadcast(@event);
        }
    }
}
