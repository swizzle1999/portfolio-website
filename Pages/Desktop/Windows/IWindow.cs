namespace WebsiteWASM.Pages.Desktop.Windows {
    public interface IWindow {
        public void Show();
        public void Hide();
        public void Toggle();
        public IWindow? Window { get; }
    }
}
