using System;
namespace Service.Helpers.Extentions
{
    public static class ConsoleExtentions
    {
        public static void WriteConsole(this ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

