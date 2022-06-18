using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Acme.CarRentalService.MessagingServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.CarRentalService.CarRentalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalStatisticsController : ControllerBase
    {
        private readonly IReadOnlyRepository<CarRentalStatistics> _statsRepo;
        private readonly SalesChannel _salesChannel;

        public RentalStatisticsController(IReadOnlyRepository<CarRentalStatistics> statsRepository,
            SalesChannel channel)

        {
            _statsRepo = statsRepository;

            _salesChannel = channel;
        }

        [HttpGet]
        public async Task<IEnumerable<CarRentalStatistics>> Get()
        {
            var stats = await _statsRepo.GetAll();

            return stats.OrderByDescending(s => s.NumberOfRentals).Take(3);
        }
    }

    internal class CarRentalStatisticsComparer : IEqualityComparer<CarRentalStatistics>
    {
        public bool Equals([AllowNull] CarRentalStatistics x, [AllowNull] CarRentalStatistics y)
        {
            return x.CarId.Equals(y.CarId);
        }

        public int GetHashCode([DisallowNull] CarRentalStatistics obj)
        {
            return obj.GetHashCode();
        }
    }
}
