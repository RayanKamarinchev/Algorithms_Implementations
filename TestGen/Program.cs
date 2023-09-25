using System;
using System.Collections.Generic;

namespace TestGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            int endDay = 5;
            Random rand = new Random();
            Console.WriteLine(n);
            List<int> used = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int c = rand.Next(0, 100);
                while (used.Contains(c))
                {
                    c = rand.Next(0, 100);
                }
                used.Add(c);
                Console.Write(c + " ");
                Console.WriteLine(rand.Next(1, endDay));
            }

            Console.WriteLine("2 3");
        }
    }
}
