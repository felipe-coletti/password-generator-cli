namespace passgen.Commands;

using System;
using passgen.Models;
using passgen.Utils;

public static class VersionCommand
{
    public static void PrintHelp(Command cmd)
    {
        var usage = CommandTreeHelper.GetFullPath(cmd);

        Console.WriteLine($"Usage: {ProjectInfo.Name} {usage}");
        Console.WriteLine();
        Console.WriteLine(cmd.Description ?? "Show the current version of the application");
    }

    public static void Execute(string[] args)
    {
        Console.WriteLine(ProjectInfo.Version);
    }
}
