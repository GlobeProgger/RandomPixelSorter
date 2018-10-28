using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomPixelSorter.Models
{
    /// <summary>
    /// Represents a screen
    /// </summary>
    /// <remarks>Holds screen pixels and supports the generation of random pixels and pixel sorting</remarks>
    public class Screen
    {
        /// <summary>
        /// Creates a new instance of a screen
        /// </summary>
        /// <param name="width">The width of the screen</param>
        /// <param name="height">The height of the screen</param>
        public Screen(double width, double height)
        {
            _width = width;
            _height = height;

            IsSorted = true;
        }

        /// <summary>
        /// Determines if the screen pixel data is sorted
        /// </summary>
        public bool IsSorted { get; private set; }

        /// <summary>
        /// Determines if the screen has pixel data
        /// </summary>
        public bool IsEmpty => _pixels == null || _pixels?.Any() == false;

        /// <summary>
        /// Returns an array of pixels in form of integer values with random hue property
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random values. Defaults to the lower bound of int</param>
        /// <param name="maxValue">The inclusive upper bound of the random values. Defaults to the upper bound of int</param>
        /// <returns>An array of 32-bit signed integer values greater than or equal to minValue and less than maxValue</returns>
        public int[] GetRandomPixels(int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            _pixels = new int[_screenSize];

            for (var index = 0; index < _pixels.Length; index++)
                _pixels[index] = _rand.Next(minValue, maxValue);

            IsSorted = false;

            return _pixels;
        }

        /// <summary>
        /// Returns a sorted array of pixels in form of integer values
        /// </summary>
        /// <param name="sortCallback">The desired property to sort the pixels by</param>
        /// <returns>A sorted array of 32-bit signed integer values</returns>
        public int[] GetSortedPixels(Func<System.Drawing.Color, double> sortCallback)
        {
            if (IsEmpty)
                return null;

            var sortedPixels = new int[_screenSize];

            int pixelIndex = 0;
            var columnIndex = 0;

            var pixelToSortValueDict = new Dictionary<int, double>();

            // Create a dictionary of the row indexes and the sorting values of all pixel in the current column
            for (int widthIndex = 0; widthIndex < _screenSize; widthIndex++)
            {
                var pixelColor = System.Drawing.Color.FromArgb(_pixels[widthIndex]);

                double sortValue = sortCallback(pixelColor);

                pixelToSortValueDict.Add(widthIndex, sortValue);
            }

            // Sort the dictionary in ascending order of the sorting values and 
            // copy the elements of the original pixel array in order of the 
            // sorted dictionary's index to the returning array 
            // while rotating the screen pixels by 90°
            foreach (var pair in pixelToSortValueDict.OrderBy(pair => pair.Value))
            {
                if (pixelIndex >= _screenSize)
                {
                    columnIndex++;
                    pixelIndex = columnIndex;
                }

                sortedPixels[pixelIndex] = _pixels[pair.Key];

                pixelIndex += (int)_width;
            }

            IsSorted = true;

            return sortedPixels;
        }

        #region Implementation
        private double _width;
        private int[] _pixels;
        private double _height;
        private Random _rand = new Random();

        private int _screenSize => (int)(_width * _height);
        #endregion
    }
}
