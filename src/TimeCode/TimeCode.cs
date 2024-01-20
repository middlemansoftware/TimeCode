// <copyright file="TimeCode.cs" company="Middleman Software, Inc.">
//  Middleman Time Code Library - Time Code class
//  Copyright © 2022, Middleman Software, Inc.
//  All rights reserved.
// </copyright>
// <author>James Heliker</author>
using Newtonsoft.Json;
using System;

namespace Middleman
{
    /// <summary>
    /// Represents a SMPTE ST 12 time code.
    /// </summary>
    [Serializable]
    public class TimeCode : IComparable, IComparable<TimeCode>, IEquatable<TimeCode>
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

        [JsonConstructor]
        public TimeCode(int frameCount, FrameRate frameRate)
        {
            this.frameCount = frameCount;
            this.frameRate = frameRate;
        }

        #endregion Constructors

        #region Public Properties

        public int FrameCount
        {
            get { return frameCount; }
            set { frameCount = value; }
        }

        public FrameRate FrameRate
        {
            get { return frameRate; }
        }

        public int HoursSegment
        {
            get
            {
                string tc = this.ToString();
                string hours = tc.Substring(0, 2);
                return Convert.ToInt32(hours);
            }
        }

        public int MinutesSegment
        {
            get
            {
                string tc = this.ToString();
                string minutes = tc.Substring(3, 2);
                return Convert.ToInt32(minutes);
            }
        }

        public int SecondsSegment
        {
            get
            {
                string tc = this.ToString();
                string seconds = tc.Substring(6, 2);
                return Convert.ToInt32(seconds);
            }
        }

        public int FramesSegment
        {
            get
            {
                string tc = this.ToString();
                string frames = tc.Substring(9, 2);
                return Convert.ToInt32(frames);
            }
        }

        public double TotalSeconds
        {
            get
            {
                return this.ToTimeSpan().TotalSeconds;
            }
        }

        #endregion Public Properties

        #region Operator Overloads

        /// <summary>
        /// <para>Adds two specified TimeCode instances.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>A TimeCode whose value is the sum of the values of tc1 and tc2.</returns>
        public static TimeCode operator +(TimeCode tc1, TimeCode tc2)
        {
            bool exceededMaximum = false;
            return tc1.Add(tc2, out exceededMaximum);
        }

        /// <summary>
        /// <para>Subtracts a specified TimeCode from another specified TimeCode.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>A TimeCode whose value is the result of the value of tc1 minus the value of tc2.</returns>
        public static TimeCode operator -(TimeCode tc1, TimeCode tc2)
        {
            bool subceededMinimum = false;
            return tc1.Subtract(tc2, out subceededMinimum);
        }

