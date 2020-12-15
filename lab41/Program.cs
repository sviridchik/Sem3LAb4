using System;
using System.Configuration;
using System.Threading;
using DataAccessLayer;
using FileManager;
using ServiceLayer;

namespace lab41
{
    class Program
    {
        static void Main(string[] args)
        {
           

            var logger = new Logger();


            AccessAndConnection serviceLayer = new AccessAndConnection();

            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            Thread loggerThreaddb = new Thread(new ThreadStart(serviceLayer.StartAccess));

            loggerThread.Start();
            loggerThreaddb.Start();


        }
    }
}
