using Acme.CarRentalService.Core;
using Acme.CarRentalService.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.CarRentalService.CarRentalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarWishListController : ControllerBase
    {
        private readonly IRepository<WishList> _repo;
        public CarWishListController(IRepository<WishList> repository)
        {
            _repo = repository;
        }
        [HttpPost]
        public async Task Post(WishList wishList)
        {
            await _repo.Insert(wishList);
        }
    }
}
