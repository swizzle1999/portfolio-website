namespace WebsiteWASM.Services {
    public class FileSystemService {
        public Folder RootFolder { get; set; }

        public Folder? CurrentFolder { get; set; }

        public FileSystemService() {
            RootFolder = new Folder("test1", null, new List<Folder> {
                new Folder("subfolder1", null, null, new List<File> {
                    new File("file1.txt", "This is some test data in file1.txt"),
                    new File("file2.txt", "This is some test data in file2.txt")
                }),
                new Folder("subfolder2", null)
            });

            CurrentFolder = RootFolder;
        }

        public void NavigateToFolder(Folder? folder) {
            CurrentFolder = folder;
        }
    }

    public class Folder {
        private Folder? parentFolder;
        private List<Folder>? subFolders = new();
        private List<File>? files = new();
        private string name = string.Empty;

        public Folder(string name, Folder? parentFolder, List<Folder>? subFolders = null, List<File>? files = null) {
            this.name = name;
            this.parentFolder = parentFolder;
            this.subFolders = subFolders;
            this.files = files;

            foreach (var folder in subFolders ?? new List<Folder>()) {
                folder.parentFolder = this;
            }
        }

        public Folder(Folder parentFolder) {
            this.parentFolder = parentFolder;
        }

        public string Name {
            get { return name; }
        }

        public bool IsRoot => parentFolder == null;

        public string GetFullPath() {
            if (parentFolder == null) {
                return name;
            } else {
                return parentFolder.GetFullPath() + "/" + name;
            }
        }

        public Folder? GetParentFolder() {
            return parentFolder;
        }

        public void AddSubFolder(Folder folder) {
            subFolders?.Add(folder);
        }

        public void AddFile(File file) {
            files?.Add(file);
        }

        public List<Folder>? GetSubFolders() {
            return subFolders;
        }

        public List<File>? GetFiles() {
            return files;
        }
    }

    public class File {
        // theses two fields are public for simplicity, but should be properties in a real implementation 
        public string name;
        public string data;

        public File(string name, string data) {
            this.name = name;
            this.data = data;
        }
    }
}
