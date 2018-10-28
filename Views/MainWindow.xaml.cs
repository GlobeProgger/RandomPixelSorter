using RandomPixelSorter.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace RandomPixelSorter.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(this);
        }

        #region IMainViewModel
        public Image Image
        {
            get { return image; }
        } 
        #endregion

        #region EventHandlers
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        #endregion
    }
}
