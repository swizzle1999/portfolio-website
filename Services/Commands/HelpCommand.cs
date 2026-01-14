
using System.Text;

namespace WebsiteWASM.Services.Commands {
    public class HelpCommand : ICommand {
        public string Name { get; set; } = "help";
        public string HelpText { get; set; } = "Display help information about available commands.";

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        private CommandParserService CommandParserService { get; set; }

        public HelpCommand(CommandParserService commandParserService) {
            CommandParserService = commandParserService;
        }

        public string Execute() {
            // Help for a s
            if (Parameters.Count > 0) {
                var commandName = Parameters[0].Value;
                var command = CommandParserService.ValidCommands
                    .FirstOrDefault(c => c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));
                if (command != null) {
                    return FormatCommandHelp(command);
                } else {
                    return $"Error: Command '{commandName}' not found.\r\n";
                }
            }

            // List all commands help
            var orderedCommands = CommandParserService.ValidCommands.OrderBy(c => c.Name);
            StringBuilder helpOutput = new StringBuilder();
            helpOutput.Append("\r\n");

            foreach (var command in orderedCommands) {
                helpOutput.Append(FormatCommandHelp(command));
            }

            return helpOutput.ToString();
        }

        private string FormatCommandHelp(ICommand command) {
            return $"{command.Name}: {command.HelpText}\r\n";
        }
    }
}
