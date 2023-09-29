using System;
using System.Linq;

namespace _10_Plants
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] rows = new int[n];
            for (int i = 0; i < n; i++)
            {
                rows[i] = Console.ReadLine().Split().Select(int.Parse).Sum();
            }
            int max = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                rows[i] = max - rows[i];
            }

        }
    }
}
