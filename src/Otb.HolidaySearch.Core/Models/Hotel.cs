using System.Text.Json.Serialization;

namespace Otb.HolidaySearch.Core.Models
{
    /// <summary>
    /// Hotel record as appears in hotels.json
    /// </summary>
    public class Hotel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("arrival_date")]
        public DateOnly ArrivalDate { get; set; }

        [JsonPropertyName("price_per_night")]
        public decimal PricePerNight { get; set; }

        [JsonPropertyName("local_airports")]
        public List<string> LocalAirports { get; set; } = new();

        [JsonPropertyName("nights")]
        public int Nights { get; set; }
    }
}
