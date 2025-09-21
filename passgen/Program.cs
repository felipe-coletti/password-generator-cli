using System;
using System.Linq;

class Program
{
    static string GeneratePassword(int length, bool useUpper, bool useLower, bool useNumbers, bool useSymbols)
    {
        string chars = "";

        if (useUpper) chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (useLower) chars += "abcdefghijklmnopqrstuvwxyz";
        if (useNumbers) chars += "0123456789";
        if (useSymbols) chars += "!@#$%^&*()-_=+[]{};:,.<>?";

        if (string.IsNullOrEmpty(chars))
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        var random = new Random();

        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[random.Next(chars.Length)])
            .ToArray());
    }

    static void Main(string[] args)
    {
        if (args.Length == 0 || (args.Length == 1 && args[0] == "--help"))
        {
            Console.WriteLine("Usage: passgen --count <num> --length <num> [--uppercase] [--lowercase] [--numbers] [--specials]");
            return;
        }

        int count = 1;
        int length = 15;

        bool useUpper = args.Contains("--uppercase");
        bool useLower = args.Contains("--lowercase");
        bool useNumbers = args.Contains("--numbers");
        bool useSymbols = args.Contains("--symbols");

        int countIndex = Array.IndexOf(args, "--count");
        if (countIndex >= 0 && countIndex + 1 < args.Length)
            int.TryParse(args[countIndex + 1], out count);

        int lengthIndex = Array.IndexOf(args, "--length");
        if (lengthIndex >= 0 && lengthIndex + 1 < args.Length)
            int.TryParse(args[lengthIndex + 1], out length);

        if (!useUpper && !useLower && !useNumbers && !useSymbols)
        {
            useUpper = true;
            useLower = true;
            useNumbers = true;
            useSymbols = true;
        }

        for (int i = 1; i <= count; i++)
        {
            Console.WriteLine($"{i}: {GeneratePassword(length, useUpper, useLower, useNumbers, useSymbols)}");
        }
    }
}
