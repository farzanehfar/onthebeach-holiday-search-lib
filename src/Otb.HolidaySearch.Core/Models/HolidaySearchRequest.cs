namespace Otb.HolidaySearch.Core.Models
{
    /// <summary>
    /// Input the user provides to search for a holiday
    /// </summary>
    public class HolidaySearchRequest
    {
        /// <summary>Departing airport code, or special values: "Any London Airport" or "Any Airport".</summary>
        public string DepartingFrom { get; set; } = string.Empty;
        /// <summary>Destination airport code (IATA), e.g. AGP/PMI/LPA.</summary>
        public string TravelingTo { get; set; } = string.Empty;
        /// <summary>Desired departure date, must match flight.DepartureDate and hotel.ArrivalDate.</summary>
        public DateOnly DepartureDate { get; set; }
        /// <summary>Trip duration in nights.</summary>
        public int Duration { get; set; }
    }
}
