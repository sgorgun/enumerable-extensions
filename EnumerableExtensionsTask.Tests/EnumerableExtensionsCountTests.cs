using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Count")]
    public class EnumerableExtensionsCountTests
    {
         private static IEnumerable<TestCaseData> TestCasesDataForCountValueType
         {
            get
            {
                yield return new TestCaseData(new int[] { 1, 4, 5, 6, 7, 8, 9, -123, 45, 12 }, 10);
                yield return new TestCaseData(Array.Empty<int>(), 0);
            }
         }

         private static IEnumerable<TestCaseData> TestCasesDataForCountReferenceType
        {
            get
            {
                yield return new TestCaseData(new string[] { "abc", "vdf", "lalalal", null! }, 4);
                yield return new TestCaseData(new object[] { "abc", 2, 3.3d, null! }, 4);
            }
        }

         private static IEnumerable<TestCaseData> TestCasesDataForCountWithPredicateValueType
        {
            get
            {
                yield return new TestCaseData(new int[] { 1, 4, 5, 6, 7, 8, 9, -123, 45, 12 }, 9, new Func<int, bool>((x) => x > 0));
                yield return new TestCaseData(Array.Empty<int>(), 0, new Func<int, bool>((x) => x == 0));
            }
        }

         private static IEnumerable<TestCaseData> TestCasesDataForCountWithPredicateReferenceType
        {
            get
            {
                yield return new TestCaseData(new string[] { "abc", "vdf", "lalalal", null! }, 1, new Func<object, bool>((x) => x is null));
                yield return new TestCaseData(new object[] { "abc", 2, 3.3d, null! }, 1, new Func<object, bool>((x) => x is int));
            }
        }

         [TestCaseSource(nameof(TestCasesDataForCountValueType))]
         public void Count_ValueType(IEnumerable<int> source, int expected) =>
            Assert.AreEqual(expected, source.Count());

         [TestCaseSource(nameof(TestCasesDataForCountReferenceType))]
         public void Count_ReferenceType(IEnumerable<object> source, int expected) =>
            Assert.AreEqual(expected, source.Count());

         [TestCase(null, null)]
         public void CountWithPredicate_SourceIsNull_ThrowArgumentNullException(IEnumerable<int> source, Func<int, bool> predicate) =>
           Assert.Throws<ArgumentNullException>(() => source.Count(predicate));

         [TestCase(new int[] { 1, 2 }, null)]
         public void CountWithPredicate_PredicateIsNull_ThrowArgumentNullException(IEnumerable<int> source, Func<int, bool> predicate) =>
          Assert.Throws<ArgumentNullException>(() => source.Count(predicate));

         [TestCaseSource(nameof(TestCasesDataForCountWithPredicateValueType))]
         public void CountWithPredicate_ValueType(IEnumerable<int> source, int expected, Func<int, bool> predicate) =>
            Assert.AreEqual(expected, source.Count(predicate));

         [TestCaseSource(nameof(TestCasesDataForCountWithPredicateReferenceType))]
         public void CountWithPredicate_ReferenceType(IEnumerable<object> source, int expected, Func<object, bool> predicate) =>
           Assert.AreEqual(expected, source.Count(predicate));

         [TestCase(null)]
         public void Count_SourceIsNull_ThrowArgumentNullException(IEnumerable<int> source) =>
           Assert.Throws<ArgumentNullException>(() => source.Count());
    }
}
