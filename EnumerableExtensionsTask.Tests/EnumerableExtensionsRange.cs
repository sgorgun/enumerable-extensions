using System;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Range")]
    public class EnumerableExtensionsRange
    {
        [TestCase(0, 5, ExpectedResult = new[] { 0, 1, 2, 3, 4 })]
        [TestCase(5, 3, ExpectedResult = new[] { 5, 6, 7 })]
        [TestCase(10, 2, ExpectedResult = new[] { 10, 11 })]
        public IEnumerable<int> RangeTests(int start, int count) => EnumerableExtensions.Range(start, count);

        [Test]
        public void Range_Source_Is_Null_Throw_ArgumentOutOfRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => EnumerableExtensions.Range(5, -5));
    }
}
