﻿using System;
using System.Collections.Generic;

namespace CUE.NET.Helper
{
    /// <summary>
    /// Offers some extensions and helper-methods for rectangle related things.
    /// </summary>
    public static class RectangleHelper
    {
        /// <summary>
        /// Moves a rectangle by a adding an offset.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="offsetX">The offset for the x-value</param>
        /// <param name="offsetY">The offset for the y-value</param>
        /// <returns>The moved rectangle.</returns>
        public static RectangleF Move(this RectangleF rectangle, float offsetX = 0f, float offsetY = 0f)
        {
            return new RectangleF(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// Calculates the center-point of a rectangle adding an offset.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="offsetX">The offset for the x-value</param>
        /// <param name="offsetY">The offset for the y-value</param>
        /// <returns>The center point of the rectangle.</returns>
        public static PointF GetCenter(this RectangleF rectangle, float offsetX = 0f, float offsetY = 0f)
        {
            return new PointF((rectangle.Left + rectangle.Width / 2f) + offsetX, (rectangle.Top + rectangle.Height / 2f) + offsetY);
        }

        /// <summary>
        /// Creates a rectangle from two corner points.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second points.</param>
        /// <returns>The rectangle created from the two points.</returns>
        public static RectangleF CreateRectangleFromPoints(PointF point1, PointF point2)
        {
            float posX = Math.Min(point1.X, point2.X);
            float posY = Math.Min(point1.Y, point2.Y);
            float width = Math.Max(point1.X, point2.X) - posX;
            float height = Math.Max(point1.Y, point2.Y) - posY;

            return new RectangleF(posX, posY, width, height);
        }

        /// <summary>
        /// Creates a rectangle containing two other rectangles.
        /// </summary>
        /// <param name="rectangle1">The first rectangle.</param>
        /// <param name="rectangle2">The second rectangle.</param>
        /// <returns>The rectangle created from the two rectangles.</returns>
        public static RectangleF CreateRectangleFromRectangles(RectangleF rectangle1, RectangleF rectangle2)
        {
            float posX = Math.Min(rectangle1.X, rectangle2.X);
            float posY = Math.Min(rectangle1.Y, rectangle2.Y);
            float width = Math.Max(rectangle1.X + rectangle1.Width, rectangle2.X + rectangle2.Width) - posX;
            float height = Math.Max(rectangle1.Y + rectangle1.Height, rectangle2.Y + rectangle2.Height) - posY;

            return new RectangleF(posX, posY, width, height);
        }

        /// <summary>
        /// Creates a rectangle containing n other rectangles.
        /// </summary>
        /// <param name="rectangles">The list of rectangles.</param>
        /// <returns>The rectangle created from the rectangles.</returns>
        public static RectangleF CreateRectangleFromRectangles(IEnumerable<RectangleF> rectangles)
        {
            float posX = float.MaxValue;
            float posY = float.MaxValue;
            float posX2 = float.MinValue;
            float posY2 = float.MinValue;

            foreach (RectangleF rectangle in rectangles)
            {
                posX = Math.Min(posX, rectangle.X);
                posY = Math.Min(posY, rectangle.Y);
                posX2 = Math.Max(posX2, rectangle.X + rectangle.Width);
                posY2 = Math.Max(posY2, rectangle.Y + rectangle.Height);
            }

            return CreateRectangleFromPoints(new PointF(posX, posY), new PointF(posX2, posY2));
        }

        /// <summary>
        /// Calculates the percentage of the intersection of two rectangles.
        /// </summary>
        /// <param name="rect">The rectangle from which the percentage should be calculated.</param>
        /// <param name="referenceRect">The intersecting rectangle.</param>
        /// <returns>The percentage of the intersection.</returns>
        public static float CalculateIntersectPercentage(RectangleF rect, RectangleF referenceRect)
        {
            if (rect.IsEmpty || referenceRect.IsEmpty) return 0;

            referenceRect.Intersect(rect); // replace referenceRect with intersect
            return referenceRect.IsEmpty ? 0 : (referenceRect.Width * referenceRect.Height) / (rect.Width * rect.Height);
        }
    }
}
