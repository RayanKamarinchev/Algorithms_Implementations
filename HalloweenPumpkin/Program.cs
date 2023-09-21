using System;

namespace HalloweenPumpkin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //8:42 - 3min
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(new string('.', n-1) + "_/_" + new string('.', n - 1));
            Console.WriteLine($"/{new string('.', n-2)}^,^{new string('.', n-2)}\\");
            for (int i = 0; i < n-3; i++)
            {
                Console.WriteLine($"|{new string('.', n*2-1)}|");
            }
            Console.WriteLine($"\\{new string('.', n - 2)}\\_/{new string('.', n - 2)}/");
        }
    }
}
