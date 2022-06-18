using Acme.CarRentalService.Core;
using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL.MessageHandlers
{
    public class CarReservationStatisticsHandler : IConsumeMessage
    {
        private readonly IRepository<CarRentalStatistics> _statsRepo;
        private readonly MessageChannel _channel;
        private readonly IReadOnlyRepository<Car> _carRepo;

        public CarReservationStatisticsHandler(IRepository<CarRentalStatistics> statsRepo,
            IReadOnlyRepository<Car> carRepo, MessageChannel channel)
        {
            _statsRepo = statsRepo;

            _channel = channel;

            _carRepo = carRepo;
        }

        public bool CanConsume(Message message)
        {
            return message.Type.Equals("CarReserved");
        }

        public async Task Consume(Message message)
        {
            var carId = (string)message.Parameters["carId"].Item1;
            var car = await _carRepo.GetByID(carId);

            var statistics = await _statsRepo.GetByID(carId) ?? CreateRecord(carId, car);
            statistics.NumberOfRentals += 1;

            if (ItsANewRecord(statistics))
            {
                await _statsRepo.Insert(statistics);
            }
            else
            {
                await _statsRepo.Update(statistics);
            }

            var @event = new Message("CarRentalStatisticsUpdated");
            @event.AddParameter("statistics", new System.Tuple<object, System.Type>(statistics, typeof(CarRentalStatistics)));

            await _channel.Broadcast(@event);
        }

        private static bool ItsANewRecord(CarRentalStatistics statistics)
        {
            return string.IsNullOrEmpty(statistics.Id);
        }

        private CarRentalStatistics CreateRecord(string carId, Car car)
        {
            return new CarRentalStatistics
            {
                Brand = car.Brand,
                CarId = carId,
                Model = car.Model,
                NumberOfRentals = 0
            };
        }
    }
}
