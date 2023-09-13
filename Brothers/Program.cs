using System;
using System.Linq;

namespace Brothers
{
    internal class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var nums = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Console.WriteLine(Check3Partitions(nums) ? "Yes" : "No");
            }
        }

        static bool Check3Partitions(int[] nums)
        {
            int totalSum = nums.Sum();
            if (totalSum % 3 != 0)
            {
                // The total sum cannot be divided into 3 equal integer numbers
                return false;
            }

            int targetSum = totalSum / 3;
            var sumReached = new bool[targetSum + 1, targetSum + 1];
            //var prev1 = new short[targetSum + 1, targetSum + 1];
            //var prev2 = new short[targetSum + 1, targetSum + 1];

            // Dynamic programming: fill sumReached[0...targetSum, 0...targetSum]
            sumReached[0, 0] = true;
            foreach (short num in nums)
            {
                for (int s1 = targetSum; s1 >= 0; s1--)
                {
                    for (int s2 = targetSum; s2 >= 0; s2--)
                    {
                        if (sumReached[s1, s2])
                        {
                            if (s1 + num <= targetSum && !sumReached[s1 + num, s2])
                            {
                                sumReached[s1 + num, s2] = true;
                                //prev1[s1 + num, s2] = num;
                            }
                            if (s2 + num <= targetSum && !sumReached[s1, s2 + num])
                            {
                                sumReached[s1, s2 + num] = true;
                                //prev2[s1, s2 + num] = num;
                            }
                        }
                    }
                }
            }

            bool possible = sumReached[targetSum, targetSum];
            //if (possible)
            //{
            //    PrintSolution(nums, sumReached, prev1, prev2, targetSum);
            //}
            return possible;
        }

    }
}
