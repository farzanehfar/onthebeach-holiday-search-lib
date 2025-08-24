using Otb.HolidaySearch.Core.Models;

namespace Otb.HolidaySearch.Core.Search
{
    /// <summary>
    /// Core search logic: find all matching flights and hotels, then pick lowest total price.
    /// </summary>
    public class HolidaySearchEngine
    {
        /// <summary>Set of London airports considered as "Any London Airport".</summary>
        private static readonly HashSet<string> LondonAirports = new(StringComparer.OrdinalIgnoreCase)
        { "LTN", "LGW", "LHR", "LCY", "STN" };

        /// <summary>
        /// Runs a search over in-memory lists of flights and hotels.
        /// </summary>
        public HolidayResult? Search(HolidaySearchRequest req, IEnumerable<Flight> flights, IEnumerable<Hotel> hotels)
        {
            // 1) Filter flights by origin (supports special keywords), destination and date
            var matchingFlights = flights.Where(f =>
                OriginMatches(req.DepartingFrom, f.From) &&
                string.Equals(f.To, req.TravelingTo, StringComparison.OrdinalIgnoreCase) &&
                f.DepartureDate == req.DepartureDate
            );

            // 2) Filter hotels by destination airport list, arrival date and nights
            var matchingHotels = hotels.Where(h =>
                h.LocalAirports.Any(a => string.Equals(a, req.TravelingTo, StringComparison.OrdinalIgnoreCase)) &&
                h.ArrivalDate == req.DepartureDate &&
                h.Nights == req.Duration
            );

            // 3) Join every flight with every hotel (Cartesian) and compute total price
            var allCombos = from f in matchingFlights
                            from h in matchingHotels
                            let total = f.Price + (h.PricePerNight * h.Nights)
                            orderby total ascending
                            select new HolidayResult { Flight = f, Hotel = h, TotalPrice = total };

            // 4) Return the cheapest (first) or null if none
            return allCombos.FirstOrDefault();
        }

        /// <summary>
        /// Checks if a flight origin matches the request origin, supporting "Any Airport" and "Any London Airport".
        /// </summary>
        private static bool OriginMatches(string requestedOrigin, string flightFrom)
        {
            if (string.Equals(requestedOrigin, "Any Airport", StringComparison.OrdinalIgnoreCase))
                return true; // any origin is acceptable

            if (string.Equals(requestedOrigin, "Any London Airport", StringComparison.OrdinalIgnoreCase))
                return LondonAirports.Contains(flightFrom);

            return string.Equals(requestedOrigin, flightFrom, StringComparison.OrdinalIgnoreCase);
        }
    }
}
