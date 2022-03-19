using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace EnumerableExtensionsTask.Tests
{
    [Category("Select")]
    public class EnumerableExtensionsSelectTests
    {
        private static IEnumerable<TestCaseData> TestCasesSource
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
                        "five",
                        "six",
                        "seven",
                        "eight",
                        "nine",
                        "ten",
                    },
                    new List<string>
                    {
                        " one ",
                        " two ",
                        " three ",
                        " four ",
                        " five ",
                        " six ",
                        " seven ",
                        " eight ",
                        " nine ",
                        " ten ",
                    },
                    new Func<string, string>(x => $" {x} "));
                yield return new TestCaseData(
                    new List<string>
                    {
                        "one",
                        "two",
                        null!,
                        "three",
                        "four",
                        null!,
                        "five",
                        "six",
                        "seven",
                        "eight",
                        "nine",
                        "ten",
                    },
                    new List<string>
                    {
                        "ONE",
                        "TWO",
                        null!,
                        "THREE",
                        "FOUR",
                        null!,
                        "FIVE",
                        "SIX",
                        "SEVEN",
                        "EIGHT",
                        "NINE",
                        "TEN",
                    },
                    new Func<string, string?>(x => x?.ToUpperInvariant()));
                yield return new TestCaseData(
                    new List<string>
                    {
                        "one  ",
                        "two ",
                        "Two  ",
                        "Three ",
                        "three   ",
                        "four  ",
                        "five ",
                        "six",
                        "seven",
                        "eight",
                        "nine",
                        "ten  ",
                    },
                    new List<string>
                    {
                        "one",
                        "two",
                        "Two",
                        "Three",
                        "three",
                        "four",
                        "five",
                        "six",
                        "seven",
                        "eight",
                        "nine",
                        "ten",
                    },
                    new Func<string, string>(x => x.Trim()));
            }
        }

        [TestCaseSource(nameof(TestCasesSource))]
        public void Select_With_String_Sequence(IEnumerable<string> source, IEnumerable<string> expected, Func<string, string> selector) =>
            CollectionAssert.AreEqual(expected, source.Select(selector));

        [Test]
        public void Where_After_Add_New_Element_To_Source_Sequence_Actual_Result()
        {
            List<int> source = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
            };
            List<string> expected = new List<string>
            {
                "(1)",
                "(2)",
                "(3)",
                "(4)",
                "(5)",
                "(6)",
                "(7)",
                "(8)",
                "(9)",
                "(10)",
            };

            var actual = source.Select(item => $"({item})");
            source.Add(10);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_After_Remove_Element_From_Source_Sequence_Actual_Result()
        {
            List<int> source = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
            };
            List<string> expected = new List<string>
            {
                "(1)",
                "(2)",
                "(3)",
                "(4)",
                "(5)",
                "(6)",
                "(7)",
                "(8)",
                "(9)",
            };

            var actual = source.Select(item => $"({item})");
            source.Remove(10);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => ((IEnumerable<int>)null!).Select(x => x.ToString(CultureInfo.InvariantCulture)),
                $"Source can not be null.");
        }
    }
}
