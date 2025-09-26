namespace passgen.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using passgen.Models;
using passgen;

public static class CommandTreeHelper
{
    public static string GetFullPath(Command target)
    {
        var path = FindPath(CommandRegistry.Commands, target);

        return path == null ? target.Name : string.Join(' ', path);
    }

    public static List<string>? FindPath(List<Command> current, Command target)
    {
        foreach (var c in current)
        {
            if (CommandsEqual(c, target))
            {
                return [c.Name];
            }

            if (c.Subcommands != null)
            {
                var sub = FindPath(c.Subcommands, target);

                if (sub != null)
                {
                    sub.Insert(0, c.Name);
                    
                    return sub;
                }
            }
        }

        return null;
    }

    public static bool CommandsEqual(Command a, Command b)
    {
        if (string.Equals(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)) return true;

        if (a.Aliases != null && b.Aliases != null
            && a.Aliases.Intersect(b.Aliases, StringComparer.OrdinalIgnoreCase).Any())
            return true;

        return false;
    }

    public static Command? FindRecursive(List<Command> commands, string token)
    {
        foreach (var cmd in commands)
        {
            if (string.Equals(cmd.Name, token, StringComparison.OrdinalIgnoreCase)
                || (cmd.Aliases != null && cmd.Aliases.Any(a => string.Equals(a, token, StringComparison.OrdinalIgnoreCase))))
                return cmd;

            if (cmd.Subcommands != null)
            {
                var sub = FindRecursive(cmd.Subcommands, token);

                if (sub != null) return sub;
            }
        }

        return null;
    }
}
