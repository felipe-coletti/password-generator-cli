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

        if (args[0] != ProjectInfo.Name)
        {
            Console.WriteLine($"Invalid syntax. Use '{ProjectInfo.Name} <command>'.");
            return;
        }

        if (args.Length > 1)
        {
            string cmdName = args[1];
            var cmd = CommandRegistry.Commands.FirstOrDefault(
                c => c.Name == cmdName || (c.Aliases != null && c.Aliases.Contains(cmdName))
            );

            if (cmd != null)
                cmd.Execute(args);
            else
                Console.WriteLine($"Unknown command. Use '{ProjectInfo.Name} help' to list commands.");
        }
    }
}
