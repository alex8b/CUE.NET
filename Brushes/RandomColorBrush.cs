﻿// ReSharper disable UnusedMember.Global

using System;
using CUE.NET.Devices.Generic;
using CUE.NET.Helper;

namespace CUE.NET.Brushes
{
    //TODO DarthAffe 30.09.2015: Like this the brush seems kinda useless. Think about making it cool.

    /// <summary>
    /// Represents a brush drawing random colors.
    /// </summary>
    public class RandomColorBrush : AbstractBrush
    {
        #region Properties & Fields
        
        private Random _random = new Random();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the color at an specific point assuming the brush is drawn into the given rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle in which the brush should be drawn.</param>
        /// <param name="renderTarget">The target (key/point) from which the color should be taken.</param>
        /// <returns>The color at the specified point.</returns>
        protected override CorsairColor GetColorAtPoint(RectangleF rectangle, BrushRenderTarget renderTarget)
        {
            return ColorHelper.ColorFromHSV((float)_random.NextDouble() * 360f, 1, 1);
        }

        #endregion
    }
}
