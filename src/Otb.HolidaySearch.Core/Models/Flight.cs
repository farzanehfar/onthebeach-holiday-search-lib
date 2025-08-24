using System.Text.Json.Serialization;

namespace Otb.HolidaySearch.Core.Models
{
    /// <summary>
    /// Flight record as appears in flights.json
    /// </summary>
    public class Flight
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("airline")]
        public string Airline { get; set; } = string.Empty;

        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;

        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        // Map snake_case "departure_date" to C# DateOnly property
        [JsonPropertyName("departure_date")]
        public DateOnly DepartureDate { get; set; }
    }
}
