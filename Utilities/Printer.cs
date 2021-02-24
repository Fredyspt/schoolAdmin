using System.Collections.Generic;
using CoreSchool.entities;
using System.Linq;
using static System.Console;

namespace CoreSchool.Utilities
{
    public static class Printer
    {
        public static void PrintLine(int size)
        {
            WriteLine(new string('=', size));
        }

        public static void PrintTitle(string title)
        {
            var size = title.Length + 4;
            PrintLine(size);
            WriteLine($"| {title} |");
            PrintLine(size);
        }

        public static void Beep(int hz, int duration, int quantity)
        {
            for(int i = 0; i < quantity; i++)
            {
                System.Console.Beep(hz, duration);
            }
        }

        public static void PressEnter()
        {
            WriteLine("Press ENTER to continue...");
        }

        
    }
}