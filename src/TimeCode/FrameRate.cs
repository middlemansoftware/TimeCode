// <copyright file="FrameRate.cs" company="Middleman Software, Inc.">
//  Middleman Time Code Library - Frame Rate class
//  Copyright © 2022, Middleman Software, Inc.
//  All rights reserved.
// </copyright>
// <author>James Heliker</author>
using System;

namespace Middleman
{
    /// <summary>
    /// Represents a frame rate, to support SMPTE ST 12 time code.
    /// </summary>
    public class FrameRate
    {
        #region Private Fields

        private int framesPerSecond;
        private double frameRateDivisor;
        private bool dropFrame;
        private int maximumFrames;

        #endregion Private Fields

        #region Constructors

        public FrameRate(FrameRates frameRate)
        {
            if (frameRate == FrameRates.FPS_23_976_NDF)
            {
                this.framesPerSecond = 24;
                this.frameRateDivisor = 1.001;
                this.dropFrame = false;
                this.maximumFrames = 2073599;
            }
            else if (frameRate == FrameRates.FPS_24_NDF)
            {
                this.framesPerSecond = 24;
                this.frameRateDivisor = 1;
                this.dropFrame = false;
                this.maximumFrames = 2073599;
            }
            else if (frameRate == FrameRates.FPS_25_NDF)
            {
                this.framesPerSecond = 25;
                this.frameRateDivisor = 1;
                this.dropFrame = false;
                this.maximumFrames = 2159999;
            }
            else if (frameRate == FrameRates.FPS_29_97_DF)
            {
                this.framesPerSecond = 30;
                this.frameRateDivisor = 1.001;
                this.dropFrame = true;
                this.maximumFrames = 2589407;
            }
            else if (frameRate == FrameRates.FPS_29_97_NDF)
            {
                this.framesPerSecond = 30;
                this.frameRateDivisor = 1.001;
                this.dropFrame = false;
                this.maximumFrames = 2591999;
            }
            else if (frameRate == FrameRates.FPS_30_NDF)
            {
                this.framesPerSecond = 30;
                this.frameRateDivisor = 1;
                this.dropFrame = false;
                this.maximumFrames = 2591999;
            }
            else if (frameRate == FrameRates.FPS_50_NDF)
            {
                this.framesPerSecond = 50;
                this.frameRateDivisor = 1;
                this.dropFrame = false;
                this.maximumFrames = 4319999;
            }
            else if (frameRate == FrameRates.FPS_59_94_DF)
            {
                this.framesPerSecond = 60;
                this.frameRateDivisor = 1.001;
                this.dropFrame = true;
                this.maximumFrames = 5178815;
            }
            else if (frameRate == FrameRates.FPS_59_94_NDF)
            {
                this.framesPerSecond = 60;
                this.frameRateDivisor = 1.001;
                this.dropFrame = false;
                this.maximumFrames = 5183999;
            }
            else if (frameRate == FrameRates.FPS_60_NDF)
            {
                this.framesPerSecond = 60;
                this.frameRateDivisor = 1;
                this.dropFrame = false;
                this.maximumFrames = 5183999;
            }
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// <para>The base frame rate, not considering the <see cref="FrameRateDivisor"/>.</para>
        /// <para>For example, 59.94 fps (a non-integer frame rate) is defined as 60 frames per second, with a 1.001 frame rate divisor.</para>
        /// </summary>
        public int FramesPerSecond
        {
            get { return framesPerSecond; }
        }

        /// <summary>
        /// <para>The number to divide <see cref="FramesPerSecond"/> by, to produce the precise frames rate.</para>
        /// <para>For integer frame rates, the divisor value is 1.</para>
        /// <para>For non-integer frame rates, the divisor value is 1.001.</para>
        /// </summary>
        public double FrameRateDivisor
        {
            get { return frameRateDivisor; }
        }

        /// <summary>
        /// <para>A boolean indicating if the frame rate calls for dropping frames to compensate for the deviation between time code and real time, as defined in SMPTE ST 12-1.</para>
        /// </summary>
        public bool DropFrame
        {
            get { return dropFrame; }
        }

        /// <summary>
        /// <para>The maximum number of frames able to be represented by the frame rate.</para>
        /// </summary>
        public int MaximumFrames
        {
            get { return maximumFrames; }
        }

        #endregion Public Properties

        public override string ToString()
        {
            //Write 24/1.001 as 23.976 (3 digits), all others use two digits of precision
            if (framesPerSecond == 24 && frameRateDivisor == 1.001)
            {
                return String.Format("{0:00.000} fps", framesPerSecond / frameRateDivisor);
            }
            else
            {
                return String.Format("{0:00.00} fps", framesPerSecond / frameRateDivisor);
            }
        }
    }
}
