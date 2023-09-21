using System;
using System.Linq;

namespace ParkingZones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //10:30 30min
            int n = int.Parse(Console.ReadLine());
            Zone[] zones = new Zone[n];
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string name = input.Split(':')[0];
                string[] els = input.Split(new string[] {": "},StringSplitOptions.None)[1]
                                    .Split(new string[] {", "}, StringSplitOptions.None);
                zones[i] = new Zone(name, int.Parse(els[1]), int.Parse(els[0]), int.Parse(els[1]) + int.Parse(els[3]), int.Parse(els[0]) + int.Parse(els[2]),
                                    decimal.Parse(els[4]));
            }
            var slots = Console.ReadLine().Split(new string[] { "; " }, StringSplitOptions.None).Select(x=>x.Split(new string[] { ", " }, StringSplitOptions.None).Select(int.Parse).ToArray());
            int[] target = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.None).Select(int.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());
            Slot final = null;
            foreach (var slot in slots)
            {
                Zone chosen = null;
                foreach (var zone in zones)
                {
                    if (slot[0]>=zone.Left && slot[0] <= zone.Right && slot[1] >= zone.Top && slot[1] <= zone.Bottom)
                    {
                        chosen = zone;
                    }
                }

                int path = (Math.Abs(target[0] - slot[0]) + Math.Abs(target[1] - slot[1])) - 1;
                decimal price = chosen.PricePerMin * Math.Ceiling(k * path/30.0m);
                if (final is null || final.Price > price || (final.Price == price && final.Path > path))
                {
                    final = new Slot(chosen.Name, slot[0], slot[1], price, path);
                }
            }

            Console.WriteLine($"Zone Type: {final.ZoneName}; X: {final.X}; Y: {final.Y}; Price: {final.Price:f2}");
        }
    }

    class Zone
    {
        public string Name { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Bottom { get; set; }
        public int Right { get; set; }
        public decimal PricePerMin { get; set; }
        public Zone(string name, int top, int left, int bottom, int right, decimal pricePerMin)
        {
            Name = name;
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
            PricePerMin = pricePerMin;
        }
    }

    class Slot
    {
        public string ZoneName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public decimal Price { get; set; }
        public int Path { get; set; }

        public Slot(string zoneName, int x, int y, decimal price, int path)
        {
            ZoneName = zoneName;
            X = x;
            Y = y;
            Price = price;
            Path = path;
        }
    }
}
