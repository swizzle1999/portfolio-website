namespace WebsiteWASM.Services {
    public class UIEventService {
        public event EventHandler? TerminalCloseRequested;
        public void RequestTerminalClose(object? sender, EventArgs e) => TerminalCloseRequested?.Invoke(sender, e);
    }
}
