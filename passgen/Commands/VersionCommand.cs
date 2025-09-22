namespace passgen.Commands;

using passgen;

public static class VersionCommand
{
    public static void PrintHelp()
    {
        var cmd = CommandRegistry.Commands.FirstOrDefault(c => c.Name == "generate") ?? throw new InvalidOperationException("Generate command not found");
        var cmdDescription = cmd.Description;

        Console.WriteLine($"Usage: {ProjectInfo.Name} version");
        Console.WriteLine();
        Console.WriteLine(cmdDescription);

    }

    public static void Execute(string[] args)
    {
        Console.WriteLine(ProjectInfo.Version);
    }
}