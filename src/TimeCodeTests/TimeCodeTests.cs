// <copyright file="TimeCodeTests.cs" company="Middleman Software, Inc.">
//  Middleman Time Code Library - Unit Tests
//  Copyright © 2022, Middleman Software, Inc.
//  All rights reserved.
// </copyright>
// <author>James Heliker</author>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Middleman;
using System;

namespace TimeCodeTests
{
    [TestClass()]
    public class TimeCodeTests
    {
        #region 23.976 fps NDF tests

        FrameRate FPS_23_976_NDF = new FrameRate(FrameRates.FPS_23_976_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_23_976_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_23_976_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_23_976_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_23_976_NDF);

            Assert.AreEqual("00:00:01:12/FPS_23_976_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_23_976_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_23_976_NDF);

            Assert.AreEqual("23:59:59:23/FPS_23_976_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_23_976_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_23_976_NDF);

            Assert.AreEqual("00:01:00:01/FPS_23_976_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_23_976_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_23_976_NDF);

            Assert.AreEqual("00:10:00:00/FPS_23_976_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_23_976_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_23_976_NDF);

            Assert.AreEqual("23:59:00:00/FPS_23_976_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_23_976_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_23_976_NDF.MaximumFrames, FPS_23_976_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("1.00:01:26.3580000", ts1.ToString());
        }

        #endregion 23.976 fps NDF tests

        #region 24 fps NDF tests

        FrameRate FPS_24_NDF = new FrameRate(FrameRates.FPS_24_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_24_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_24_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_24_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_24_NDF);

            Assert.AreEqual("00:00:01:12/FPS_24_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_24_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_24_NDF);

            Assert.AreEqual("23:59:59:23/FPS_24_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_24_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_24_NDF);

            Assert.AreEqual("00:01:00:01/FPS_24_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_24_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_24_NDF);

            Assert.AreEqual("00:10:00:00/FPS_24_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_24_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_24_NDF);

            Assert.AreEqual("23:59:00:00/FPS_24_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_24_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_24_NDF.MaximumFrames, FPS_24_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.9580000", ts1.ToString());
        }

        #endregion 24 fps NDF tests

        #region 25 fps NDF tests

        FrameRate FPS_25_NDF = new FrameRate(FrameRates.FPS_25_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_25_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_25_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_25_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_25_NDF);

            Assert.AreEqual("00:00:01:10/FPS_25_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_25_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_25_NDF);

            Assert.AreEqual("23:59:59:24/FPS_25_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_25_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_25_NDF);

            Assert.AreEqual("00:01:00:01/FPS_25_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_25_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_25_NDF);

            Assert.AreEqual("00:09:36:00/FPS_25_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_25_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_25_NDF);

            Assert.AreEqual("23:01:26:10/FPS_25_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_25_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_25_NDF.MaximumFrames, FPS_25_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.9600000", ts1.ToString());
        }

        #endregion 25 fps NDF tests

        #region 29.97 fps DF tests

        FrameRate FPS_29_97_DF = new FrameRate(FrameRates.FPS_29_97_DF);

        [TestMethod()]
        public void SanityCheck_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_DF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_29_97_DF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_29_97_DF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_29_97_DF);

            Assert.AreEqual("00:00:01;00/FPS_29_97_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_DF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_29_97_DF);

            Assert.AreEqual("23:59:59;29/FPS_29_97_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_DF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_29_97_DF);

            Assert.AreEqual("00:00:59;29/FPS_29_97_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_DF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_29_97_DF);

            Assert.AreEqual("00:08:00;16/FPS_29_97_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_DF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_29_97_DF);

            Assert.AreEqual("19:12:21;04/FPS_29_97_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_29_97_DF()
        {
            TimeCode tc1 = new TimeCode(FPS_29_97_DF.MaximumFrames, FPS_29_97_DF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.8800000", ts1.ToString());
        }

        #endregion 29.97 fps DF tests

        #region 29.97 fps NDF tests

        FrameRate FPS_29_97_NDF = new FrameRate(FrameRates.FPS_29_97_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_29_97_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_29_97_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_29_97_NDF);

            Assert.AreEqual("00:00:01:00/FPS_29_97_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_29_97_NDF);

            Assert.AreEqual("23:59:59:29/FPS_29_97_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_29_97_NDF);

            Assert.AreEqual("00:01:00:01/FPS_29_97_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_29_97_NDF);

            Assert.AreEqual("00:08:00:00/FPS_29_97_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_29_97_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_29_97_NDF);

            Assert.AreEqual("19:11:12:00/FPS_29_97_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_29_97_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_29_97_NDF.MaximumFrames, FPS_29_97_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("1.00:01:26.3670000", ts1.ToString());
        }

        #endregion 29.97 fps NDF tests

        #region 30 fps NDF tests

        FrameRate FPS_30_NDF = new FrameRate(FrameRates.FPS_30_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_30_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_30_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_30_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_30_NDF);

            Assert.AreEqual("00:00:01:00/FPS_30_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_30_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_30_NDF);

            Assert.AreEqual("23:59:59:29/FPS_30_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_30_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_30_NDF);

            Assert.AreEqual("00:01:00:01/FPS_30_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_30_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_30_NDF);

            Assert.AreEqual("00:08:00:00/FPS_30_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_30_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_30_NDF);

            Assert.AreEqual("19:11:12:00/FPS_30_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_30_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_30_NDF.MaximumFrames, FPS_30_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.9670000", ts1.ToString());
        }

        #endregion 30 fps NDF tests

        #region 50 fps NDF tests

        FrameRate FPS_50_NDF = new FrameRate(FrameRates.FPS_50_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_50_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_50_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_50_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_50_NDF);

            Assert.AreEqual("00:00:00:10/FPS_50_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_50_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_50_NDF);

            Assert.AreEqual("23:59:59:49/FPS_50_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_50_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_50_NDF);

            Assert.AreEqual("00:01:00:01/FPS_50_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_50_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_50_NDF);

            Assert.AreEqual("00:04:48:00/FPS_50_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_50_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_50_NDF);

            Assert.AreEqual("11:30:43:10/FPS_50_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_50_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_50_NDF.MaximumFrames, FPS_50_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.9800000", ts1.ToString());
        }

        #endregion 50 fps NDF tests

        #region 59.94 fps DF tests

        FrameRate FPS_59_94_DF = new FrameRate(FrameRates.FPS_59_94_DF);

        [TestMethod()]
        public void SanityCheck_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_DF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_59_94_DF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_59_94_DF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_59_94_DF);

            Assert.AreEqual("00:00:00;00/FPS_59_94_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_DF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_59_94_DF);

            Assert.AreEqual("23:59:59;59/FPS_59_94_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_DF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_59_94_DF);

            Assert.AreEqual("00:00:59;57/FPS_59_94_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_DF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_59_94_DF);

            Assert.AreEqual("00:04:00;16/FPS_59_94_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_DF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_59_94_DF);

            Assert.AreEqual("09:36:10;36/FPS_59_94_DF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_59_94_DF()
        {
            TimeCode tc1 = new TimeCode(FPS_59_94_DF.MaximumFrames, FPS_59_94_DF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.8970000", ts1.ToString());
        }

        #endregion 59.94 fps DF tests

        #region 59.94 fps NDF tests

        FrameRate FPS_59_94_NDF = new FrameRate(FrameRates.FPS_59_94_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_59_94_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_59_94_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_59_94_NDF);

            Assert.AreEqual("00:00:00:00/FPS_59_94_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_59_94_NDF);

            Assert.AreEqual("23:59:59:59/FPS_59_94_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_59_94_NDF);

            Assert.AreEqual("00:01:00:01/FPS_59_94_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_59_94_NDF);

            Assert.AreEqual("00:04:00:00/FPS_59_94_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_59_94_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_59_94_NDF);

            Assert.AreEqual("09:35:36:00/FPS_59_94_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_59_94_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_59_94_NDF.MaximumFrames, FPS_59_94_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("1.00:01:26.3830000", ts1.ToString());
        }

        #endregion 59.94 fps NDF tests

        #region 60 fps NDF tests

        FrameRate FPS_60_NDF = new FrameRate(FrameRates.FPS_60_NDF);

        [TestMethod()]
        public void SanityCheck_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_60_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 0, 0, 1, FPS_60_NDF);

            Assert.AreEqual(1, tc2.FrameCount);
        }

        [TestMethod()]
        public void ExceedMaximum_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(23, 59, 59, 0, FPS_60_NDF);
            TimeCode tc2 = tc1 + new TimeCode(60, FPS_60_NDF);

            Assert.AreEqual("00:00:00:00/FPS_60_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SubceedMinimum_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_60_NDF);
            TimeCode tc2 = tc1 - new TimeCode(1, FPS_60_NDF);

            Assert.AreEqual("23:59:59:59/FPS_60_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void DropFrameAlgorithm_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_60_NDF);
            TimeCode tc2 = tc1 + new TimeCode(0, 1, 0, 1, FPS_60_NDF);

            Assert.AreEqual("00:01:00:01/FPS_60_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void SmallExcercise_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_60_NDF);
            TimeCode tc2 = tc1 + new TimeCode(14400, FPS_60_NDF);

            Assert.AreEqual("00:04:00:00/FPS_60_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void LargeExcercise_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(0, 0, 0, 0, FPS_60_NDF);
            TimeCode tc2 = tc1 + new TimeCode(2072160, FPS_60_NDF);

            Assert.AreEqual("09:35:36:00/FPS_60_NDF", tc2.ToString(false));
        }

        [TestMethod()]
        public void RealTimeConversion_FPS_60_NDF()
        {
            TimeCode tc1 = new TimeCode(FPS_60_NDF.MaximumFrames, FPS_60_NDF);
            TimeSpan ts1 = tc1.ToTimeSpan();

            Assert.AreEqual("23:59:59.9830000", ts1.ToString());
        }

        #endregion 60 fps NDF tests
    }
}