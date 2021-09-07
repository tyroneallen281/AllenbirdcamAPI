using ABC.DomainService.Interfaces;
using ABC.Worker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ABC.Worker
{
    class Program
    {
        public static void Main(string[] args)
        {
            EntryPoint.Startup();
           
            Console.WriteLine("Press 'Enter' to quit");
            Console.ReadLine();
        }

     
    }
}
