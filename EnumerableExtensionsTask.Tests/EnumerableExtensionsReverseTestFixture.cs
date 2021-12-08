using System;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture(
        new[] { "Beg", null, "Life", "I", "i", "I", null, "To" },
        new[] { "To", null, "I", "i", "I", "Life", null, "Beg" },
        TypeArgs = new Type[] { typeof(string) })]
    [TestFixture(
        new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
        new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
        TypeArgs = new Type[] { typeof(int) })]
    [TestFixture(
        new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' },
        new[] { '9', '8', '7', '6', '5', '4', '3', '2', '1', '0' },
        TypeArgs = new Type[] { typeof(char) })]
    [TestFixture(
        new[] { true, false, false, true, false },
        new[] { false, true, false, false, true },
        TypeArgs = new Type[] { typeof(bool) })]
    [Category("Reverse")]
    public class EnumerableExtensionsReverseTestFixture<T>
    {
        private readonly List<T> source;
        private readonly List<T> expected;

        public EnumerableExtensionsReverseTestFixture(IEnumerable<T> source, IEnumerable<T> expected)
        {
            this.expected = new List<T>(expected);
            this.source = new List<T>(source);
        }

        [Test]
        [Order(1)]
        public void Reverse_With_Initial_Sequence()
        {
            CollectionAssert.AreEqual(this.expected, EnumerableExtensions.Reverse(this.source));
        }

        [Test]
        [Order(2)]
        public void Reverse_After_Add_New_Element_To_Source_Sequence_Actual_Result()
        {
            this.expected.Insert(0, default);

            var actual = EnumerableExtensions.Reverse(this.source);
            this.source.Add(default);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(3)]
        public void Reverse_After_Remove_Element_From_Source_Sequence_Actual_Result()
        {
            this.expected.RemoveAt(0);

            var actual = EnumerableExtensions.Reverse(this.source);
            this.source.RemoveAt(this.source.Count - 1);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(0)]
        public void Reverse_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<T>)null).Reverse(), $"Source can not be null.");
        }
    }
}
