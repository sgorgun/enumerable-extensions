using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Cast")]
    public class EnumerableExtensionsCastTests
    {
        [Test]
        public void CastToInt_Throw_InvalidCastException()
        {
            var list = new object[] { 1, 4, 5, 6, 7, 8, 9, 123, 45, 12, "Hello", true };
            Assert.Throws<InvalidCastException>(() => list.Cast<int>().ToArray());
        }

        [Test]
        public void CastToString_Throw_InvalidCastException()
        {
            var list = new object[] { "Hello", "true", 12, 12.67, true };
            Assert.Throws<InvalidCastException>(() => list.Cast<string>().ToArray());
        }
    }
}
