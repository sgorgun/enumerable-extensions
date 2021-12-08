using System;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Quantifiers")]
    public class EnumerableExtensionsQuantifiersTestFixture
    {
        [TestCase(new[] { 123, 321, 1235 }, 100, ExpectedResult = true)]
        [TestCase(new[] { 23, 23, 15 }, 100, ExpectedResult = false)]
        public bool All_Numbers_More_Than_Value(IEnumerable<int> numbers, int value) =>
            numbers.All(item => item > value);

        [TestCase(new[] { "believe", "relief", "receipt", "field" }, "ie", ExpectedResult = false)]
        [TestCase(new[] { "believe", "relief", "receipt", "field" }, "xyz", ExpectedResult = false)]
        [TestCase(new[] { "believe", "relief", "field" }, "ie", ExpectedResult = true)]
        public bool All_Strings_Contains_Phrase(IEnumerable<string> numbers, string phrase) =>
            numbers.All(item => item.Contains(phrase, StringComparison.InvariantCulture));
    }
}
