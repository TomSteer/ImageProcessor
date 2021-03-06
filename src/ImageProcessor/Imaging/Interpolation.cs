﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Interpolation.cs" company="James South">
//   Copyright (c) James South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Provides interpolation routines for resampling algorithms.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Imaging
{
    using System;

    /// <summary>
    /// Provides interpolation routines for resampling algorithms.
    /// </summary>
    internal static class Interpolation
    {
        /// <summary>
        /// Returns a bicubic kernel for the given value.
        /// <remarks>
        /// The function implements bicubic kernel W(x) as described on
        /// <a href="https://en.wikipedia.org/wiki/Lanczos_resampling#Algorithm">Wikipedia</a>
        /// (coefficient <c>a</c> is set to <c>-0.5</c>).
        /// </remarks>
        /// </summary>
        /// <param name="x">X value.</param> 
        /// <returns>
        /// The <see cref="double"/> representing the bicubic coefficient.
        /// </returns>
        public static double BiCubicKernel(double x)
        {
            // The coefficient.
            double a = -0.5;

            if (x < 0)
            {
                x = -x;
            }

            double bicubicCoeffient = 0;

            if (x <= 1)
            {
                bicubicCoeffient = (((1.5 * x) - 2.5) * x * x) + 1;
            }
            else if (x < 2)
            {
                bicubicCoeffient = (((((a * x) + 2.5) * x) - 4) * x) + 2;
            }

            return bicubicCoeffient;
        }

        /// <summary>
        /// Returns a Lanczos kernel for the given value.
        /// <remarks>
        /// The function implements Lanczos kernel as described on
        /// <see href="https://en.wikipedia.org/wiki/Lanczos_resampling#Algorithm">Wikipedia</see>
        /// </remarks>
        /// </summary>
        /// <param name="x">X value.</param> 
        /// <returns>
        /// The <see cref="double"/> representing the bicubic coefficient.
        /// </returns>
        internal static double LanczosKernel(double x)
        {
            if (x < 0)
            {
                x = -x;
            }

            if (x < 3)
            {
                return SinC(x) * SinC(x / 3f);
            }

            return 0;
        }

        /// <summary>
        /// Gets the result of a sine cardinal function for the given value.
        /// </summary>
        /// <param name="x">
        /// The value to calculate the result for.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private static double SinC(double x)
        {
            if (Math.Abs(x) > .0001)
            {
                x *= Math.PI;
                return Math.Sin(x) / x;
            }

            return 1;
        }
    }
}
