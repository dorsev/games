using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SnakesWpfed.Common;
using System.IO;

namespace SnakesWpfed
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        /// Handler for unknonw exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                TryLogUnHandledException(e);
            }
            catch
            {
                //?
            }
        }

        private void TryLogUnHandledException(UnhandledExceptionEventArgs e)
        {
            DebugHelper.WriteLog("applicatino crashed!!!" + e.ExceptionObject.ToString(), "app.xaml.cs");
            using (StreamWriter s = new StreamWriter("SnakesCrash.txt", true))
            {
                s.WriteLine(e.ExceptionObject);
            }
        }


    }
}
