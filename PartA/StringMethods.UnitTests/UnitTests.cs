using System;
using System.Linq;
using NUnit.Framework;
using PartA;

namespace StringMethods.UnitTests
{
    [TestFixture]
    public class UnitTests
    {

        [Test]
        [TestCase("Cat and dog", ' ', new string[] { "Cat", " ", "and", " ", "dog" })]
        [TestCase("Cat ? and! dog.", ' ', new string[] { "Cat", " ","?"," ", "and!", " ", "dog." })]
        public void MySplitWithSeparatorToStringEnumerableShouldWork(string main, char seperator, string[] expected)
        {
            var actual = main.SplitWithSeparatorToStringEnumerable(seperator);
            Assert.AreEqual(actual.ToArray().Length, expected.Length);

        }

        [Test]
        [TestCase("Cat ", new char[] { 'C', 'a', 't', ' ' })]
        public void ToEnumerableCharShouldWork(string main, char[] expected)
        {
            var actual = main.ToEnumerableChar();
            var actualArray = actual.ToArray();
            Assert.AreEqual(actualArray.Length, expected.Length);
        }

        [Test]
        [TestCase("Cat ", " taC")]
        [TestCase("Cat", "taC")]
        [TestCase(" ", " ")]
        [TestCase("a", "a")]
        [TestCase("ab", "ba")]
        public void ReverseStringShouldWork(string main, string expected)
        {
            var actual = main.ReverseString();
            Assert.AreEqual(actual, expected);
        }

        [Test]
        [TestCase("Cat and dog", "taC dna god", ' ')]
        [TestCase("    a ", "    a ", ' ')]
        [TestCase(" a b,c d. ", " a c,b .d ", ' ')]
        public void ReverseWordShouldWork(string main, string expected, char token)
        {
            var actual = main.ReverseWords(token);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("Cat and dog", "god dna taC")]
        [TestCase("    a ", " a    ")]
        [TestCase(" a b,c d. ", " .d c,b a ")]
        public void ReverseWordWithoutSeparatorShouldReverseString(string main, string expected)
        {
            var actual = main.ReverseWords();
            Assert.AreEqual(expected, actual);
        }

    }
}
