using Microsoft.AspNetCore.Components;
using System.Text;

namespace WebsiteWASM.Services.Commands {
    public class LSCommand : ICommand {
        [Inject]
        private FileSystemService FileSystemService { get; set; }

        public string Name { get; set; } = "ls";
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public LSCommand() { }

        public string Execute() {
            List<Folder>? subFolders = FileSystemService.CurrentFolder?.GetSubFolders();
            List<File>? files = FileSystemService.CurrentFolder?.GetFiles();

            // FUCK THESE SHITTY NULL REFERENCES. THIS AINT IT.
            List<IFileSystemItem?> allItems = (subFolders?.Cast<IFileSystemItem?>().Concat(files?.Cast<IFileSystemItem?>())).ToList();
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
            }

            return output.ToString();
        }
    }
}
