using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace StarCube
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // - 10 min
            //6:54
            int n = int.Parse(Console.ReadLine());
            char[,,] cube = new char[n,n,n];
            for (int i = 0; i < n; i++)
            {
                var rows = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.None).Select(x => x.Split(' ')).ToArray();
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        cube[j, i, k] = rows[j][k][0];
                    }
                }
            }

            Dictionary<char, int> res = new Dictionary<char, int>();
            int r = 0;
            for (int i = 0; i < n-2; i++)
            {
                for (int j = 1; j < n-1; j++)
                {
                    for (int k = 1; k < n-1; k++)
                    {
                        char c = cube[i, j, k];
                        if (cube[i+1, j+1, k] == c && cube[i + 1, j - 1, k] == c && cube[i + 1, j, k + 1] == c && cube[i + 1, j, k - 1] == c && cube[i + 1, j, k] == c && cube[i + 2, j, k] == c)
                        {
                            if (!res.ContainsKey(c))
                            {
                                res.Add(c, 0);
                            }
                            res[c]++;
                            r++;
                        }
                    }
                }
            }

            Console.WriteLine(r);
            var keyValuePairs = from entry in res orderby entry.Key ascending select entry;
            foreach (var a in keyValuePairs)
            {
                Console.WriteLine(a.Key + " -> " + a.Value);
            }
        }
    }
}
