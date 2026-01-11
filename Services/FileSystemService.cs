namespace WebsiteWASM.Services {
    public class FileSystemService {
        private Folder rootFolder;

        public FileSystemService() {
            rootFolder = new Folder("test1", null, new List<Folder> {
                new Folder("subfolder1", null, null, new List<File> {
                    new File("This is some test data in file1.txt"),
                    new File("This is some test data in file2.txt")
                }),
                new Folder("subfolder2", null)
            });
        }

        public Folder GetRootFolder() {
            return rootFolder;
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
        }

        public Folder(Folder parentFolder) {
            this.parentFolder = parentFolder;
        }

        public string Name {
            get { return name; }
        }

        public string GetFullPath() {
            if (parentFolder == null) {
                return name;
            } else {
                return parentFolder.GetFullPath() + "/" + name;
            }
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
        public string data;

        public File(string data) {
            this.data = data;
        }
    }
}
