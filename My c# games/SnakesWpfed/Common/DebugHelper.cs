using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SnakesWpfed.Common
{
    public static class DebugHelper
    {
        /// <summary>
        /// writes log to debug
        /// </summary>
        /// <param name="whatToWrite">log to write to debug writeline</param>
        /// <param name="identifier">where the log was from</param>
        public static void WriteLog(string whatToWrite, string identifier)
        {
            Debug.WriteLine(string.Format("log {0} was written from {1}", whatToWrite, identifier));
        }
    }
}
