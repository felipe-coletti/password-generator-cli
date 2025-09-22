using passgen;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine($"Enter a valid command or use '{ProjectInfo.Name} help'.");
            return;
        }

        string cmdName = args[0];
        var cmd = CommandRegistry.Commands.FirstOrDefault(
            c => c.Name == cmdName || (c.Aliases != null && c.Aliases.Contains(cmdName))
        );

        if (cmd != null)
            cmd.Execute(args);
        else
            Console.WriteLine($"Unknown command. Use '{ProjectInfo.Name} help' to list commands.");
    }
}