        /// <summary>
        /// <para>Indicates whether a specified TimeCode is less than another specified TimeCode.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns> True if the value of tc1 is less than the value of tc2; otherwise, false.</returns>
        public static bool operator <(TimeCode tc1, TimeCode tc2)
        {
            if (tc1.FrameCount < tc2.FrameCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// <para>Indicates whether a specified TimeCode is greater than another specified TimeCode.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>true if the value of tc1 is greater than the value of tc2; otherwise, false.</returns>
        public static bool operator >(TimeCode tc1, TimeCode tc2)
        {
            if (tc1.FrameCount > tc2.FrameCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// <para>Indicates whether two TimeCode instances are equal.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>true if the values of tc1 and tc2 are equal; otherwise, false.</returns>
        public static bool operator ==(TimeCode tc1, TimeCode tc2)
        {
            Object t1 = tc1 as Object;
            Object t2 = tc2 as Object;

            if (t1 != null && t2 != null)
            {
                return tc1.FrameCount == tc2.FrameCount;
            }
            else
            {
                return t1 == t2;
            }
        }

        /// <summary>
        /// <para>Indicates whether two TimeCode instances are not equal.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>true if the values of tc1 and tc2 are not equal; otherwise, false.</returns>
        public static bool operator !=(TimeCode tc1, TimeCode tc2)
        {
            //No null check required as it is handled in == operator overload above
            return !(tc1 == tc2);
        }

        /// <summary>
        /// <para>Indicates whether a specified TimeCode is less than or equal to another specified TimeCode.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>True if the value of tc1 is less than or equal to the value of tc2; otherwise, false.</returns>
        public static bool operator <=(TimeCode tc1, TimeCode tc2)
        {
            Object t1 = tc1 as Object;
            Object t2 = tc2 as Object;

            if (t1 != null && t2 != null)
            {
                if ((tc1.FrameCount < tc2.FrameCount) || (tc1 == tc2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <para>Indicates whether a specified TimeCode is greater than or equal to another specified TimeCode.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>True if the value of tc1 is greater than or equal to the value of tc2; otherwise, false.</returns>
        public static bool operator >=(TimeCode tc1, TimeCode tc2)
        {
            Object t1 = tc1 as Object;
            Object t2 = tc2 as Object;

            if (t1 != null && t2 != null)
            {
                if ((tc1.FrameCount > tc2.FrameCount) || (tc1 == tc2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <para>Compares two TimeCode values and returns an integer that indicates their relationship.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>Value Condition -1 tc1 is less than tc2, 0 tc1 is equal to tc2, 1 tc1 is greater than tc2.</returns>
        public static int Compare(TimeCode tc1, TimeCode tc2)
        {
            if (tc1 < tc2)
            {
                return -1;
            }

            if (tc1 == tc2)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// <para>Compares this instance to a specified object and returns an indication of their relative values.</para>
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <exception cref="System.ArgumentException">Object value is not a TimeCode.</exception>
        public int CompareTo(object obj)
        {
            if (!(obj is TimeCode))
            {
                throw new ArgumentException("Object is not a TimeCode.");
            }

            TimeCode tc1 = (TimeCode)obj;

            if (this < tc1)
            {
                return -1;
            }

            if (this == tc1)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// <para>Compares this instance to a specified TimeCode object and returns an indication of their relative values.</para>
        /// </summary>
        /// <param name="other">The TimeCode object to compare to this instance.</param>
        public int CompareTo(TimeCode other)
        {
            if (this < other)
            {
                return -1;
            }

            if (this == other)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// <para>Returns a value indicating whether two specified instances of TimeCode are equal.</para>
        /// </summary>
        /// <param name="tc1">The first TimeCode.</param>
        /// <param name="tc2">The second TimeCode.</param>
        /// <returns>true if the values of tc1 and tc2 are equal; otherwise, false.</returns>
        public static bool Equals(TimeCode tc1, TimeCode tc2)
        {
            if (tc1 == tc2)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj.GetType() == typeof(TimeCode) &&
                    this == (TimeCode)obj)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Equals(TimeCode tc2)
        {
            if (this == tc2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

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

        public TimeCode Add(TimeCode timeCode)
        {
            bool exceededMaximum = false;
            return Add(timeCode, out exceededMaximum);
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

        public TimeCode Subtract(TimeCode timeCode)
        {
            bool subceededMinimum = false;
            return Subtract(timeCode, out subceededMinimum);
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

        public static TimeCode FromDateTime(DateTime dateTime, FrameRate frameRate)
        {
            return TimeCode.FromTimeSpan(dateTime.TimeOfDay, frameRate);
        }

        public DateTime ToDateTime(int year = 1, int month = 1, int day = 1, bool timeCodeIsUTC = false)
        {
            TimeSpan tmp = this.ToTimeSpan();

            if (timeCodeIsUTC)
            {
                return new DateTime(year, month, day, tmp.Hours, tmp.Minutes, tmp.Seconds, tmp.Milliseconds, DateTimeKind.Utc);
            }
            else
            {
                return new DateTime(year, month, day, tmp.Hours, tmp.Minutes, tmp.Seconds, tmp.Milliseconds, DateTimeKind.Local);
            }
        }

        public static TimeCode Parse(string timeCodeString)
        {
            TimeCode toReturn = null;

            string[] tmpArr = timeCodeString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (tmpArr.Length == 2)
            {
                FrameRate tmpFr;
                if (FrameRate.TryParse(tmpArr[1], out tmpFr))
                {
                    toReturn = TimeCode.FromString(tmpArr[0], tmpFr);
                }
                else
                {
                    throw new ArgumentException("The provided string did not include frame rate information.");
                }
            }
            else
            {
                throw new ArgumentException("The provided string cannot be parsed as a time code.");
            }

            return toReturn;
        }

        public static bool TryParse(string timeCodeString, out TimeCode timeCode)
        {
            bool toReturn = false;

            try
            {
                timeCode = TimeCode.Parse(timeCodeString);
                toReturn = true;
            }
            catch (ArgumentException argEx)
            {
                timeCode = null;
            }

            return toReturn;
        }

        public override string ToString()
        {
            return ToString();
        }

        public string ToString(bool friendlyFormat = true)
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

            string toReturn = String.Format("{0:00}:{1:00}:{2:00}" + framesSeparator + "{3:00}", hours, minutes, seconds, frames);

            if (friendlyFormat)
            {
                return toReturn;
            }
            else
            {
                //Not-friendly format mirrors what is needed for the TimeCode.Parse method, including the associated frame rate string
                return toReturn += "/" + frameRate.ToString(false);
            }
        }
    }
}
