using System;
using Serilog;

namespace SerilogTutorial
{
    public class HelloSerilog
    {
        public static void HelloLog()
        {
            // Create Logger
            var logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();
            // Output logs
            logger.Information("Hello, Serilog!");
        }

        public static void ParameterizedLog()
        {
            // Create Logger
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            // write structured data
            logger.Information("Processed {Number} records in {Time} ms", 500, 120);
        }

        public static void TemplateLog()
        {
            // Create Logger
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            // prepare data
            var order = new {Id = 12, Total = 128.50, CustomerId = 72};
            var customer = new {Id = 72, Name = "John Smith"};
            // write log message
            logger.Information("New orders {OrderId} by {Customer}", order.Id, customer);
        }
    }
}