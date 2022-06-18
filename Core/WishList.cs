using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Acme.CarRentalService.Core
{
    public class WishList
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }
        [JsonPropertyName("carId")]
        public string CarId { get; set; }
    }
}
