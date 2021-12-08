using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EnumerableExtensionsTask.Tests")]

namespace EnumerableExtensionsTask
{
    /// <summary>
    /// Implements array helper methods.
    /// </summary>
    internal static class BufferData
    {
        /// <summary>
        /// Creates array on base of enumerable sequence.
        /// </summary>
        /// <param name="source">The enumerable sequence.</param>
        /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
        /// <returns>Single dimension zero based array.</returns>
        internal static (T[] buffer, int count) ToArray<T>(IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }
    }
}