// <copyright file="TimeCode.cs" company="Middleman Software, Inc.">
//  Middleman Time Code Library - Time Code class
//  Copyright © 2022, Middleman Software, Inc.
//  All rights reserved.
// </copyright>
// <author>James Heliker</author>
using System;

namespace TimeCode
{
    /// <summary>
    /// Represents a SMPTE ST 12 time code.
    /// </summary>
    public class TimeCode
    {
        #region Private Fields

        private int frameCount;
        private FrameRate frameRate;

        #endregion Private Fields

        #region Constructors

        public TimeCode(int hours, int minutes, int seconds, int frames, FrameRate frameRate)
        {
            int frameCount = Convert.ToInt32(((hours * 3600) + (minutes * 60) + seconds) * frameRate.FramesPerSecond + frames);

            if (frameRate.DropFrame)
            {
                //Only frame rates that are a multiple of 30 can drop frames
                if (frameRate.FramesPerSecond % 30 == 0)
                {
                    //Drop 2 frames for every 30 frames per second, i.e. drop 2 @ 30 (29.97), drop 4 @ 60 (59.94), etc.
                    int framesToDrop = (frameRate.FramesPerSecond / 30) * 2;
                    double totalMinutes = frameCount / (frameRate.FramesPerSecond * 60);
                    int dropMinutes = Convert.ToInt32(totalMinutes - Math.Floor(totalMinutes / 10));

                    frameCount -= (framesToDrop * dropMinutes);
                }
            }

            this.frameCount = frameCount;
            this.frameRate = frameRate;
        }

        public TimeCode(int frames, FrameRate frameRate)
        {
            this.frameCount = frames;
            this.frameRate = frameRate;
        }

        #endregion Constructors

        #region Public Properties

        public int FrameCount
        {
            get { return frameCount; }
            set { frameCount = value; }
        }

        #endregion Public Properties

        public static TimeCode operator +(TimeCode tc1, TimeCode tc2)
        {
            bool exceededMaximum = false;
            return tc1.Add(tc2, out exceededMaximum);
        }

        public static TimeCode operator -(TimeCode tc1, TimeCode tc2)
        {
            bool subceededMinimum = false;
            return tc1.Subtract(tc2, out subceededMinimum);
        }

        public TimeCode Add(TimeCode timeCode, out bool exceededMaximum)
        {
            //Check if the operation would exceeed the maximum frames able to be represented at a given frame rate
            bool operationExceeds = false;
            int additionFrameCount = this.frameCount + timeCode.frameCount;
            if (additionFrameCount > this.frameRate.MaximumFrames)
            {
                operationExceeds = true;
                additionFrameCount = additionFrameCount - (this.frameRate.MaximumFrames + 1);
            }

            exceededMaximum = operationExceeds;
            return new TimeCode(additionFrameCount, timeCode.frameRate);
        }

        public TimeCode Subtract(TimeCode timeCode, out bool subceededMinimum)
        {
            //Check if the operation would subceed the minimum frames able to be represented (zero)
            bool operationSubceeds = false;
            int subtractionFrameCount = this.frameCount - timeCode.frameCount;
            if (subtractionFrameCount < 0)
            {
                operationSubceeds = true;
                subtractionFrameCount = subtractionFrameCount + (this.frameRate.MaximumFrames + 1);
            }

            subceededMinimum = operationSubceeds;
            return new TimeCode(subtractionFrameCount, timeCode.frameRate);
        }

        public static TimeCode FromString(string timeCode, FrameRate frameRate)
        {
            string[] split = timeCode.Split(new string[] { ":", ";" }, StringSplitOptions.RemoveEmptyEntries);

            int hours = Convert.ToInt32(split[0]);
            int minutes = Convert.ToInt32(split[1]);
            int seconds = Convert.ToInt32(split[2]);
            int frames = Convert.ToInt32(split[3]);

            return new TimeCode(hours, minutes, seconds, frames, frameRate);
        }

        public static TimeCode FromTimeSpan(TimeSpan timeSpan, FrameRate frameRate)
        {
            return new TimeCode(Convert.ToInt32(timeSpan.TotalMilliseconds / (1000 / (frameRate.FramesPerSecond / frameRate.FrameRateDivisor))), frameRate);
        }

        public TimeSpan ToTimeSpan()
        {
            return TimeSpan.FromMilliseconds(frameCount * (1000 / (frameRate.FramesPerSecond / frameRate.FrameRateDivisor)));
        }

        public override string ToString()
        {
            int tmpFrameCount = frameCount;

            string framesSeparator = ":";
            if (frameRate.DropFrame)
            {
                framesSeparator = ";";

                //Only frame rates that are a multiple of 30 can drop frames
                if (frameRate.FramesPerSecond % 30 == 0)
                {
                    //Drop 2 frames for every 30 frames per second, i.e. drop 2 @ 30 (29.97), drop 4 @ 60 (59.94), etc.
                    int framesToDrop = (frameRate.FramesPerSecond / 30) * 2;

                    //Ensure frame numbers 00 and 01 are dropped at the start of every minute except for "tenth" minutes (00, 10, 20, 30, 40 and 50)
                    double d = Math.Floor((double)tmpFrameCount / (17982 * framesToDrop / 2));

                    int m = tmpFrameCount % (17982 * framesToDrop / 2);

                    if (m < framesToDrop)
                        m = m + framesToDrop;

                    tmpFrameCount += Convert.ToInt32(9 * framesToDrop * d + framesToDrop * Math.Floor((double)(m - framesToDrop) / (1798 * framesToDrop / 2)));
                }
            }

            double hours = Math.Floor((double)tmpFrameCount / (frameRate.FramesPerSecond * 3600)) % 24;
            double minutes = Math.Floor((double)tmpFrameCount / (frameRate.FramesPerSecond * 60)) % 60;
            double seconds = Math.Floor((double)tmpFrameCount / frameRate.FramesPerSecond % 60) % 60;
            double frames = Convert.ToInt32(tmpFrameCount % frameRate.FramesPerSecond) % frameRate.FramesPerSecond;

            return String.Format("{0:00}:{1:00}:{2:00}" + framesSeparator + "{3:00}", hours, minutes, seconds, frames);
        }
    }
}
