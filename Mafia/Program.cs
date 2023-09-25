using System;
using System.Collections.Generic;
using System.Linq;

namespace Mafia
{
    internal class Program
    {
        private static int[] work;

        private static int[] bfsDist;
        private static List<int>[] edges;
        private static int[,] capacities;
        private static int endNode;

        static void Main(string[] args)
        {
            //9:13 20min
            string inp = Console.ReadLine();
            int group = 0;
            while (inp != "end")
            {
                group++;
                int n = int.Parse(inp);
                endNode = n - 1;
                edges = new List<int>[n];
                capacities = new int[n,n];
                bfsDist = new int[n];
                work = new int[n];
                for (int i = 0; i < n; i++)
                {
                    edges[i] = new List<int>();
                }
                inp = Console.ReadLine();
                var data = inp.Split(new char[] {' ', '-'}).Select(int.Parse).ToArray();
                int testout;
                while (!int.TryParse(inp, out testout) && inp!="end")
                {
                    data = inp.Split(new char[] { ' ', '-' }).Select(int.Parse).ToArray();
                    edges[data[0]-1].Add(data[1] - 1);
                    edges[data[1]-1].Add(data[0] - 1);
                    capacities[data[0] - 1, data[1] - 1] = data[2];
                    capacities[data[1] - 1, data[0] - 1] = data[2];
                    inp = Console.ReadLine();
                }

                Console.WriteLine($"Group {group}: {Dinic(0, endNode)}");
            }
        }
        static int Dinic(int source, int destination)
        {
            int result = 0;
            while (Bfs(source, destination))
            {
                for (int i = 0; i < work.Length; i++)
                {
                    work[i] = 0;
                }

                int delta;
                do
                {
                    delta = Dfs(source, int.MaxValue);
                    result += delta;
                }
                while (delta != 0);
            }
            return result;
        }

        static bool Bfs(int src, int dest)
        {
            for (int i = 0; i < bfsDist.Length; i++)
            {
                bfsDist[i] = -1;
            }
            bfsDist[src] = 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(src);
            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();
                for (int i = 0; i < edges[currentNode].Count; i++)
                {
                    int child = edges[currentNode][i];
                    if (bfsDist[child] < 0 && capacities[currentNode, child] > 0)
                    {
                        bfsDist[child] = bfsDist[currentNode] + 1;
                        queue.Enqueue(child);
                    }
                }
            }
            return bfsDist[dest] >= 0;
        }

        static int Dfs(int source, int flow)
        {
            if (source == endNode)
            {
                return flow;
            }
            for (int i = work[source]; i < edges[source].Count; i++, work[source]++)
            {
                int child = edges[source][i];
                if (capacities[source, child] <= 0) continue;
                if (bfsDist[child] == bfsDist[source] + 1)
                {
                    int augmentationPathFlow = Dfs(child, Math.Min(flow, capacities[source, child]));
                    if (augmentationPathFlow > 0)
                    {
                        capacities[source, child] -= augmentationPathFlow;
                        capacities[child, source] += augmentationPathFlow;
                        return augmentationPathFlow;
                    }
                }
            }
            return 0;
        }
    }
}
