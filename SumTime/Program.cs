using System;
using System.Linq;
using DateTime = System.DateTime;

namespace SumTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //-8 min
            //6:32
            var time1Str = Console.ReadLine().Split(':').ToArray();
            var time2Str = Console.ReadLine().Split(':').ToArray();
            TimeSpan time1 = new TimeSpan();
            if (time1Str.Contains(""))
            {
                time1 = new TimeSpan(int.Parse(time1Str[0]), int.Parse(time1Str[2]), int.Parse(time1Str[3]), 0);
            }
            else
            {
                time1 = new TimeSpan(0, int.Parse(time1Str[0]), int.Parse(time1Str[1]), 0);
            }
            TimeSpan time2 = new TimeSpan();
            if (time2Str.Contains(""))
            {
                time2 = new TimeSpan(int.Parse(time2Str[0]), int.Parse(time2Str[2]), int.Parse(time2Str[3]), 0);
            }
            else
            {
                time2 = new TimeSpan(0, int.Parse(time2Str[0]), int.Parse(time2Str[1]), 0);
            }

            TimeSpan comb = time1 + time2;
            if (comb.Days >= 1)
            {
                Console.WriteLine($"{comb.Days}::{comb.Hours}:{comb.Minutes:d2}");
            }
            else
            {
                Console.WriteLine($"{comb.Hours}:{comb.Minutes:d2}");
            }
        }
    }
}
