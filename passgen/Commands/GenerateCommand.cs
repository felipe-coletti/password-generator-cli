namespace passgen.Commands;

public static class GenerateCommand
{
    public static void PrintHelp()
    {
        Console.WriteLine("Usage: passgen generate [options]");
        Console.WriteLine();
        Console.WriteLine("Generate random passwords.");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --count <num>       Number of passwords (default: 1)");
        Console.WriteLine("  --length <num>      Length of each password (default: 15)");
        Console.WriteLine("  --uppercase         Include uppercase letters");
        Console.WriteLine("  --lowercase         Include lowercase letters");
        Console.WriteLine("  --numbers           Include numbers");
        Console.WriteLine("  --symbols           Include special symbols");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine(" passgen generate    # 1 password, 15 chars, all character types");
        Console.WriteLine(" passgen generate --count 5 --length 15 --uppercase --lowercase --numbers --symbols");
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

        bool useUpper = args.Contains("--uppercase");
        bool useLower = args.Contains("--lowercase");
        bool useNumbers = args.Contains("--numbers");
        bool useSymbols = args.Contains("--symbols");

        int countIndex = Array.IndexOf(args, "--count");

        if (countIndex >= 0 && countIndex + 1 < args.Length)
            _ = int.TryParse(args[countIndex + 1], out count);

        int lengthIndex = Array.IndexOf(args, "--length");

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