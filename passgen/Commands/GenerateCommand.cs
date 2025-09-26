namespace passgen.Commands;

using passgen;
using passgen.Models;
using passgen.Utils;

public static class GenerateCommand
{
    public static void PrintHelp(Command cmd)
    {
        var usage = CommandTreeHelper.GetFullPath(cmd);
        
        Console.WriteLine($"Usage: {ProjectInfo.Name} {usage} [options]");
        Console.WriteLine();
        Console.WriteLine(cmd.Description);
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --count, -C <num>       Number of passwords (default: 1)");
        Console.WriteLine("  --length, -L <num>      Length of each password (default: 15)");
        Console.WriteLine("  --uppercase, -u         Include uppercase letters");
        Console.WriteLine("  --lowercase, -l         Include lowercase letters");
        Console.WriteLine("  --numbers, -n           Include numbers");
        Console.WriteLine("  --symbols, -s           Include special symbols");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine($"  {ProjectInfo.Name} {usage}    # 1 password, 15 chars, all character types");
        Console.WriteLine($"  {ProjectInfo.Name} {usage} --count 5 --length 15 --uppercase --lowercase --numbers --symbols");
        Console.WriteLine($"  {ProjectInfo.Name} {usage} gen -C 5 -L 15 -u -l -n -s");
    }

    private static string GeneratePassword(int length, bool useUpper, bool useLower, bool useNumbers, bool useSymbols)
    {
        string chars = "";

        if (useUpper) chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (useLower) chars += "abcdefghijklmnopqrstuvwxyz";
        if (useNumbers) chars += "0123456789";
        if (useSymbols) chars += "!@#$%^&*()-_=+[]{};:,.<>?";

        if (string.IsNullOrEmpty(chars))
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        var random = new Random();

        return new string([.. Enumerable.Range(0, length).Select(_ => chars[random.Next(chars.Length)])]);
    }

    public static void Execute(string[] args)
    {
        int count = 1;
        int length = 15;

        bool useUpper = args.Contains("--uppercase") || args.Contains("-u");
        bool useLower = args.Contains("--lowercase") || args.Contains("-l");
        bool useNumbers = args.Contains("--numbers") || args.Contains("-n");
        bool useSymbols = args.Contains("--symbols") || args.Contains("-s");

        int countIndex = Array.IndexOf(args, "--count");

        if (countIndex == -1) countIndex = Array.IndexOf(args, "-C");
        if (countIndex >= 0 && countIndex + 1 < args.Length)
            _ = int.TryParse(args[countIndex + 1], out count);

        int lengthIndex = Array.IndexOf(args, "--length");

        if (lengthIndex == -1) lengthIndex = Array.IndexOf(args, "-L");
        if (lengthIndex >= 0 && lengthIndex + 1 < args.Length)
            _ = int.TryParse(args[lengthIndex + 1], out length);

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