using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Transportly.FlightScheduler
{
    /// <summary>
    /// Class to manage the flight schedule
    /// </summary>
    public class FlightScheduler
    {
        /// <summary>
        /// Gets the loaded flight schedule
        /// </summary>
        public List<Flight> Flights { get; private set; }

        /// <summary>
        /// Loads the flight schedule from the json string
        /// This can easily be unit tested
        /// </summary>
        /// <param name="scheduleJson">The flight schedule in json format</param>
        public void LoadScheduleFromJson(string scheduleJson)
        {
            // Read the schedule from the json file and convert to in-memory flight schedule
            Flights = JArray.Parse(scheduleJson).ToObject<List<Flight>>();
        }

        /// <summary>
        /// Print the flight schedule
        /// </summary>
        public void PrintSchedule()
        {
            foreach (var flight in Flights)
            {
                Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.Departure.Code}, arrival: {flight.Destination.Code}, day: {flight.Day}");
            }
        }
    }

    /// <summary>
    /// The flight schedule information
    /// </summary>
    public class Flight
    {
        private readonly List<Order> orders = new List<Order>();

        /// <summary>
        /// Gets or sets the day of the flight starting at 1
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the flight number for this flight
        /// </summary>
        public int FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the departure information of the flight
        /// </summary>
        public Airport Departure { get; set; }

        /// <summary>
        /// Gets or sets the destination information of the flight
        /// </summary>
        public Airport Destination { get; set; }

        /// <summary>
        /// Gets a value indicating whether gets whether this flight is full
        /// </summary>
        public bool IsFlightFull => orders.Count >= 20;

        /// <summary>
        /// Load the order onto the flight
        /// </summary>
        /// <param name="order">The order</param>
        public void LoadOrder(Order order)
        {
            orders.Add(order);
        }
    }

    /// <summary>
    /// The airport information for the flight
    /// </summary>
    public class Airport
    {
        /// <summary>
        /// Gets or sets the aiport code for departure or destination
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AirportCode Code { get; set; }

        /// <summary>
        /// Gets or sets the friendly name for the city
        /// </summary>
        public string City { get; set; }
    }

    /// <summary>
    /// Enum for the airport codes
    /// </summary>
    public enum AirportCode
    {
        YUL,
        YYZ,
        YYC,
        YVR,
        YYE
    }
}
