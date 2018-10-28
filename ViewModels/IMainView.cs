namespace RandomPixelSorter.ViewModels
{
    /// <summary>
    /// Declares the interface injected into the <paramref name="MainViewModel"/>
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// An image control to be provided by the implementation
        /// </summary>
        System.Windows.Controls.Image Image { get; }
    }
}
