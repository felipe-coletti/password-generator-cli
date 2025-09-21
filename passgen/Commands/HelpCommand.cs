namespace passgen.Commands;

using passgen.Models;

public static class HelpCommand
{
    public static void Execute(string[] args, List<Command> commands)
    {
        if (args.Length > 2)
        {
            string cmdName = args[2].ToLower();
            var cmd = commands.FirstOrDefault(c => c.Name == cmdName);

            if (cmd != null && cmd.PrintHelp != null)
            {
                cmd.PrintHelp();
            }
            else
            {
                Console.WriteLine($"No detailed help found for '{cmdName}'.");
            }
        }
        else
        {
            Console.WriteLine("Usage: passgen <command> [options]");
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            foreach (var cmd in commands)
            {
                Console.WriteLine($"  {cmd.Name,-10} {cmd.Description}");
            }
            Console.WriteLine();
            Console.WriteLine("Use 'passgen help <command>' to see detailed options for a command.");
        }
    }
}