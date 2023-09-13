using System;
using System.Linq;

namespace SameGroupSums
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //6:12
            int[] nums = Console.ReadLine().Split('\n').Select(int.Parse).ToArray();
            if (nums.Sum() %2 == 0)
            {
                int target = nums.Sum() / 2;
            }
        }
    }
}
