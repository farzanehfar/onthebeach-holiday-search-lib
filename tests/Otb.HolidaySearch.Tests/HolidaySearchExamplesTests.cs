using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Otb.HolidaySearch.Core.Data;
using Otb.HolidaySearch.Core.Models;
using Otb.HolidaySearch.Core.Search;
using Xunit;


namespace Otb.HolidaySearch.Tests
{
    /// <summary>
    /// Integration-style tests that load the JSON files and run real searches.
    /// </summary>
    public class HolidaySearchExamplesTests
    {
        private readonly IReadOnlyList<Flight> _flights;
        private readonly IReadOnlyList<Hotel> _hotels;
        private readonly HolidaySearchEngine _engine = new();

        public HolidaySearchExamplesTests()
        {
            // NOTE: test project copies data/ files to output using <CopyToOutputDirectory>.
            var baseDir = AppContext.BaseDirectory;
            var flights = Path.Combine(baseDir, "data", "flights.json");
            var hotels  = Path.Combine(baseDir, "data", "hotels.json");

            var store = new JsonDataStore(flights, hotels);
            _flights = store.LoadFlights();
            _hotels  = store.LoadHotels();
        }

        [Fact]
        public void Customer1_Manchester_to_Malaga_2023_07_01_7nights()
        {
            var req = new HolidaySearchRequest
            {
                DepartingFrom = "MAN",
                TravelingTo = "AGP",
                DepartureDate = new DateOnly(2023, 7, 1),
                Duration = 7
            };

            var best = _engine.Search(req, _flights, _hotels);
            Assert.NotNull(best);
            Assert.Equal(2, best!.Flight.Id); // Expected Flight 2
            Assert.Equal(9, best.Hotel.Id);   // Expected Hotel 9
        }

        [Fact]
        public void Customer2_AnyLondon_to_Mallorca_2023_06_15_10nights()
        {
            var req = new HolidaySearchRequest
            {
                DepartingFrom = "Any London Airport",
                TravelingTo = "PMI",
                DepartureDate = new DateOnly(2023, 6, 15),
                Duration = 10
            };

            var best = _engine.Search(req, _flights, _hotels);
            Assert.NotNull(best);
            Assert.Equal(6, best!.Flight.Id); // Expected Flight 6
            Assert.Equal(5, best.Hotel.Id);   // Expected Hotel 5
        }

        [Fact]
        public void Customer3_Any_to_GranCanaria_2022_11_10_14nights()
        {
            var req = new HolidaySearchRequest
            {
                DepartingFrom = "Any Airport",
                TravelingTo = "LPA",
                DepartureDate = new DateOnly(2022, 11, 10),
                Duration = 14
            };

            var best = _engine.Search(req, _flights, _hotels);
            Assert.NotNull(best);
            Assert.Equal(7, best!.Flight.Id); // Expected Flight 7
            Assert.Equal(6, best.Hotel.Id);   // Expected Hotel 6
        }

        [Fact]
        public void NoMatch_ReturnsNull()
        {
            var req = new HolidaySearchRequest
            {
                DepartingFrom = "MAN",
                TravelingTo = "PMI",
                DepartureDate = new DateOnly(2025, 1, 1),
                Duration = 5
            };

            var best = _engine.Search(req, _flights, _hotels);
            Assert.Null(best);
        }
    }
}
