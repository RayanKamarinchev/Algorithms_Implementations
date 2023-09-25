using System;
using System.Linq;
using System.Xml;

namespace OutsideCircle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:35 18 min
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var circle = Console.ReadLine().Remove(0, 7).Split(new string[] { ", " }, StringSplitOptions.None).Select(decimal.Parse).ToArray();
                var trig = Console.ReadLine().Remove(0, 9).Split(new string[] { ", " }, StringSplitOptions.None).Select(decimal.Parse).ToArray();
                bool isOutsideCircle = Distance(trig[0], circle[0], trig[1], circle[1], circle[2]) &&
                                       Distance(trig[2], circle[0], trig[3], circle[1], circle[2]) &&
                                       Distance(trig[4], circle[0], trig[5], circle[1], circle[2]);
                bool centerInTriangle = PointInTriangle((circle[0], circle[1]), (trig[0], trig[1]), (trig[2], trig[3]), (trig[4], trig[5]));
                string res = "";
                if (isOutsideCircle)
                {
                    res += "The circle is circumscribed and ";
                }
                else
                {
                    res += "The circle is not circumscribed and ";
                }

                if (centerInTriangle)
                {
                    res += "the center is inside.";
                }
                else
                {
                    res += "the center is outside.";
                }

                Console.WriteLine(res);
            }
        }

        static bool Distance(decimal x1, decimal x2, decimal y1, decimal y2, decimal r)
        {
            return Math.Abs((double)(Math.Sqrt((double)((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))) - (double)r)) <= 0.01;
        }

        static decimal sign((decimal, decimal) p1, (decimal, decimal) p2, (decimal, decimal) p3)
        {
            return (p1.Item1 - p3.Item1) * (p2.Item2 - p3.Item2) - (p2.Item1 - p3.Item1) * (p1.Item2 - p3.Item2);
        }

        static bool PointInTriangle((decimal, decimal) pt, (decimal, decimal) v1, (decimal, decimal) v2, (decimal, decimal) v3)
        {
            decimal d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(pt, v1, v2);
            d2 = sign(pt, v2, v3);
            d3 = sign(pt, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }
    }
}
