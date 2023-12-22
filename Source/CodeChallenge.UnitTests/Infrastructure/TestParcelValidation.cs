using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PageUp.CodeChallenge.Model;
using PageUp.CodeChallenge.Contracts;
using PageUp.CodeChallenge.Infrastructure;

namespace PageUp.CodeChallenge.UnitTests.Model
{
    [TestClass]
    public class TestParcelValidation
    {
        /// <summary>
        /// Parcel validator
        /// </summary>
        private IValidator<Parcel, ParcelTag> _parcelValidator = new ParcelValidator();

        [TestMethod]
        public void MediumParcelTest()
        {
            var parcel = new Parcel(new Weight(10), new Volume(20, 5, 20));
            var parcelTag = _parcelValidator.Validate(parcel);

            Assert.AreEqual(parcelTag.DeliveryCost.Value, 80);
            Assert.AreEqual(parcelTag.Category, ParcelCategory.MediumParcel);
        }

        [TestMethod]
        public void HeavyParcelTest()
        {
            var parcel = new Parcel(new Weight(22), new Volume(5, 5, 5));
            var parcelTag = _parcelValidator.Validate(parcel);

            Assert.AreEqual(parcelTag.DeliveryCost.Value, 330);
            Assert.AreEqual(parcelTag.Category, ParcelCategory.HeavyParcel);
        }

        [TestMethod]
        public void SmallParcelTest()
        {
            var parcel = new Parcel(new Weight(2), new Volume(3, 10, 12));
            var parcelTag = _parcelValidator.Validate(parcel);

            Assert.AreEqual(parcelTag.DeliveryCost.Value, 18);
            Assert.AreEqual(parcelTag.Category, ParcelCategory.SmallParcel);
        }

        [TestMethod]
        public void RejectedParcelTest()
        {
            var parcel = new Parcel(new Weight(110), new Volume(20, 55, 120));
            var parcelTag = _parcelValidator.Validate(parcel);

            Assert.AreEqual(parcelTag.DeliveryCost, NullMoney.Instance);
            Assert.AreEqual(parcelTag.Category, ParcelCategory.RejectedParcel);
        }

        [TestMethod]
        public void LargeParcelTest()
        {
            var parcel = new Parcel(new Weight(3), new Volume(10, 20, 20));
            var parcelTag = _parcelValidator.Validate(parcel);

            Assert.AreEqual(parcelTag.DeliveryCost.Value, 120);
            Assert.AreEqual(parcelTag.Category, ParcelCategory.LargeParcel);
        }
    }
}