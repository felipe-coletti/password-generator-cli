namespace passgen.Models;

public record Command(
    string Name,
    string Description,
    Action<string[]>? Execute = null,
    Action? PrintHelp = null,
    string[]? Aliases = null,
    List<Command>? Subcommands = null
);