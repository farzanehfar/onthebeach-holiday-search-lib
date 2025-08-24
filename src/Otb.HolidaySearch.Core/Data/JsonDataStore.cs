using System.Text.Json;
using Otb.HolidaySearch.Core.Models;

namespace Otb.HolidaySearch.Core.Data
{
    /// <summary>
    /// Loads flight and hotel data from two JSON files.
    /// </summary>
    public class JsonDataStore
    {
        /// <summary>Path to flights.json file.</summary>
        private readonly string _flightsPath;
        /// <summary>Path to hotels.json file.</summary>
        private readonly string _hotelsPath;

        /// <summary>
        /// Create a data store that reads from the given file paths.
        /// </summary>
        public JsonDataStore(string flightsPath, string hotelsPath)
        {
            _flightsPath = flightsPath;
            _hotelsPath = hotelsPath;
        }

        /// <summary>Reads and parses flights.json into memory.</summary>
        public IReadOnlyList<Flight> LoadFlights()
        {
            var json = File.ReadAllText(_flightsPath);
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var flights = JsonSerializer.Deserialize<List<Flight>>(json, opts) ?? new();
            return flights;
        }

        /// <summary>Reads and parses hotels.json into memory.</summary>
        public IReadOnlyList<Hotel> LoadHotels()
        {
            var json = File.ReadAllText(_hotelsPath);
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var hotels = JsonSerializer.Deserialize<List<Hotel>>(json, opts) ?? new();
            return hotels;
        }
    }
}
