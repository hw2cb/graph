using System;
using System.Globalization;
using System.Threading;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            var ci = new CultureInfo("fr-FR");
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;
            Console.WriteLine("Что бы узнать список команд, введите help");
            while (true)
            {
                HandlerCommand.Process(Console.ReadLine(), new Core());
            }
        }
    }
}
