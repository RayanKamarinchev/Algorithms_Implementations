using System;
using System.Collections.Generic;

namespace _05_GridVoyage
{
    internal class Program
    {
        static int Simplify(int num)
        {
            if (num > 0)
            {
                return 1;
            }

            if (num == 0)
            {
                return 0;
            }

            return -1;
        }

        static void Main(string[] args)
        {
            //10:27 38min
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            var line = Console.ReadLine().Split();
            int x = int.Parse(line[0]);
            int y = int.Parse(line[1]);
            int targetX;
            int targetY;
            string dir;
            int stamina;
            int[] change = new int[] { 0, 0 };
            int[] diaChange = new int[] { 0, 0 };
            List<int[]> moves = new List<int[]>();

            bool isOnX = false;

            line = Console.ReadLine().Split();
            while (line[0] != "eastern odyssey")
            {
                dir = line[2];
                stamina = int.Parse(line[3]);
                targetX = int.Parse(line[0]);
                targetY = int.Parse(line[1]);
                switch (dir)
                {
                    case "left":
                        change = new int[]{-1, 0};
                        isOnX = true;
                        break;
                    case "right":
                        change = new int[] { 1, 0 };
                        isOnX = true;
                        break;
                    case "up":
                        isOnX = false;
                        change = new int[] { 0, -1 };
                        break;
                    case "down":
                        isOnX = false;
                        change = new int[] { 0, 1 };
                        break;
                }

                x += change[0] * stamina;
                y += change[1] * stamina;
                int distX = targetX - x;
                int distY = targetY - y;
                if (distX % stamina != 0 || distY % stamina != 0)
                {
                    Console.WriteLine("impossible");
                    break;
                }
                distX/=stamina;
                distY/=stamina;
                diaChange = new int[] { Simplify(distX), Simplify(distY) };
                bool xFirst = !isOnX;

                distX = Math.Abs(distX);
                distY = Math.Abs(distY);
                int diagonals = Math.Min(distX, distY);
                for (int i = 0; i < diagonals; i++)
                {
                    if (xFirst)
                    {
                        moves.Add(new int[] { diaChange[0], 0 });
                        moves.Add(new int[] { 0, diaChange[1] });
                    }
                    else
                    {
                        moves.Add(new int[] { 0, diaChange[1] });
                        moves.Add(new int[] { diaChange[0], 0 });
                    }
                }
                distX -=diagonals;
                distY -=diagonals;
                int newDoubles;

                if (distX == 0)
                {
                    newDoubles = distY / 2;

                }
                else
                {
                    newDoubles = distX / 2;

                }
            }
        }
    }
}
