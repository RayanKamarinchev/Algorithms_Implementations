using System;
using System.Linq;

namespace DuplicatedLetters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //9:19 5-6 min
            var text = Console.ReadLine().ToCharArray().ToList();
            int count = 0;
            for (int i = 1; i < text.Count; i++)
            {
                if (i != 0 && text[i] == text[i-1])
                {
                    text.RemoveRange(i-1,2);
                    i -= 2;
                    count++;
                }
            }

            if (text.Count == 0)
            {
                Console.WriteLine("Empty String");
            }
            else
            {
                Console.WriteLine(string.Join("", text));
            }
            Console.WriteLine(count + " operations");
        }
    }
}
