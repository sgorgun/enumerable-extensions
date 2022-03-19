using System;
using System.Collections.Generic;
using EnumerableExtensionsTask.Tests.InternalClasses;
using NUnit.Framework;

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("OrderByWithComparer")]
    public class EnumerableExtensionsOrderByWithComparerTests
    {
        private static IEnumerable<TestCaseData> TestCasesDataForOrderByIntKey
        {
            get
            {
                yield return new TestCaseData(
                    new List<int>
                    {
                        44,
                        56,
                        123,
                        456,
                        11,
                        13,
                        154,
                        879,
                        11111,
                    },
                    new List<int>
                    {
                        123,
                        56,
                        154,
                        44,
                        13,
                        11,
                        456,
                        879,
                        11111,
                    },
                    new Func<int, int>(x => x - 100), new IntegerByAbsComparer());
                yield return new TestCaseData(
                    new List<int>
                    {
                        44,
                        56,
                        456,
                        11,
                        13,
                        158,
                        879,
                    },
                    new List<int>
                    {
                        11,
                        13,
                        44,
                        56,
                        456,
                        158,
                        879,
                    },
                    new Func<int, int>(x => x % 10), new IntegerByAbsComparer());
                yield return new TestCaseData(
                    new List<int>
                    {
                        49,
                        25,
                        64,
                        625,
                        100,
                    },
                    new List<int>
                    {
                        25,
                        49,
                        64,
                        100,
                        625,
                    },
                    new Func<int, int>(x => (int)Math.Sqrt(x)),
                    new IntegerByAbsComparer());
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForOrderByExceptionCase
        {
            get
            {
                yield return new TestCaseData(
                    new List<int>
                    {
                        44,
                        56,
                        123,
                        456,
                        11,
                        13,
                        154,
                        879,
                        11111,
                    },
                    null, null);
                yield return new TestCaseData(
                    null,
                    new Func<int, int>(x => x % 10), new IntegerByAbsComparer());
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForOrderByStringKey
        {
            get
            {
                yield return new TestCaseData(
                    new List<int>
                    {
                        49,
                        25,
                        64,
                        625,
                        100,
                    },
                    new List<int>
                    {
                        49,
                        25,
                        64,
                        625,
                        100,
                    },
                    new Func<int, string>(x => x.ToString()),
                    Comparer<string>.Create((x, y) => (x?.Length ?? 0).CompareTo(y?.Length ?? 0)));

                yield return new TestCaseData(
                    new List<int>
                    {
                        49,
                        41,
                        25,
                        64,
                        625,
                        100,
                    },
                    new List<int>
                    {
                        49,
                        25,
                        64,
                        625,
                        100,
                        41,
                    },
                    new Func<int, string>(x => x.ToString()),
                    Comparer<string>.Create((x, y) =>
                        (x?.IndexOf('1', StringComparison.InvariantCulture) ?? -1).CompareTo(
                            y?.IndexOf('1', StringComparison.InvariantCulture) ?? -1)));
            }
        }

        [TestCaseSource(nameof(TestCasesDataForOrderByIntKey))]
        public void OrderBy_IntKey(IEnumerable<int> source, IEnumerable<int> expected, Func<int, int> key,
            IComparer<int> comparer) =>
            CollectionAssert.AreEqual(expected, source.OrderBy<int, int>(key, comparer));

        [TestCaseSource(nameof(TestCasesDataForOrderByStringKey))]
        public void OrderBy_IntKey(IEnumerable<int> source, IEnumerable<int> expected, Func<int, string> key,
            IComparer<string> comparer) =>
            CollectionAssert.AreEqual(expected, source.OrderBy<int, string>(key, comparer));

        [TestCaseSource(nameof(TestCasesDataForOrderByExceptionCase))]
        public void OrderBy_KeyOrSourceIsNull_ThrowArgumentNullException(IEnumerable<int> source, Func<int, int> key,
            IComparer<int> comparer) =>
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(key, comparer));
    }
}
