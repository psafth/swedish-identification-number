using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentificationNumber.Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void AreEqual<T>(this Assert assert, T expected, T actual, IComparer comparer)
        {
            CollectionAssert.AreEqual(
                new[] { expected },
                new[] { actual }, comparer,
                $"\nExpected: <{expected}>.\nActual: <{actual}>.");
        }
    }
}
