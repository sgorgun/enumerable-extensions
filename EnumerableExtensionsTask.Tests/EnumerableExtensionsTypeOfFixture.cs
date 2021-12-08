using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707
#pragma warning disable CA1812

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture(
        new object[] { 12, 3, 4, true, "12", "2", 13.56, "6", null, 17.901, false },
        new int[] { 12, 3, 4 },
        123,
        TypeArgs = new Type[] { typeof(int) })]
    [TestFixture(
        new object[] { 12, null, 3, 4, true, "12", "2", 13.56, "6", 17.901, false },
        new string[] { "12", "2", "6" },
        "hello",
        TypeArgs = new Type[] { typeof(string) })]
    [TestFixture(
        new object[] { 12, -123.543, 3, null, 4, true, "12", "2", 13.56, "6", 17.901, false },
        new double[] { -123.543, 13.56, 17.901 },
        123.89,
        TypeArgs = new Type[] { typeof(double) })]
    [TestFixture(
        new object[] { -123.543, 12, 3, 4, true, "12", "2", null, 13.56, "6", 17.901, false },
        new bool[] { true, false },
        true,
        TypeArgs = new Type[] { typeof(bool) })]
    [TestFixture(
        new object[] { 's', -123.543, '\n', 12, 3, 4, true, "12", "2", null, 13.56, "6", 17.901, false },
        new char[] { 's', '\n' },
        'A',
        TypeArgs = new Type[] { typeof(char) })]
    [Category("OfType")]
    internal class EnumerableExtensionsTypeOfFixture<T>
    {
        private readonly List<T> expected;
        private readonly ArrayList source;
        private readonly T item;

        public EnumerableExtensionsTypeOfFixture(ICollection source, IEnumerable<T> expected, T item)
        {
            this.expected = new List<T>(expected);
            this.source = new ArrayList(source);
            this.item = item;
        }

        [Test]
        [Order(1)]
        public void TypeOf_With_Initial_Sequence()
        {
            CollectionAssert.AreEqual(this.expected, this.source.OfType<T>());
        }

        [Test]
        [Order(2)]
        public void TypeOf_Add_New_Element_To_Source_Sequence_Lazy_Evaluation()
        {
            this.expected.Add(this.item);

            var actual = this.source.OfType<T>();
            this.source.Add(this.item);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(3)]
        public void TypeOf_Remove_Element_From_Source_Sequence_Lazy_Evaluation()
        {
            this.source.Remove(this.item);

            var actual = this.source.OfType<T>();
            this.expected.Remove(this.item);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(0)]
        public void TypeOf_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<T>)null).OfType<T>(), $"Source can not be null.");
        }
    }
}
