﻿// ReSharper disable VirtualMemberNeverOverriden.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable VirtualMemberNeverOverridden.Global

using System.Collections.Generic;
using System.Linq;
using CUE.NET.ColorCorrection;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Keyboard.Enums;
using CUE.NET.Effects;
using CUE.NET.Helper;

namespace CUE.NET.Brushes
{
    /// <summary>
    /// Represents a basic brush.
    /// </summary>
    public abstract class AbstractBrush : AbstractEffectTarget<IBrush>, IBrush
    {
        #region Properties & Fields

        /// <summary>
        /// Gets or sets the calculation mode used for the rectangle/points used for color-selection in brushes.
        /// </summary>
        public BrushCalculationMode BrushCalculationMode { get; set; } = BrushCalculationMode.Relative;

        /// <summary>
        /// Gets or sets the overall percentage brightness of the brush.
        /// </summary>
        public float Brightness { get; set; }

        /// <summary>
        /// Gets or sets the overall percentage opacity of the brush.
        /// </summary>
        public float Opacity { get; set; }

        /// <summary>
        /// Gets a list of <see cref="IColorCorrection"/> used to correct the colors of the brush.
        /// </summary>
        public IList<IColorCorrection> ColorCorrections { get; } = new List<IColorCorrection>();

        /// <summary>
        /// Gets the Rectangle used in the last render pass.
        /// </summary>
        public RectangleF RenderedRectangle { get; protected set; }

        /// <summary>
        /// Gets a dictionary containing all colors for points calculated in the last render pass.
        /// </summary>
        public Dictionary<BrushRenderTarget, CorsairColor> RenderedTargets { get; } = new Dictionary<BrushRenderTarget, CorsairColor>();

        /// <summary>
        /// Gets the strongly-typed target used for the effect.
        /// </summary>
        protected override IBrush EffectTarget => this;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBrush"/> class.
        /// </summary>
        /// <param name="brightness">The overall percentage brightness of the brush. (default: 1f)</param>
        /// <param name="opacity">The overall percentage opacity of the brush. (default: 1f)</param>
        protected AbstractBrush(float brightness = 1f, float opacity = 1f)
        {
            this.Brightness = brightness;
            this.Opacity = opacity;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performas the render pass of the brush and calculates the raw colors for all requested points.
        /// </summary>
        /// <param name="rectangle">The rectangle in which the brush should be drawn.</param>
        /// <param name="renderTargets">The targets (keys/points) of which the color should be calculated.</param>
        public virtual void PerformRender(RectangleF rectangle, IEnumerable<BrushRenderTarget> renderTargets)
        {
            RenderedRectangle = rectangle;
            RenderedTargets.Clear();

            foreach (BrushRenderTarget point in renderTargets)
                RenderedTargets[point] = new CorsairColor(GetColorAtPoint(rectangle, point)); // Clone the color, we don't want to have reference issues here and brushes might return the same color multiple times!
        }

        /// <summary>
        /// Performs the finalize pass of the brush and calculates the final colors for all previously calculated points.
        /// </summary>
        public virtual void PerformFinalize()
        {
            List<BrushRenderTarget> renderTargets = RenderedTargets.Keys.ToList();
            foreach (BrushRenderTarget renderTarget in renderTargets)
                RenderedTargets[renderTarget] = FinalizeColor(RenderedTargets[renderTarget]); // Cloning here again shouldn't be needed since we did this above.
        }

        /// <summary>
        /// Gets the color at an specific point assuming the brush is drawn into the given rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle in which the brush should be drawn.</param>
        /// <param name="renderTarget">The target (key/point) from which the color should be taken.</param>
        /// <returns>The color at the specified point.</returns>
        protected abstract CorsairColor GetColorAtPoint(RectangleF rectangle, BrushRenderTarget renderTarget);

        /// <summary>
        /// Finalizes the color by appliing the overall brightness and opacity.<br/>
        /// This method should always be the last call of a <see cref="GetColorAtPoint" /> implementation.
        /// If you overwrite this method please make sure that you never return the same color-object twice to prevent reference-issues!
        /// </summary>
        /// <param name="color">The color to finalize.</param>
        /// <returns>The finalized color.</returns>
        protected virtual CorsairColor FinalizeColor(CorsairColor color)
        {
            foreach (IColorCorrection colorCorrection in ColorCorrections)
                colorCorrection.ApplyTo(color);

            // Since we use HSV to calculate there is no way to make a color 'brighter' than 100%
            // Be carefull with the naming: Since we use HSV the correct term is 'value' but outside we call it 'brightness'
            // THIS IS NOT A HSB CALCULATION!!!
            float finalBrightness = color.GetHSVValue() * (Brightness < 0 ? 0 : (Brightness > 1f ? 1f : Brightness));
            byte finalAlpha = (byte)(color.A * (Opacity < 0 ? 0 : (Opacity > 1f ? 1f : Opacity)));

            return ColorHelper.ColorFromHSV(color.GetHSVHue(), color.GetHSVSaturation(), finalBrightness, finalAlpha);
        }

        #endregion
    }
}
