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
    }
}