using System;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Orderings")]
    public class EnumerableExtensionsOrderByTestFixture
    {
        private static IEnumerable<TestCaseData> TestCasesDataForStrings
        {
            get
            {
                yield return new TestCaseData(
                    new List<string>
                    {
                        "one",
                        "two",
                        "three",
                        "four",
                        null,
                        "five",
                        "six",
                        "seven",
                        "eight",
                        null,
                        "nine",
                        "ten",
                    },
                    new List<string>
                    {
                        null,
                        null,
                        "one",
                        "two",
                        "six",
                        "ten",
                        "four",
                        "five",
                        "nine",
                        "three",
                        "seven",
                        "eight",
                    },
                    new Func<string, int?>(item => item?.Length));
                yield return new TestCaseData(
                    new List<string>
                    {
                        "one",
                        "two",
                        "three",
                        "four",
                        null,
                        "five",
                        "six",
                        "seven",
                        "eight",
                        null,
                        "nine",
                        "ten",
                    },
                    new List<string>
                    {
                        null,
                        null,
                        "two",
                        "four",
                        "six",
                        "eight",
                        "seven",
                        "ten",
                        "one",
                        "three",
                        "five",
                        "nine",
                    },
                    new Func<string, int?>(item => item?.IndexOf('e', StringComparison.InvariantCulture)));
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForDoubles
        {
            get
            {
                yield return new TestCaseData(
                    new List<double>
                    {
                        -9.56,
                        67.908,
                        45.34,
                        0.123,
                        -100.453,
                    },
                    new List<double>
                    {
                        0.123,
                        -9.56,
                        45.34,
                        67.908,
                        -100.453,
                    },
                    new Func<double, double>(Math.Abs));
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForIntegers
        {
            get
            {
                yield return new TestCaseData(
                    new List<int>
                    {
                        123,
                        21,
                        543,
                        75,
                        34,
                        77777,
                        1235,
                    },
                    new List<int>
                    {
                        21,
                        123,
                        543,
                        34,
                        75,
                        1235,
                        77777,
                    },
                    new Func<int, int>(item => item % 10));
            }
        }

        [TestCaseSource(nameof(TestCasesDataForStrings))]
        public void OrderBy_Strings(IEnumerable<string> source, IEnumerable<string> expected, Func<string, int?> key) =>
            CollectionAssert.AreEqual(expected, source.OrderBy(key));

        [TestCaseSource(nameof(TestCasesDataForDoubles))]
        public void OrderBy_Doubles(IEnumerable<double> source, IEnumerable<double> expected,
            Func<double, double> key) =>
            CollectionAssert.AreEqual(expected, source.OrderBy(key));

        [TestCaseSource(nameof(TestCasesDataForIntegers))]
        public void OrderBy_Integers(IEnumerable<int> source, IEnumerable<int> expected, Func<int, int> key) =>
            CollectionAssert.AreEqual(expected, source.OrderBy(key));

        [Test]
        public void OrderBy_After_Add_New_Element_To_Source_Sequence_Actual_Result()
        {
            List<int> source = new List<int>
            {
                1, 2, 3, 4,
            };
            List<int> expected = new List<int>
            {
                5,
                4,
                3,
                2,
                1,
            };

            var actual = source.OrderBy(item => -item);
            source.Add(5);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderBy_After_Remove_Element_From_Source_Sequence_Actual_Result()
        {
            List<int> source = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
            };
            List<int> expected = new List<int>
            {
                4, 3, 2, 1,
            };

            var actual = source.OrderBy(item => -item);
            source.Remove(5);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderBy_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).OrderBy(x => x),
                $"Source can not be null.");
        }
    }
}
