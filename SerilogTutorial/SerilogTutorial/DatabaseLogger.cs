using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SerilogTutorial
{
    public class DatabaseLogger
    {
        public static void Run()
        {
            var logger = new LoggerConfiguration()
        .WriteTo.MSSqlServer(@"Server=.;Database=LogEvents;Trusted_Connection=True;", "Logs")
        .CreateLogger();
            logger.Information("I am an information log");
            logger.Error("Hello, I am an error log");

        }
    }
}
