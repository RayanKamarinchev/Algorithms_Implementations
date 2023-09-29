using System;
using System.Collections.Generic;

namespace TriForce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:07 17
            int p = int.Parse(Console.ReadLine());
            double r = double.Parse(Console.ReadLine());
            int c = (int)(2 * r);
            List<(int,int)> pairs = new List<(int,int)> ();
            for (int i = 1; i <= (p-c)/2; i++)
            {
                if (i*(p-c-i) == p*(p/2 - c))
                {
                    pairs.Add((i, p - c - i));
                }
            }

            foreach (var (a,b) in pairs)
            {
                Console.WriteLine($"{c}.{b}.{a}");
                Console.WriteLine($"{c}.{a}.{b}");
                Console.WriteLine($"{b}.{c}.{a}");
                Console.WriteLine($"{b}.{a}.{c}");
                Console.WriteLine($"{a}.{c}.{b}");
                Console.WriteLine($"{a}.{b}.{c}");
            }
        }
    }
}
