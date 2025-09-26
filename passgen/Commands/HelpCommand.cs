namespace passgen.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using passgen.Models;

public static class HelpCommand
{
    public static void Execute(string[] args, List<Command> commands)
    {
        if (args == null || args.Length == 0)
        {
            PrintTopLevel(commands);

            return;
        }

        var path = args.Select(a => a).ToArray();
        var found = FindByPath(commands, path);

        if (found == null && path.Length == 1)
            found = FindRecursive(commands, path[0]);
        
        if (found == null)
        {
            Console.WriteLine($"No command found matching '{string.Join(' ', args)}'.");

            return;
        }

        if (found.PrintHelp != null)
        {
            found.PrintHelp(found);

            return;
        }

        Console.WriteLine($"{found.Name} - {found.Description}");

        if (found.Aliases != null && found.Aliases.Length > 0)
            Console.WriteLine($"Aliases: {string.Join(", ", found.Aliases)}");

        if (found.Subcommands != null && found.Subcommands.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Subcommands:");

            foreach (var sub in found.Subcommands)
                Console.WriteLine($"  {sub.Name,-12} {sub.Description}");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("No detailed help available for this command.");
        }
    }

    static void PrintTopLevel(List<Command> commands)
    {
        Console.WriteLine("Usage:");
        Console.WriteLine($"  {ProjectInfo.Name} <command> [options]");
        Console.WriteLine($"  {ProjectInfo.Name} <group> <command> [options]");
        Console.WriteLine();
        Console.WriteLine("Available commands:");

        foreach (var cmd in commands)
        {
            Console.WriteLine($"  {cmd.Name,-12} {cmd.Description}");

            if (cmd.Subcommands != null && cmd.Subcommands.Count > 0)
            {
                foreach (var sub in cmd.Subcommands)
                    Console.WriteLine($"    {sub.Name,-10} {sub.Description}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Use '{ProjectInfo.Name} help <command>' or '{ProjectInfo.Name} help <group> <command>' for details.");
    }

    static Command? FindByPath(List<Command> commands, string[] path)
    {
        List<Command> current = commands;
        Command? found = null;

        for (int i = 0; i < path.Length; i++)
        {
            var token = path[i];

            found = current.FirstOrDefault(c =>
                string.Equals(c.Name, token, StringComparison.OrdinalIgnoreCase)
                || (c.Aliases != null && c.Aliases.Any(a => string.Equals(a, token, StringComparison.OrdinalIgnoreCase)))
            );

            if (found == null) return null;

            if (i < path.Length - 1)
            {
                if (found.Subcommands == null) return null;

                current = found.Subcommands;
            }
        }

        return found;
    }

    static Command? FindRecursive(List<Command> commands, string token)
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
