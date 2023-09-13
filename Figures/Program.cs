using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace Figures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //- 20min
            //7: 34 - 8:34
            //9:50 - 10:20
            decimal clamp(decimal val, decimal min, decimal max)
            {
                if (val < min)
                    val = min;
                if (val > max)
                    val = max;
                return val;
            }
            
            int n = int.Parse(Console.ReadLine());
            string s = "";
            Regex re1 = new Regex("circle\\(([+-]?\\d*\\.?\\d+), ([+-]?\\d*\\.?\\d+), ([+-]?\\d*\\.?\\d+)\\)");
            Regex re2 = new Regex(
                "rectangle\\(([+-]?\\d*\\.?\\d+), ([+-]?\\d*\\.?\\d+), ([+-]?\\d*\\.?\\d+), ([+-]?\\d*\\.?\\d+)\\)");
            for (int i = 0; i < n; i++)
            {
                decimal circleX, circleY, circleRadius, left, right, top, bottom;

                string in1 = Console.ReadLine();
                string in2 = Console.ReadLine();

                var match = re1.Match(in1);
                if (!match.Success)
                {
                    match = re2.Match(in1);
                    //Rect
                    left = decimal.Parse(match.Groups[1].Value);
                    top = decimal.Parse(match.Groups[2].Value);
                    right = decimal.Parse(match.Groups[3].Value);
                    bottom = decimal.Parse(match.Groups[4].Value);
                    (top, bottom) = (Math.Max(top, bottom), Math.Min(top, bottom));
                    (right, left) = (Math.Max(right, left), Math.Min(right, left));

                    match = re1.Match(in2);
                    //Circle
                    circleX = decimal.Parse(match.Groups[1].Value);
                    circleY = decimal.Parse(match.Groups[2].Value);
                    circleRadius = decimal.Parse(match.Groups[3].Value);
                }
                else
                {
                    //Circle
                    circleX = decimal.Parse(match.Groups[1].Value);
                    circleY = decimal.Parse(match.Groups[2].Value);
                    circleRadius = decimal.Parse(match.Groups[3].Value);

                    match = re2.Match(in2);
                    //Rect
                    left = decimal.Parse(match.Groups[1].Value);
                    top = decimal.Parse(match.Groups[2].Value);
                    right = decimal.Parse(match.Groups[3].Value);
                    bottom = decimal.Parse(match.Groups[4].Value);
                    (top, bottom) = (Math.Max(top, bottom), Math.Min(top, bottom));
                    (right, left) = (Math.Max(right, left), Math.Min(right, left));
                }

                decimal closestX = clamp(circleX, left,right);
                decimal closestY = clamp(circleY, bottom,top);

                // Calculate the distance between the circle's center and this closest point
                decimal distanceX = Math.Abs(circleX - closestX) - 0.01m;
                decimal distanceY = Math.Abs(circleY - closestY) - 0.01m;
                // If the distance is less than the circle's radius, an intersection occurs
                decimal distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);
                bool intercept = distanceSquared <= (circleRadius * circleRadius);

                decimal up = (Math.Abs(top - circleY)-0.01m) * (Math.Abs(top - circleY) - 0.01m);
                decimal down = (Math.Abs(bottom - circleY)-0.01m) * (Math.Abs(bottom - circleY) - 0.01m);
                decimal left2 = (Math.Abs(circleX - left)-0.01m) * (Math.Abs(circleX - left) - 0.01m);
                decimal right2 = (Math.Abs(circleX - right)-0.01m) * (Math.Abs(circleX - right) - 0.01m);
                decimal distMax = Math.Max(Math.Max(Math.Max(left2+up, left2+down), right2 + up), right2 + down);
                bool rectIn = distMax <= (circleRadius * circleRadius);
                bool circleIn = (circleX-circleRadius+0.01m>=left) && (circleX+circleRadius - 0.01m <= right) && (circleY + circleRadius - 0.01m <= top) && (circleY - circleRadius + 0.01m >= bottom);
                if (circleIn)
                    s+="Circle inside rectangle\n";
                else if (rectIn)
                    s+="Rectangle inside circle\n";
                else if (intercept)
                    s+="Rectangle and circle cross\n";
                else
                    s+="Rectangle and circle do not cross\n";
            }

            Console.WriteLine(s);
        }
    }
}
