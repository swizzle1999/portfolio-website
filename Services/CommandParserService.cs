using WebsiteWASM.Services.Commands;

namespace WebsiteWASM.Services {
    public class CommandParserService {
        public List<ICommand> ValidCommands = new List<ICommand>();

        public CommandParserService(FileSystemService fileSystemService) {
            ValidCommands.Add(new HelpCommand(this));
            ValidCommands.Add(new LSCommand(fileSystemService));
            ValidCommands.Add(new CDCommand(fileSystemService));
        }

        public string ParseCommand(string text) {
            var tokens = text.Split(' ').ToList();

            ICommand? command = null;
            for (int i = 0; i < tokens.Count; i++) {
                tokens[i] = tokens[i].Trim();
                // Figure out which command it is
                if (i == 0) {
                    foreach (var cmd in ValidCommands) {
                        if (tokens[i].Equals(cmd.Name, StringComparison.OrdinalIgnoreCase)) {
                            command = cmd;
                            break;
                        }
                    }
                }

                // Parse parameters
                else {
                    if (tokens[i].StartsWith("--")) {
                        var paramParts = tokens[i].Substring(2).Split('=');
                        var paramName = paramParts[0];
                        var paramValue = paramParts.Length > 1 ? paramParts[1] : string.Empty;
                        command?.Parameters.Add(new Parameter(paramName, paramValue));
                    } else {
                        command?.Parameters.Add(new Parameter(string.Empty, tokens[i]));
                    }
                }
            }

            if (command == null) {
                return $"Error: Command not found.";
            }

            string commandOuptput = command.Execute();
            command.CleanUp();

            return commandOuptput;

        }

    }

    public interface ICommand  {
        public string Name { get; set; }
        public string HelpText { get; set; }

        public List<Parameter> Parameters { get; set; }
        public string Execute();
        public void CleanUp() {
            Parameters.Clear();
        }
    }

    public class Parameter {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public Parameter(string name, string value) {
            Name = name;
            Value = value;
        }

        public bool IsPositional() {
            return string.IsNullOrEmpty(Name);
        }
    }
}
