using System;
using System.IO;

namespace Transportly.FlightScheduler
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // TODO add stylecop
            // TODO add comments
            // TODO read the doc and make sure it follows the principles
            var flightScheduler = new FlightScheduler();
            flightScheduler.LoadSchedule(File.ReadAllText("FlightSchedule.json"));
            flightScheduler.PrintSchedule();

            var orderBatch = new OrderBatch();
            orderBatch.LoadOrders(File.ReadAllText("Orders.json"));

            flightScheduler.LoadOrderBatch(orderBatch);
            orderBatch.PrintOrderBatchScheduleInformation();

            Console.ReadLine();
        }
    }
}
