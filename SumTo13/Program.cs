using System;
using System.Linq;

namespace SumTo13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //8:30 - 5min
            var nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for (int i = -1; i < 2; i+=2)
            {
                for (int j = -1; j < 2; j += 2)
                {
                    for (int k = -1; k < 2; k += 2)
                    {
                        if (nums[0]*i + nums[1] * j + nums[2] * k == 13)
                        {
                            Console.WriteLine("Yes");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("No");
        }
    }
}
