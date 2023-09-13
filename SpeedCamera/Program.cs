using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedCamera
{
    internal class Program
    {
        //1:15 - 1:30 1:40 - 2:25
        static decimal[,] floydWarshall(decimal[,] graph, int V)
        {
            decimal[,] dist = new decimal[V, V];
            int i, j, k;
            
            for (i = 0; i < V; i++)
            {
                for (j = 0; j < V; j++)
                {
                    dist[i, j] = graph[i, j];
                }
            }
            for (k = 0; k < V; k++)
            {
                for (i = 0; i < V; i++)
                {
                    for (j = 0; j < V; j++)
                    {
                        if (dist[i, k] + dist[k, j]
                            < dist[i, j])
                        {
                            dist[i, j]
                                = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            // Print the shortest distance matrix
            return dist;
        }

        public struct Edge
        {
            public int Source;
            public int Destination;
            public decimal Weight;
        }

        static void Main(string[] args)
        {
            Console.ReadLine();
            string[] input = Console.ReadLine().Split(' ');
            List<string> cities = new List<string>();
            List<Edge> edges = new List<Edge>();
            while (input[0] != "Records:")
            {
                int index1 = cities.IndexOf(input[0]);
                if (index1 == -1)
                {
                    index1 = cities.Count;
                    cities.Add(input[0]);
                }
                int index2 = cities.IndexOf(input[1]);
                if (index2 == -1)
                {
                    index2 = cities.Count;
                    cities.Add(input[1]);
                }
                decimal time = decimal.Parse(input[2]) / decimal.Parse(input[3]);
                edges.Add(new Edge(){Source = index1, Destination = index2, Weight = time});
                input = Console.ReadLine().Split(' ');
            }

            int v = cities.Count;
            decimal[,] graph = new decimal[v, v];
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    graph[i, j] = 100000000;
                }
            }
            foreach (var edge in edges)
            {
                graph[edge.Destination, edge.Source] = edge.Weight;
                graph[edge.Source, edge.Destination] = edge.Weight;
            }

            decimal[,] dist = floydWarshall(graph, v);
            for (int i = 0; i < v; i++)
            {
                dist[i, i] = 0;
            }
            Dictionary<string, (decimal, int)> cars = new Dictionary<string, (decimal, int)>();
            List<string> bad = new List<string>();
            List<(string, string, decimal)> inputs = new List<(string, string, decimal)>();
            var input2 = Console.ReadLine().Split();
            while (input2[0] != "End")
            {
                var timeStr = input2[2].Split(':').Select(int.Parse).ToArray();
                decimal time = timeStr[0] + timeStr[1] / 60.0m + timeStr[2] / 3600.0m;
                inputs.Add((input2[0], input2[1], time));
                input2 = Console.ReadLine().Split();
            }
            foreach (var input1 in inputs.OrderBy(x => x.Item3))
            {
                if (bad.Contains(input1.Item2))
                {
                }
                else if (!cars.ContainsKey(input1.Item2))
                {
                    cars.Add(input1.Item2, (input1.Item3, cities.IndexOf(input1.Item1)));
                }
                else
                {
                    int currIndex = cities.IndexOf(input1.Item1);
                    var lastIndex = cars[input1.Item2];
                    if (input1.Item3 - lastIndex.Item1 < (dist[currIndex, lastIndex.Item2] == 100000000 ? 0 : dist[currIndex, lastIndex.Item2]))
                    {
                        bad.Add(input1.Item2);
                    }

                    cars[input1.Item2] = (input1.Item3, currIndex);
                }
            }

            bad.OrderBy(x=>x).ToList().ForEach(x=>Console.WriteLine(x));
        }
    }
}
