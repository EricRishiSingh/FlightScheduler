using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Transportly.FlightScheduler
{
    public class OrderBatch
    {
        public List<Order> Orders { get; private set; } = new List<Order>();

        public void LoadOrders(string ordersJson)
        {
            // Read the orders from the json file and convert to in-memory orders
            var orders = JObject.Parse(ordersJson);
            foreach (var order in orders)
            {
                Orders.Add(new Order
                {
                    OrderNumber = order.Key,
                    Destination = new Airport
                    {
                        Code = (AirportCode)Enum.Parse(typeof(AirportCode), order.Value.First.First.ToString())
                    }
                });
            }
        }

        public void PrintOrderBatchScheduleInformation()
        {
            foreach (var order in Orders)
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

    public class Order
    {
        public string OrderNumber { get; set; }

        public Flight ScheduledDeliveryFlight { get; set; }

        /// <summary>
        /// Gets or sets the destination of the order.
        /// </summary>
        public Airport Destination { get; set; }
    }
}
