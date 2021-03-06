﻿using System;
using System.IO;

namespace Transportly.FlightScheduler
{
    /// <summary>
    /// The entry point for the application
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var flightScheduler = new FlightScheduler();
                flightScheduler.LoadScheduleFromJson(File.ReadAllText("FlightSchedule.json"));
                flightScheduler.PrintSchedule();

                var orderBatch = new OrderBatch();
                orderBatch.LoadOrdersFromJson(File.ReadAllText("Orders.json"));
                orderBatch.AssignOrderToFlight(flightScheduler.Flights);
                orderBatch.PrintOrderBatchScheduleInformation();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
