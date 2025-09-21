namespace passgen.Models;

public record Command(
    string Name,
    string Description,
    Action<string[]> Execute,
    Action? PrintHelp = null
);