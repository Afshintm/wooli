using System;

namespace PartA
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("This program gets a string and reverse each word of the string separated by space.");
            Console.WriteLine("Unit tests have been provided with NUnit framework.");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Please enter your string:");
            var str = Console.ReadLine();
            while (!string.IsNullOrEmpty(str))
            {
                var reversed = str.ReverseWords(' ');
                Console.WriteLine(reversed);
                str = Console.ReadLine();
            }
        }
    }
}
