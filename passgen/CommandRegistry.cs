namespace passgen;

using passgen.Models;
using passgen.Commands;

public static class CommandRegistry
{
    public static readonly List<Command> Commands = [];

    static CommandRegistry()
    {
        Commands.Add(new Command("generate", "Generate random passwords", GenerateCommand.Execute, GenerateCommand.PrintHelp, ["gen"]));
        Commands.Add(new Command("help", "Show this help message", static args => HelpCommand.Execute(args, Commands)));
        Commands.Add(new Command("version", "Show the current version of the application", VersionCommand.Execute, VersionCommand.PrintHelp));
    }
}
