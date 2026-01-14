using WebsiteWASM.Services.Commands;

namespace WebsiteWASM.Services {
    public class CommandParserService {
        public List<ICommand> ValidCommands = new List<ICommand>();

        public CommandParserService(FileSystemService fileSystemService) {
            ValidCommands.Add(new HelpCommand());
            ValidCommands.Add(new LSCommand(fileSystemService));
        }

        public string ParseCommand(string command) {
            var tokens = command.Split(' ').ToList();

            foreach (var token in tokens) {
                foreach (var cmd in ValidCommands) {
                    if (token.Equals(cmd.Name, StringComparison.OrdinalIgnoreCase)) {
                        return cmd.Execute();
                    }
                }
            }

            return string.Empty;

        }

    }

    public interface ICommand  {
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Execute();
    }

    public class Parameter {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
