using Microsoft.Owin.Hosting;
using System;

namespace Archiver.WebService
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:8081/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}