using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace EnumerableExtensionsTask.Tests.TestFixtures
{
    [TestFixture(
        new object[] { 12, 3, 4, 1356, 17, 901, },
        new int[] { 12, 3, 4, 1356, 17, 901, },
        123,
        TypeArgs = new Type[] { typeof(int) })]
    [TestFixture(
        new object[] { "12", "2", "6", "Hello", "Hi" },
        new string[] { "12", "2", "6", "Hello", "Hi", },
        "hello",
        TypeArgs = new Type[] { typeof(string) })]
    [TestFixture(
        new object[] { 12.1, -123.543, 3.4, 13.56, 17.901, },
        new double[] { 12.1, -123.543, 3.4, 13.56, 17.901, },
        123.89,
        TypeArgs = new Type[] { typeof(double) })]
    [TestFixture(
        new object[] { false, true, false, false, true, },
        new bool[] { false, true, false, false, true, },
        true,
        TypeArgs = new Type[] { typeof(bool) })]
    [TestFixture(
        new object[] { 's', 'T', '\n', '1', '\a' },
        new char[] { 's', 'T', '\n', '1', '\a', },
        'A',
        TypeArgs = new Type[] { typeof(char) })]
    [Category("OfType")]
    internal class EnumerableExtensionsCastFixture<T>
    {
        private readonly List<T> expected;
        private readonly ArrayList source;
        private readonly T item;

        public EnumerableExtensionsCastFixture(ICollection source, IEnumerable<T> expected, T item)
        {
            this.expected = new List<T>(expected);
            this.source = new ArrayList(source);
            this.item = item;
        }

        [Test]
        [Order(1)]
        public void TypeOf_With_Initial_Sequence()
        {
            CollectionAssert.AreEqual(this.expected, this.source.Cast<T>());
        }

        [Test]
        [Order(2)]
        public void TypeOf_Add_New_Element_To_Source_Sequence_Lazy_Evaluation()
        {
            this.expected.Add(this.item);

            var actual = this.source.Cast<T>();
            this.source.Add(this.item);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(3)]
        public void TypeOf_Remove_Element_From_Source_Sequence_Lazy_Evaluation()
        {
            this.source.Remove(this.item);

            var actual = this.source.Cast<T>();
            this.expected.Remove(this.item);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(0)]
        public void TypeOf_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<T>)null!).Cast<T>(), $"Source can not be null.");
        }
    }
}
