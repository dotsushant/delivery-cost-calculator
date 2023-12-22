using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageUp.CodeChallenge.Model;

namespace PageUp.CodeChallenge.UnitTests.Model
{
    [TestClass]
    public class TestMoney
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMoneyTest()
        {
            var money = new Money(-100);
        }

        [TestMethod]
        public void ValidMoneyTest()
        {
            var money = new Money(200);
            Assert.AreEqual(money.Value, 200);
        }

        [TestMethod]
        public void NullMoneySingletonTest()
        {
            var nullMoney1 = NullMoney.Instance;
            var nullMoney2 = NullMoney.Instance;
            Assert.AreEqual(nullMoney1, nullMoney2);
            Assert.AreEqual(nullMoney1.Value, nullMoney2.Value);
        }
    }
}
