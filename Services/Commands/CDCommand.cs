using Microsoft.AspNetCore.Components;
using System.Text;

namespace WebsiteWASM.Services.Commands {
    public class CDCommand : ICommand {

        public string Name { get; set; } = "cd";
        public string HelpText { get; set; } = "Change the current directory. Usage: cd <directory_name> or cd .. to go up one level.";
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        private FileSystemService FileSystemService { get; set; }

        public CDCommand(FileSystemService fileSystemService) {
            FileSystemService = fileSystemService;
        }

        public string Execute() {
            var targetPath = Parameters.FirstOrDefault()?.Value;

            if (targetPath == "..") {
                FileSystemService.NavigateToParentFolder();
                return string.Empty;
            }

            if (string.IsNullOrEmpty(targetPath)) {
                return "Error: No target path specified.";
            }

            FileSystemService.NavigateToFolder(targetPath);

            return string.Empty;
        }
    }
}
