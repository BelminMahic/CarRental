using System;
using System.Text.Json.Serialization;

namespace Acme.CarRentalService.Core
{
    public class CarReservation
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("carId")]
        public string CarId { get; set; }

        [JsonPropertyName("rentFrom")]
        public DateTime RentFrom { get; set; }

        [JsonPropertyName("rentTo")]
        public DateTime RentTo { get; set; }
    }
}
