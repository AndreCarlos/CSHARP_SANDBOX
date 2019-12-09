using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerilogTutorial
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            HelloSerilog.HelloLog();
            //HelloSerilog.ParameterizedLog();
            //HelloSerilog.TemplateLog();

            //BasicLogger.Run();

            //CustomizedLogger.SetLevel();

            //DatabaseLogger.Run();
        }
    }
}
