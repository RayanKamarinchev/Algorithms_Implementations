using System;

namespace DrawHouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //6:22
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(new string('.', n-1) + '*' + new string('.', n - 1));
            for (int i = 0; i < n-2; i++)
            {
                Console.WriteLine(new string('.', n-i-2) + '*' + new string(' ', i*2 + 1) + '*' + new string('.', n - i - 2));
            }

            for (int i = 0; i < n-1; i++)
            {
                Console.Write("* ");
            }
            Console.WriteLine("*");

            Console.WriteLine('+' + new string('-', n*2-3) + '+');
            for (int i = 0; i < n-2; i++)
            {
                Console.WriteLine('|' + new string(' ', n * 2 - 3) + '|');
            }
            Console.WriteLine('+' + new string('-', n*2-3) + '+');
        }
    }
}
