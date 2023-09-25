using System;
using System.Collections.Generic;

namespace BingoGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //10:48 14
            string num = Console.ReadLine();
            string num1 = $"{num[1]}{num[3]}";
            string num2 = $"{num[0]}{num[2]}";
            var floor = int.Parse(num1) + int.Parse(num2);
            var lowest = int.Parse($"{num1}{num2}");
            List<int> div12 = new List<int>();
            List<int> div15 = new List<int>();
            for (int i = int.Parse(num2); i <= floor; i++)
            {
                for (int j = int.Parse(num1); j <= floor; j++)
                {
                    int n = i * 100 + j;
                    if (n % 12 == 0)
                    {
                        div12.Add(n);
                    }

                    if (n% 15 == 0)
                    {
                        div15.Add(n);
                    }
                }
            }

            Console.WriteLine("Dividing on 12: " + string.Join(" ", div12));
            Console.WriteLine("Dividing on 15: " + string.Join(" ", div15));
            if (div12.Count == div15.Count)
            {
                Console.WriteLine("!!!BINGO!!!");
            }
            else
            {
                Console.WriteLine("NO BINGO!");
            }
        }
    }
}
