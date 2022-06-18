using Acme.CarRentalService.Core;
using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL.MessageHandlers
{
    public class CarAvailabilityHandler : IConsumeMessage
    {
        private readonly IRepository<Car> _repo;
        private readonly MessageChannel _channel;

        public CarAvailabilityHandler(IRepository<Car> repo, MessageChannel channel)
        {
            _repo = repo;
            _channel = channel;
        }

        public bool CanConsume(Message message)
        {
            return message.Type.Equals("CarReserved") || message.Type.Equals("CarReturned");
        }

        public async Task Consume(Message message)
        {
            var carId = (string)message.Parameters["carId"].Item1;
            var car = await _repo.GetByID(carId);

            if (message.Type.Equals("CarReserved"))
            {
                car.IsAvailable = false;
            }
            else
            {
                car.IsAvailable = true;
            }

            await _repo.Update(car);
        }
    }
}
