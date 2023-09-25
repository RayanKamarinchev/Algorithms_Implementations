using System;

namespace AwesomeNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //10:35 3min
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int count = 0;
            if (Math.Abs(a)%2 == 1)
            {
                count++;
            }

            if (a < 0)
            {
                count++;
            }

            if (a%b==0)
            {
                count++;
            }

            if (count==0)
            {
                Console.WriteLine("boring");
            }

            if (count == 1)
            {
                Console.WriteLine("awesome");
            }

            if (count == 2)
            {
                Console.WriteLine("super awesome");
            }

            if (count == 3)
            {
                Console.WriteLine("super special awesome");
            }
        }
    }
}
