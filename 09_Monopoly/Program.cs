using System;
using System.Collections.Generic;
using System.Linq;

namespace _09_Monopoly
{
    internal class Graph
    {
        class Edge : IComparable<Edge>
        {
            public int src, dest, weight;

            public int CompareTo(Edge compareEdge)
            {
                return this.weight - compareEdge.weight;
            }
        }

        class Subset
        {
            public int parent, rank;
        }

        int vertices;
        List<Edge> edge;

        public Graph(int v)
        {
            vertices = v;
            edge = new List<Edge>();
        }

        int Find(Subset[] subsets, int i)
        {
            if (subsets[i].parent != i)
                subsets[i].parent = Find(subsets, subsets[i].parent);
            return subsets[i].parent;
        }

        void Union(Subset[] subsets, int x, int y)
        {
            int xroot = Find(subsets, x);
            int yroot = Find(subsets, y);

            if (subsets[xroot].rank < subsets[yroot].rank)
                subsets[xroot].parent = yroot;
            else if (subsets[xroot].rank > subsets[yroot].rank)
                subsets[yroot].parent = xroot;
            else
            {
                subsets[yroot].parent = xroot;
                subsets[xroot].rank++;
            }
        }

        Edge[] KruskalAlgo()
        {
            Edge[] result = new Edge[vertices];
            int e = 0;
            int i = 0;
            for (i = 0; i < vertices; ++i)
                result[i] = new Edge();

            edge = edge.OrderBy(x => x.weight).ToList();
            Subset[] subsets = new Subset[vertices];
            for (i = 0; i < vertices; ++i)
                subsets[i] = new Subset();

            for (int v = 0; v < vertices; ++v)
            {
                subsets[v].parent = v;
                subsets[v].rank = 0;
            }

            i = 0;
            while (e < vertices - 1)
            {
                Edge nextEdge = edge[i++];
                int x = Find(subsets, nextEdge.src);
                int y = Find(subsets, nextEdge.dest);
                if (x != y)
                {
                    result[e++] = nextEdge;
                    Union(subsets, x, y);
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            //11:47 1h 25min
            int n = int.Parse(Console.ReadLine());
            Graph graph = new Graph(n);
            int[] earnings = new int[n];
            for (int i = 0; i < n; i++)
            {
                var linez = Console.ReadLine().Split().Select(int.Parse).ToArray();
                earnings[linez[0]] = linez[1];
            }

            var line = Console.ReadLine().Split();
            while (line[0] != "end")
            {
                graph.edge.Add(new Edge()
                {
                    dest = int.Parse(line[0]), src = int.Parse(line[1]), weight = int.Parse(line[2])
                });
                graph.edge.Add(new Edge()
                {
                    dest = int.Parse(line[1]), src = int.Parse(line[0]), weight = int.Parse(line[2])
                });
                line = Console.ReadLine().Split();
            }

            var allowed = graph.KruskalAlgo();
            List<int> history = new List<int>();
            List<(int, int)>[] tree = new List<(int, int)>[n];
            for (int i = 0; i < n; i++)
            {
                tree[i] = new List<(int, int)>();
            }
            foreach (var edge in allowed)
            {
                tree[edge.src].Add((edge.dest, edge.weight));
                tree[edge.dest].Add((edge.src, edge.weight));
            }
            bool[] visited = new bool[n];
            var end = Dfs(0);
            int maxSum = int.MinValue;
            int sum = 0;
            int endIndex = 0;
            int startIndex = 0;
            end.Item1.Add(0);
            end.Item1.Reverse();
            for (int i = 0; i < end.Item1.Count; i++)
            {
                sum += earnings[end.Item1[i]];
                if (sum < 0)
                {
                    sum = 0;
                    startIndex = i+1;
                }
                else if (maxSum < sum)
                {
                    maxSum = sum;
                    endIndex = i;
                }
            }
            end.Item1 = end.Item1.Skip(startIndex).Take(endIndex-startIndex + 1).OrderBy(x => x).ToList();
            Console.WriteLine(maxSum);
            //end.Item1.RemoveAt(end.Item1.Count-1);
            Console.WriteLine(string.Join(" ", end.Item1));
            (List<int>, int) Dfs(int v)
            {
                var res = new List<int>() {};
                int max = int.MinValue;
                visited[v] = true;
                foreach (var node in tree[v])
                {
                    if (!visited[node.Item1])
                    {
                        var r = Dfs(node.Item1);
                        if (max < r.Item2 + earnings[node.Item1])
                        {
                            res = r.Item1;
                            max = r.Item2 + earnings[node.Item1];
                            res.Add(node.Item1);
                        }
                    }
                }
                return (res, max);
            }
        }
    }
}