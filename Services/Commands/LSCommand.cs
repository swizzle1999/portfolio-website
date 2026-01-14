using Microsoft.AspNetCore.Components;
using System.Text;

namespace WebsiteWASM.Services.Commands {
    public class LSCommand : ICommand {
        [Inject]
        private FileSystemService FileSystemService { get; set; }

        public string Name { get; set; } = "ls";
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public LSCommand(FileSystemService fileSystemService) {
            FileSystemService = fileSystemService;
        }

        public string Execute() {
            List<Folder>? subFolders = FileSystemService.CurrentFolder?.GetSubFolders();
            List<File>? files = FileSystemService.CurrentFolder?.GetFiles();
            List<IFileSystemItem?> allItems = new List<IFileSystemItem?>();
            allItems.AddRange(files ?? new List<File>());
            allItems.AddRange(subFolders ?? new List<Folder>());

            allItems.Sort((a, b) => string.Compare(a.Name, b.Name));

            StringBuilder output = new StringBuilder();
            foreach (var item in allItems) {
                if (item is Folder) {
                    output.AppendLine(item.Name + "/");
                } else if (item is File) {
                    output.AppendLine(item.Name);
                } else {
                    throw new Exception("Unknown file system item type.");
                }

                output.Append("\t");
            }

            return output.ToString();
        }
    }
}
