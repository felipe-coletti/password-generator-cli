using passgen.Models;

namespace passgen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Enter a valid command or use '{ProjectInfo.Name} help'.");

                return;
            }

            ExecuteCommand(args, CommandRegistry.Commands);
        }

        static void ExecuteCommand(string[] args, List<Command> commands)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Use '{ProjectInfo.Name} help'.");

                return;
            }

            var head = args[0];
            var cmd = commands.FirstOrDefault(c =>
                string.Equals(c.Name, head, StringComparison.OrdinalIgnoreCase)
                || (c.Aliases != null && c.Aliases.Any(a => string.Equals(a, head, StringComparison.OrdinalIgnoreCase)))
            );

            if (cmd == null)
            {
                Console.WriteLine($"Unknown command '{head}'. Use '{ProjectInfo.Name} help'.");

                return;
            }

            var tail = args.Skip(1).ToArray();

            if (cmd.Subcommands != null && tail.Length > 0)
            {
                ExecuteCommand(tail, cmd.Subcommands);

                return;
            }

            if (cmd.Execute != null)
            {
                cmd.Execute(tail);

                return;
            }

            if (cmd.Subcommands != null)
            {
                Console.WriteLine($"Available subcommands for '{cmd.Name}':");

                foreach (var sub in cmd.Subcommands)
                    Console.WriteLine($"  {sub.Name} - {sub.Description}");

                return;
            }

            Console.WriteLine($"No action for '{cmd.Name}'.");
        }
    }
}
