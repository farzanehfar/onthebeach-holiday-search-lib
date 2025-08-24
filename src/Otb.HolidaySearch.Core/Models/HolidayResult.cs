namespace Otb.HolidaySearch.Core.Models
{
    /// <summary>
    /// Combined best-value option: one flight + one hotel and the total price
    /// </summary>
    public class HolidayResult
    {
        /// <summary>The chosen flight that matches the search.</summary>
        public Flight Flight { get; set; } = new();
        /// <summary>The chosen hotel that matches the search.</summary>
        public Hotel Hotel { get; set; } = new();
        /// <summary>Total cost (flight + hotel nights).</summary>
        public decimal TotalPrice { get; set; }
    }
}
