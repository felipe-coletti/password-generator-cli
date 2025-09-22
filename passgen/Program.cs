using passgen;
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter a command:");

            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1 && parts[0] == ProjectInfo.Name)
                {
                    string cmdName = parts[1];
                    var cmd = CommandRegistry.Commands.FirstOrDefault(c => c.Name == cmdName || (c.Aliases != null && c.Aliases.Contains(cmdName)));

                    if (cmd != null)
                        cmd.Execute(parts);
                    else
                        Console.WriteLine($"Unknown command. Use '{ProjectInfo.Name} help' to list commands.");
                }
                else
                {
                    Console.WriteLine($"Enter a valid command or use '{ProjectInfo.Name} help'.");
                }
            }
            else
            {
                Console.WriteLine($"Enter a valid command or use '{ProjectInfo.Name} help'.");
            }
        }
    }
}
