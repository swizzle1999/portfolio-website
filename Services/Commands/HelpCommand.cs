
namespace WebsiteWASM.Services.Commands {
    public class HelpCommand : ICommand {
        public string Name { get; set; } = "help";
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public HelpCommand() { }

        public string Execute() {
            return "help function ran";
        }
    }
}
