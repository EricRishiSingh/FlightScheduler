using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transportly.FlightScheduler
{
    /// <summary>
    /// Class to manage the flight schedule
    /// </summary>
    public class FlightScheduler
    {
        /// <summary>
        /// Gets the loaded flight schedule.
        /// </summary>
        public List<Flight> Flights { get; private set; }

        /// <summary>
        /// This can be unit tested.
        /// </summary>
        /// <param name="scheduleJson">The flight schedule in json format</param>
        public void LoadSchedule(string scheduleJson)
        {
            // Read the schedule from the json file and convert to in-memory flight schedule
            var flightSchedule = JArray.Parse(scheduleJson);
            Flights = flightSchedule.ToObject<List<Flight>>();
        }

        /// <summary>
        /// Print the flight schedule.
        /// </summary>
        public void PrintSchedule()
        {
            for (int i = 0; i < Flights.Count; i++)
            {
                Console.WriteLine($"Flight: {i + 1}, departure: {Flights[i].Departure.Code}, arrival: {Flights[i].Destination.Code}, day: {Flights[i].Day}");
            }
        }

        public void LoadOrderBatch(OrderBatch orderBatch)
        {
            foreach (var order in orderBatch.Orders)
            {
                var flight = Flights.FirstOrDefault(i => i.Destination.Code == order.Destination.Code && i.Orders.Count <= 20);
                if (flight != null)
                {
                    flight.LoadOrder(order);
                }
            }
        }
    }

    /// <summary>
    /// The flight schedule information.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Gets or sets the day of the flight starting at 1.
        /// </summary>
        public int Day { get; set; }

        public int FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the departure information of the flight.
        /// </summary>
        public Airport Departure { get; set; }

        /// <summary>
        /// Gets or sets the destination information of the flight
        /// </summary>
        public Airport Destination { get; set; }

        public List<Order> Orders { get; private set; } = new List<Order>();

        public void LoadOrder(Order order)
        {
            Orders.Add(order);
            order.ScheduledDeliveryFlight = this;
        }
    }

    /// <summary>
    /// The airport information for the flight.
    /// </summary>
    public class Airport
    {
        /// <summary>
        /// Gets or sets the aiport code for departure or destination.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AirportCode Code { get; set; }

        /// <summary>
        /// Gets or sets the friendly name for the city.
        /// </summary>
        public string City { get; set; }
    }

    public enum AirportCode
    {
        YUL,
        YYZ,
        YYC,
        YVR,
        YYE
    }
}
