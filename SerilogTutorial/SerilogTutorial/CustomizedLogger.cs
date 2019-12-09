using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SerilogTutorial
{
    public class CustomizedLogger
    {
        public static void SetLevel()
        {
            var logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.ColoredConsole()
             .CreateLogger();
            var appointment =
                new { Id = 1, Subject = "Meeting of database migration", Timestamp = new DateTime(2015, 3, 12) };
            logger.Verbose("You will not see this log");
            logger.Information("An appointment is booked successfully: {@appountment}", appointment);
            logger.Error("Failed to book an appointment: {@appountment}", appointment);
        }
    }
}
