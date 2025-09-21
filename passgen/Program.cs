using passgen.Commands;
using passgen.Models;
class Program
{
    static readonly List<Command> commands = [];

    static Program()
    {
        commands.Add(new Command("generate", "Generate random passwords", GenerateCommand.Execute, GenerateCommand.PrintHelp));
        commands.Add(new Command("help", "Show this help message", static args => HelpCommand.Execute(args, commands)));
    }
    static void Main()
    {
        string projectName = "passgen";

        while (true)
        {
            Console.WriteLine("Enter a command:");

            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1 && parts[0] == "passgen")
                {
                    string cmdName = parts[1].ToLower();
                    var cmd = commands.FirstOrDefault(c => c.Name == cmdName);

                    if (cmd != null)
                        cmd.Execute(parts);
                    else
                        Console.WriteLine($"Unknown command. Use '{projectName} help' to list commands.");
                }
                else
                {
                    Console.WriteLine($"Enter a valid command or use '{projectName} help'.");
                }
            }
            else
            {
                Console.WriteLine($"Enter a valid command or use '{projectName} help'.");
            }
        }
    }
}
