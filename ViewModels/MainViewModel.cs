using System;
using System.Windows;
using RandomPixelSorter.Commands;
using RandomPixelSorter.Lib;
using System.ComponentModel;
using System.Windows.Controls;
using RandomPixelSorter.Models;

namespace RandomPixelSorter.ViewModels
{
    /// <summary>
    /// ViewModel for MainWindow
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Creates a MainViewModel instance
        /// </summary>
        /// <param name="view">The view this view model binds to</param>
        public MainViewModel(IMainView view)
        {
            var screen = new Screen(view.Image.Width, view.Image.Height);

           //Fire the commands OnCanExecuteChanged when PropertyChanged gets fired
           PropertyChanged += delegate { ShowSortedPixels.OnCanExecuteChanged(this); };

            Action<object> setRandomPixelAction = _ => SetImageSource(view.Image, screen.GetRandomPixels());
            Action<object> setSortedPixelAction = _ => SetImageSource(view.Image, screen.GetSortedPixels(x => x.GetHue()));
           
            Func<object, bool> canExecute_ShowRandomPixels = _ => true;
            Func<object, bool> canExecute_ShowSortedPixels = _ => screen.IsEmpty == false && screen.IsSorted == false;

            ShowRandomPixels = new GenericCommand(setRandomPixelAction, canExecute_ShowRandomPixels);
            ShowSortedPixels = new GenericCommand(setSortedPixelAction, canExecute_ShowSortedPixels);
        }
        
        /// <summary>
        /// Command to fill the IMainView's image with random pixels
        /// </summary>
        public GenericCommand ShowRandomPixels { get; private set; }

        /// <summary>
        /// Command to sort the IMainView's pixels
        /// </summary>
        public GenericCommand ShowSortedPixels { get; private set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region Implementation
        private Random _rand = new Random();

        /// <summary>
        /// Sets the source of the image to the provided pixel data
        /// </summary>
        /// <param name="image">The target image</param>s
        /// <param name="pixels">The source pixel data</param>
        void SetImageSource(Image image, int[] pixels)
        {
            if (image == null || pixels == null)
                return;

            var bitmap = Herlpers.CreateBitmapSource(image.Width, image.Height);
            var sourceRect = new Int32Rect(0, 0, (int)image.Width, (int)image.Height);

            bitmap.WritePixels(sourceRect, pixels, bitmap.BackBufferStride, 0);
            image.Source = bitmap;

            NotifyPropertyChanged(nameof(image));
        }
        #endregion
    }
}
