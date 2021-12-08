using System;
using System.Linq;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    public class BufferDataTests
    {
        [TestCase(4)]
        [TestCase(16)]
        [TestCase(256)]
        [TestCase(512)]
        public void ToArray_OnBaseEnumerable_(int count)
        {
            var actual = Enumerable.Range(1, count).ToArray();

            var (expected, length) = BufferData.ToArray(Enumerable.Range(1, count));

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(count, length);
        }

        [TestCase(4_000)]
        [TestCase(10_000)]
        public void ToArray_OnBaseEnumerable(int count)
        {
            var actual = Enumerable.Range(1, count).ToArray();

            var (expected, length) = BufferData.ToArray(Enumerable.Range(1, count));

            Assert.AreNotEqual(expected, actual);
            Assert.AreEqual(count, length);
        }

        [TestCase(4)]
        [TestCase(16)]
        [TestCase(256)]
        [TestCase(512)]
        [TestCase(4_000)]
        [TestCase(10_000)]
        public void ToArray_OnBaseCollection(int count)
        {
            var actual = Enumerable.Range(1, count).ToArray();

            var (expected, length) = BufferData.ToArray(actual);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(count, length);
        }

        [Test]
        public void ToArray_ReturnEmptyArray()
        {
            var (expected, count) = BufferData.ToArray(Enumerable.Repeat(12, 0));

            Assert.AreEqual(expected, Array.Empty<int>());
            Assert.AreEqual(0, count);
        }
    }
}
