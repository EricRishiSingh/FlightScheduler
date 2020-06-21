using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transportly.FlightScheduler
{
    /// <summary>
    /// Handles the processing of orders
    /// </summary>
    public class OrderBatch
    {
        private readonly List<Order> orders = new List<Order>();

        /// <summary>
        /// Loads the orders from json to in-memory object
        /// This can easily be unit tested
        /// </summary>
        /// <param name="ordersJson">The json string representing the orders</param>
        public void LoadOrdersFromJson(string ordersJson)
        {
            // Read the orders from the json file and convert to in-memory orders
            foreach (var order in JObject.Parse(ordersJson))
            {
                orders.Add(new Order
                {
                    OrderNumber = order.Key,
                    Destination = new Airport
                    {
                        Code = (AirportCode)Enum.Parse(typeof(AirportCode), order.Value.First.First.ToString())
                    }
                });
            }
        }

        /// <summary>
        /// Assign the orders to the propers flights
        /// </summary>
        /// <param name="flights">The flight list</param>
        public void AssignOrderToFlight(List<Flight> flights)
        {
            foreach (var order in orders)
            {
                // Find the best flight for the order that currently isn't full
                var flight = flights.FirstOrDefault(i => i.Destination.Code == order.Destination.Code && !i.IsFlightFull);
                if (flight != null)
                {
                    flight.LoadOrder(order);
                    order.ScheduledDeliveryFlight = flight;
                }
            }
        }

        /// <summary>
        /// Prints the order information
        /// </summary>
        public void PrintOrderBatchScheduleInformation()
        {
            foreach (var order in orders)
            {
                if (order.ScheduledDeliveryFlight != null)
                {
                    Console.WriteLine($"order: {order.OrderNumber}, flightNumber: {order.ScheduledDeliveryFlight.FlightNumber}, " +
                        $"departure: {order.ScheduledDeliveryFlight.Departure.Code}, arrival: {order.Destination.Code}, day: {order.ScheduledDeliveryFlight.Day}");
                }
                else
                {
                    Console.WriteLine($"order: {order.OrderNumber}, flightNumber: not scheduled");
                }
            }
        }
    }

    /// <summary>
    /// Class to represent an order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the order number
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the flight that this order is assigned to
        /// </summary>
        public Flight ScheduledDeliveryFlight { get; set; }

        /// <summary>
        /// Gets or sets the destination of the order.
        /// </summary>
        public Airport Destination { get; set; }
    }
}
