using System;
using System.Collections.Generic;
using System.Linq;

namespace WrongResults
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:31 16min
            int n = int.Parse(Console.ReadLine());
            int[,,] cube = new int[n, n, n];
            for (int i = 0; i < n; i++)
            {
                var rows = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.None).Select(x => x.Split(' ')).ToArray();
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        cube[j, i, k] = int.Parse(rows[j][k]);
                    }
                }
            }
            var coords = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int wrong = cube[coords[0], coords[1], coords[2]];
            List<(int,int,int)> indexes = new List<(int,int,int)> ();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (cube[i,j,k] == wrong)
                        {
                            indexes.Add((i,j,k));
                            int sum = 0;
                            if (i > 0 && cube[i - 1, j, k] != wrong && !indexes.Contains((i-1,j,k)))
                            {
                                sum += cube[i-1,j,k];
                            }
                            if (i+1 != n && cube[i + 1, j, k] != wrong && !indexes.Contains((i + 1, j, k)))
                            {
                                sum += cube[i + 1, j, k];
                            }
                            if (j > 0 && cube[i, j - 1, k] != wrong && !indexes.Contains((i, j - 1, k)))
                            {
                                sum += cube[i, j - 1, k];
                            }
                            if (j + 1 != n && cube[i, j + 1, k] != wrong && !indexes.Contains((i, j + 1, k)))
                            {
                                sum += cube[i, j + 1, k];
                            }
                            if (k > 0 && cube[i, j, k - 1] != wrong && !indexes.Contains((i, j, k - 1)))
                            {
                                sum += cube[i, j, k - 1];
                            }
                            if (k + 1 != n && cube[i, j, k + 1] != wrong && !indexes.Contains((i, j, k + 1)))
                            {
                                sum += cube[i, j, k + 1];
                            }
                            cube[i,j,k] = sum;
                        }
                    }
                }
            }

            Console.WriteLine("Wrong values found and replaced: " + indexes.Count);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        Console.Write(cube[i, j, k] + " ");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
