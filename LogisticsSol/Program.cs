using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Softuniada
{
    class Package : IComparable<Package>
    {
        public Package(int index, int price, int deadline)
        {
            this.Index = index;
            this.Price = price;
            this.Deadline = deadline;
        }

        public int Index { get; set; }
        public int Price { get; set; }
        public int Deadline { get; set; }

        public int CompareTo(Package other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }

    class Logistics
    {
        static void Main(string[] args)
        {
            var packages = new Dictionary<int, List<Package>>();
            var accidents = new HashSet<int>();
            int n = int.Parse(Console.ReadLine());
            var highestDeadline = 0;
            for (int i = 1; i <= n; i++)
            {
                var arguments = Console.ReadLine().Split();
                var price = int.Parse(arguments[0]);
                var deadline = int.Parse(arguments[1]);

                highestDeadline = deadline > highestDeadline ? deadline : highestDeadline;

                if (!packages.ContainsKey(deadline))
                {
                    packages.Add(deadline, new List<Package>());
                }

                packages[deadline].Add(new Package(i, price, deadline));
            }

            var accidentsLine = Console.ReadLine();
            if (accidentsLine != "none")
            {
                accidents = new HashSet<int>(accidentsLine.Split().Select(int.Parse));
            }

            var sortedPackages = new SortedSet<Package>();
            var bestPackages = new List<int>();

            int total = 0;
            for (int deadline = highestDeadline; deadline >= 1; deadline--)
            {
                if (packages.ContainsKey(deadline))
                {
                    foreach (var package in packages[deadline])
                    {
                        sortedPackages.Add(package);
                    }
                }

                Package bestPackage = null;

                if (accidents.Contains(deadline))
                {
                    bestPackage = sortedPackages.Min;
                    total -= 50;
                    total -= bestPackage.Price;
                }
                else
                {
                    bestPackage = sortedPackages.Max;
                    total += bestPackage.Price;
                }

                sortedPackages.Remove(bestPackage);
                bestPackages.Add(bestPackage.Index);
            }

            Console.WriteLine(total);
            bestPackages.Reverse();
            Console.WriteLine(string.Join(" ", bestPackages));
        }
    }
}
