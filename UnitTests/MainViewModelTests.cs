using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomPixelSorter.ViewModels;
using System.Windows.Controls;

namespace UnitTests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _imageMock = new ImageMock(_imageWidth, _imageHeight);
        }

        [TestMethod]
        public void EmptyScreenCanNotShowSortedPixels()
        {
            // Arrange
            var viewModel = GetViewModel();
            // Act
            // Assert
            Assert.IsFalse(viewModel.ShowSortedPixels.CanExecute(null));
        }

        [TestMethod]
        public void EmptyScreenCanShowRandomPixels()
        {            
            // Arrange
            var viewModel = GetViewModel();
            // Act
            // Assert
            Assert.IsTrue(viewModel.ShowRandomPixels.CanExecute(null));
        }

        [TestMethod]
        public void RandomPixelScreenCanShowSortedPixels()
        {
            // Arrange
            var viewModel = GetViewModel();
            // Act
            viewModel.ShowRandomPixels.Execute(null);
            // Assert
            Assert.IsTrue(viewModel.ShowSortedPixels.CanExecute(null));
        }

        [TestMethod]
        public void SortedPixelScreenCanNotShowSortedPixels()
        {
            // Arrange
            var viewModel = GetViewModel();
            // Act
            viewModel.ShowRandomPixels.Execute(null);
            viewModel.ShowSortedPixels.Execute(null);
            // Assert
            Assert.IsFalse(viewModel.ShowSortedPixels.CanExecute(null));
        }

        [TestMethod]
        public void SortedPixelScreenCanShowRandomPixels()
        {
            // Arrange
            var viewModel = GetViewModel();
            // Act
            viewModel.ShowRandomPixels.Execute(null);
            viewModel.ShowSortedPixels.Execute(null);
            // Assert
            Assert.IsTrue(viewModel.ShowRandomPixels.CanExecute(null));
        }

        #region Implementation
        ImageMock _imageMock;
        double _imageWidth = 400;
        double _imageHeight = 300;

        /// <summary>
        /// Provides a newly instantiated view model
        /// </summary>
        private MainViewModel GetViewModel()
        {
            return new MainViewModel(_imageMock);
        }

        /// <summary>
        /// Mock class, implements IMainView
        /// </summary>
        private class ImageMock : IMainView
        {
            /// <summary>
            /// Creates a new ImageMock instance
            /// </summary>
            /// <param name="width">The width of the image</param>
            /// <param name="height">The height of the image</param>
            public ImageMock(double width, double height)
            {
                _image = new Image();
                _image.Width = width;
                _image.Height = height;
            }

            #region IMainView
            public Image Image => _image; 
            #endregion

            #region Implementation
            Image _image;
            #endregion
        }
        #endregion
    }
}
