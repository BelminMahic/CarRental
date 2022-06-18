using System.Text.Json.Serialization;

namespace Acme.CarRentalService.Core
{
    public class CarRentalStatistics
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("carId")]
        public string CarId { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("numberOfRentals")]
        public int NumberOfRentals { get; set; }


    }
}
