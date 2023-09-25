using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Asteroids
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:58 30 min
            var input = Console.ReadLine().Split('x').Select(int.Parse).ToArray();
            int[,] matrix = new int[input[0], input[1]];
            List<int> groups = new List<int>();

            void ColorGroup(int i, int j)
            {
                if (matrix[i, j] == 1)
                {
                    matrix[i, j] = groups.Count + 1;
                    groups[groups.Count - 1]++;
                    if (i > 0)
                    {
                        ColorGroup(i - 1, j);
                    }
                    if (j > 0)
                    {
                        ColorGroup(i, j - 1);
                    }
                    if (i + 1 != input[0])
                    {
                        ColorGroup(i + 1, j);
                    }
                    if (j + 1 != input[1])
                    {
                        ColorGroup(i, j + 1);
                    }
                }
            }

            while (true)
            {
                matrix = new int[input[0], input[1]];
                groups = new List<int>();
                for (int i = 0; i < input[0]; i++)
                {
                    string row = Console.ReadLine();
                    for (int j = 0; j < input[1]; j++)
                    {
                        matrix[i, j] = int.Parse(row[j].ToString());
                    }
                }
                for (int i = 0; i < input[0]; i++)
                {
                    for (int j = 0; j < input[1]; j++)
                    {
                        if (matrix[i, j] == 1)
                        {
                            groups.Add(0);
                            ColorGroup(i, j);
                        }
                    }
                }

                Dictionary<int, int> res = new Dictionary<int, int>();
                foreach (var group in groups.OrderByDescending(x => x))
                {
                    if (!res.ContainsKey(group))
                    {
                        res.Add(group, 1);
                    }
                    else
                    {
                        res[group]++;
                    }
                }

                foreach (var re in res)
                {
                    Console.WriteLine($"{re.Value}x{re.Key}");
                }

                Console.WriteLine("Total: " + groups.Count);
                var pre = Console.ReadLine().Split('x');
                if (pre.Length == 1)
                {
                    return;
                }
                input = pre.Select(int.Parse).ToArray();
            }
        }
    }
}
