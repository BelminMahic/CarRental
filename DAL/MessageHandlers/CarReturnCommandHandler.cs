using Acme.CarRentalService.Core;
using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL.MessageHandlers
{
    public class CarReturnCommandHandler : IConsumeMessage
    {
        private readonly IRepository<Car> _repo;
        private readonly MessageChannel _channel;

        public CarReturnCommandHandler(IRepository<Car> carRepo, MessageChannel channel)
        {
            _repo = carRepo;

            _channel = channel;
        }
        
        public bool CanConsume(Message message)
        {
            return message.Type.Equals("ReturnCar");
        }

        public async Task Consume(Message message)
        {
            var carId = (string) message.Parameters["carId"].Item1;

            var car = await _repo.GetByID(carId);
            car.IsAvailable = true;

            await _repo.Update(car);

            var @event = new Message("CarReturned");
            @event.AddParameter("carId", new System.Tuple<object, System.Type>(carId, typeof(string)));

            await _channel.Broadcast(@event);
        }
    }
}
