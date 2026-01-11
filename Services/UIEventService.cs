namespace WebsiteWASM.Services {
    public class UIEventService {
        public event EventHandler? TerminalCloseRequested;
        public event EventHandler? FileBrowserCloseRequested;

        public void RequestTerminalClose(object? sender, EventArgs e) => TerminalCloseRequested?.Invoke(sender, e);
        public void RequestFileBrowserClose(object? sender, EventArgs e) => FileBrowserCloseRequested?.Invoke(sender, e);
    }
}
