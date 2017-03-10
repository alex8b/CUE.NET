﻿// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

using System;
using CUE.NET.Devices.Generic;

namespace CUE.NET.Brushes
{
    ///// <summary>
    ///// Represents a brush drawing an image.
    ///// </summary>
    //public class ImageBrush : AbstractBrush
    //{
    //    #region Enums

    //    /// <summary>
    //    /// Contains a list of available image-scale modes.
    //    /// </summary>
    //    public enum ScaleMode
    //    {
    //        /// <summary>
    //        /// Stretches the image to fit inside the target rectangle.
    //        /// </summary>
    //        Stretch
    //    }

    //    /// <summary>
    //    /// Contains a list of available image-interpolation modes.
    //    /// </summary>
    //    public enum InterpolationMode
    //    {
    //        /// <summary>
    //        /// Selects the pixel closest to the target point.
    //        /// </summary>
    //        PixelPerfect
    //    }

    //    #endregion

    //    #region Properties & Fields

    //    /// <summary>
    //    /// Gets or sets the image drawn by the brush. If null it will default to full transparent.
    //    /// </summary>
    //    public Bitmap Image { get; set; }

    //    /// <summary>
    //    /// Gets or sets the <see cref="ScaleMode" /> used to scale the image if needed.
    //    /// </summary>
    //    public ScaleMode ImageScaleMode { get; set; } = ScaleMode.Stretch;

    //    /// <summary>
    //    /// Gets or sets the <see cref="InterpolationMode" /> used to interpolate the image if needed.
    //    /// </summary>
    //    public InterpolationMode ImageInterpolationMode { get; set; } = InterpolationMode.PixelPerfect;

    //    #endregion

    //    #region Methods
        
    //    /// <summary>
    //    /// Gets the color at an specific point assuming the brush is drawn into the given rectangle.
    //    /// </summary>
    //    /// <param name="rectangle">The rectangle in which the brush should be drawn.</param>
    //    /// <param name="renderTarget">The target (key/point) from which the color should be taken.</param>
    //    /// <returns>The color at the specified point.</returns>
    //    protected override CorsairColor GetColorAtPoint(RectangleF rectangle, BrushRenderTarget renderTarget)
    //    {
    //        if (Image == null || Image.Width == 0 || Image.Height == 0)
    //            return CorsairColor.Transparent;

    //        //TODO DarthAffe 16.03.2016: Refactor to allow more scale-/interpolation-modes
    //        float scaleX = Image.Width / rectangle.Width;
    //        float scaleY = Image.Height / rectangle.Height;

    //        int x = (int)(renderTarget.Point.X * scaleX);
    //        int y = (int)(renderTarget.Point.Y * scaleY);

    //        x = Math.Max(0, Math.Min(x, Image.Width - 1));
    //        y = Math.Max(0, Math.Min(y, Image.Height - 1));

    //        return Image.GetPixel(x, y);
    //    }

    //    #endregion
    //}
}
