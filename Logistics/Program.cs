using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //10:05 10miin 6:36 7:20 7:36 50
            int n = int.Parse(Console.ReadLine());
            var packages = new Package[n];
            int maxDay = 0;
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                maxDay = Math.Max(maxDay, input[1]);
                packages[i] = new Package(input[0], input[1], i+1);
            }

            var failsIn = Console.ReadLine().Split();
            Stack<int> fails;
            if (failsIn[0] == "none")
            {
                fails = new Stack<int>();
            }
            else
            {
                fails = new Stack<int>(failsIn.Select(int.Parse).OrderBy(x=>x));
            }

            packages = packages.OrderByDescending(s => s.Price).ToArray();
            long profit = 0;
            int[] indexes = new int[maxDay];
            List<int> used = new List<int>(fails.Count);
            int filled = 0;
            if (fails.Count != 0)
            {
                int b = fails.Pop();
                for (int j = packages.Length - 1; j >= 0; j--)
                {
                    if (packages[j].Day > b - 1 && !used.Contains(j))
                    {
                        indexes[b - 1] = packages[j].Index;
                        profit -= 50 + packages[j].Price;
                        filled++;
                        if (fails.Count == 0)
                        {
                            break;
                        }
                        used.Add(j);
                        b = fails.Pop();
                        j = packages.Length;
                    }
                }
            }
            foreach (var t in packages)
            {
                var item = t;
                for (int k = item.Day - 1; k >= 0; k--)
                {
                    if (indexes[k] == 0)
                    {
                        indexes[k] = item.Index;
                        profit += item.Price;
                        filled++;
                        break;
                    }
                }

                if (filled==maxDay)
                {
                    break;
                }
            }

            Console.WriteLine(profit);
            Console.WriteLine(string.Join(" ", indexes));
        }
    }


    class Package
    {
        public int Price { get; set; }
        public int Day { get; set; }
        public int Index { get; set; }
        public Package(int price, int day, int index) { 
            Price = price;
            Day = day;
            Index = index;
        }
    }
}
