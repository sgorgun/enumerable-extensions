using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Where")]
    public class EnumerableExtensionsWhereTests
    {
        private static IEnumerable<TestCaseData> TestCasesSource
        {
            get
            {
                yield return new TestCaseData(
                    new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" },
                    new List<string> { "one", "two", "six", "ten" },
                    new Func<string, bool>(x => x.Length == 3));
                yield return new TestCaseData(
                    new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", },
                    new List<string> { "one", "two", "four", },
                    new Func<string, bool>(x => x.Contains('o', StringComparison.InvariantCulture)));
                yield return new TestCaseData(
                    new List<string>
                    {
                        "one", "two", "Two", "Three", "three", "four", "five", "six", "seven", "eight", "nine",
                        "ten",
                    },
                    new List<string> { "two", "Two", "Three", "three", "ten", },
                    new Func<string, bool>(x => x.ToUpper(CultureInfo.InvariantCulture).StartsWith('T')));
            }
        }

        [TestCase(
            new[] { 12, 56, -907, 567, 234, -576, -43253, 1234, },
            new[] { 12, 56, 567, 234, 1234, })]
        public void Where_Return_Only_Positive_Numbers(IEnumerable<int> source, IEnumerable<int> expected) 
            => Assert.AreEqual(expected, source.Where(x => x > 0));

        [TestCaseSource(nameof(TestCasesSource))]
        public void Where_With_String_Sequence(IEnumerable<string> source, IEnumerable<string> expected, Func<string, bool> predicate) =>
            CollectionAssert.AreEqual(expected, source.Where(predicate));
        
        [Test]
        public void Where_After_Add_New_Element_To_Source_Sequence_Actual_Result()
        {
            List<int> source = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9,
            };
            List<int> expected = new List<int>
            {
                6, 7, 8, 9, 10,
            };

            var actual = source.Where(item => item > 5);
            source.Add(10);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_After_Remove_Element_From_Source_Sequence_Actual_Result()
        {
            List<int> source = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            };
            List<int> expected = new List<int>
            {
                6, 7, 8, 9,
            };

            var actual = source.Where(item => item > 5);
            source.Remove(10);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
