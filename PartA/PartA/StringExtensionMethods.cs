using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartA
{
    public static class StringExtensionMethods
    {

        public static IEnumerable<string> SplitWithSeparatorToStringEnumerable(this string input, params char[] sep)
        {
            var size = input.Length;
            if (size <= 0) yield break;
            for (var i = 0; i < input.Length; i++)
            {
                var token = input.Skip(i).TakeWhile(x => sep.All(y => x != y));
                var charArray = token.ToArray();
                yield return new string(charArray);

                // getting the seperator character
                if (charArray.Length + i < size)
                    yield return new string(new char[] { input[charArray.Length + i] });

                i += charArray.Length;
            }
        }
        public static IEnumerable<char> ToEnumerableChar(this string input)
        {
            foreach (var item in input)
            {
                yield return item;
            }
        }

        public static char[] ReverseCharArray(this char[] input)
        {
            if (input == null) return null;

            var size = input.Length;

            for (var i = 0; i < size / 2; i++)
            {
                var temp = input[i];
                input[i] = input[size - 1 - i];
                input[size - 1 - i] = temp;
            }

            return input;
        }
        public static string ReverseString(this string input)
        {
            if (input == null) return null;
            if (input.Length == 0) return input;
            var size = input.Length;
            var result = new char[size];

            for (var i = size; i > 0; i--)
            {
                result[size - i] = input[i - 1];
            }

            return new string(result);
        }


        public static string ReverseWords(this string input, params char[] separators)
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }

            if (separators == null) return new string(input.ToEnumerableChar().ToArray().ReverseCharArray());

            var result = new List<string>();

            foreach (var token in input.SplitWithSeparatorToStringEnumerable(separators))
            {
                result.Add(token.ReverseString());
            }

            return new string(result.SelectMany(x => x).ToArray());
        }

    }
}
