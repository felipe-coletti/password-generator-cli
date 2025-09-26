namespace passgen.Models;

public record Command(
    string Name,
    string Description,
    Action<string[]>? Execute = null,
    Action<Command>? PrintHelp = null,
    string[]? Aliases = null,
    List<Command>? Subcommands = null
);