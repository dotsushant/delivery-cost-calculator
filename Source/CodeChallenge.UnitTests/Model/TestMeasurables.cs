using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageUp.CodeChallenge.Model;

namespace PageUp.CodeChallenge.UnitTests.Model
{
    [TestClass]
    public class TestMeasurables
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMeasurableTest()
        {
            var measurable = new Measurable(MeasurementUnit.CM, -1.0);
        }

        [TestMethod]
        public void ValidMeasurableTest()
        {
            var measurable = new Measurable(MeasurementUnit.CUBIC_CM, 1500);
            Assert.AreEqual(measurable.Value, 1500);
            Assert.AreEqual(measurable.Unit, MeasurementUnit.CUBIC_CM);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidWeightTest()
        {
            var weight = new Weight(-100);
        }

        [TestMethod]
        public void ValidWeightTest()
        {
            var weight = new Weight(100);
            Assert.AreEqual(weight.Value, 100);
            Assert.AreEqual(weight.Unit, MeasurementUnit.KG);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidDimensionTest()
        {
            var dimension = new Dimension(-100);
        }

        [TestMethod]
        public void ValidDimensionTest()
        {
            var dimension = new Dimension(100);
            Assert.AreEqual(dimension.Value, 100);
            Assert.AreEqual(dimension.Unit, MeasurementUnit.CM);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidVolumeTest()
        {
            var volume = new Volume(-1, -1, 1);

        }

        [TestMethod]
        public void ValidVolumeTest()
        {
            var volume = new Volume(2,3,4);
            Assert.AreEqual(volume.Value, 24);
            Assert.AreEqual(volume.Unit, MeasurementUnit.CUBIC_CM);

            Assert.AreEqual(volume.Height.Value, 2);
            Assert.AreEqual(volume.Height.Unit, MeasurementUnit.CM);

            Assert.AreEqual(volume.Width.Value, 3);
            Assert.AreEqual(volume.Width.Unit, MeasurementUnit.CM);

            Assert.AreEqual(volume.Depth.Value, 4);
            Assert.AreEqual(volume.Depth.Unit, MeasurementUnit.CM);
        }
    }
}
