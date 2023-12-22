using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageUp.CodeChallenge.Model;

namespace PageUp.CodeChallenge.UnitTests.Model
{
    [TestClass]
    public class TestParcel
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidParcelTest()
        {
            var parcel = new Parcel(new Weight(-10), new Volume(2,3,4));
        }

        [TestMethod]
        public void ValidParcelTest()
        {
            var parcel = new Parcel(new Weight(10), new Volume(2, 3, 4));

            Assert.AreEqual(parcel.Weight.Value, 10);
            Assert.AreEqual(parcel.Weight.Unit, MeasurementUnit.KG);

            Assert.AreEqual(parcel.Volume.Value, 24);
            Assert.AreEqual(parcel.Volume.Unit, MeasurementUnit.CUBIC_CM);

            Assert.AreEqual(parcel.Volume.Height.Value, 2);
            Assert.AreEqual(parcel.Volume.Height.Unit, MeasurementUnit.CM);

            Assert.AreEqual(parcel.Volume.Width.Value, 3);
            Assert.AreEqual(parcel.Volume.Width.Unit, MeasurementUnit.CM);

            Assert.AreEqual(parcel.Volume.Depth.Value, 4);
            Assert.AreEqual(parcel.Volume.Depth.Unit, MeasurementUnit.CM);
        }
    }
}
