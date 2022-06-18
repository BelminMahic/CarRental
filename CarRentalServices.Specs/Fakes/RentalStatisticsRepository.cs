using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalServices.Specs.Fakes
{
    class RentalStatisticsRepository : IRepository<CarRentalStatistics>
    {
        private readonly List<CarRentalStatistics> _db;

        public RentalStatisticsRepository()
        {
            _db = new List<CarRentalStatistics>();
        }

        public Task Delete(CarRentalStatistics entityToDelete)
        {
            _db.Remove(entityToDelete);

            return Task.CompletedTask;
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarRentalStatistics>> GetAll()
        {
            return Task.FromResult(_db.AsEnumerable());
        }

        public Task<CarRentalStatistics> GetByID(object id)
        {
            return Task.FromResult(_db.SingleOrDefault(s => s.CarId == id.ToString()));
        }

        public Task Insert(CarRentalStatistics entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            _db.Add(entity);

            return Task.CompletedTask;
        }

        public Task Update(CarRentalStatistics entityToUpdate)
        {
            var match = _db.SingleOrDefault(s => s.Id == entityToUpdate.Id);

            if (match != null)
            {
                match.Brand = entityToUpdate.Brand;
                match.CarId = entityToUpdate.CarId;
                match.Model = entityToUpdate.Model;
                match.NumberOfRentals = entityToUpdate.NumberOfRentals;
            }

            return Task.CompletedTask;
        }
    }
}
