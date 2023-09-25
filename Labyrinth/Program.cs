using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth
{
    internal class Program
    {
        static bool[] visited;
        static byte[] dp;
        static HashSet<int> keys;
        private static List<int>[] graph;
        static void Main(string[] args)
        {
            //10:35 70min
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());

            graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            for (int i = 0; i < e; i++)
            {
                var line = Console.ReadLine().Split().Select(int.Parse).ToArray();
                graph[line[0]].Add(line[1]);
                graph[line[1]].Add(line[0]);
            }

            visited = new bool[n];
            dp = new byte[n];
            dp[n - 1] = 2;
            keys = new HashSet<int>();
            Dfs(0);
            long count = 0;
            StringBuilder sb = new StringBuilder();
            keys.Remove(n - 1);
            foreach (var key in keys)
            {
                count += key;
                sb.Append(key + "->");
            }
            Console.WriteLine(count);
            Console.WriteLine(sb.ToString().TrimEnd(new char[]{'>', '-'}));
        }

        static bool Dfs(int v)
        {
            //if (dp[v]==1)
            //{
            //    return false;
            //}
            //if (dp[v] == 2)
            //{
            //    return true;
            //}
            if (v == visited.Length-1)
            {
                return true;
            }
            visited[v] = true;
            List<int> vList = graph[v];
            int bestChildIndex = -1;
            foreach (var n in vList)
            {
                if (!visited[n])
                {
                    if (Dfs(n))
                    {
                        if (bestChildIndex!=-1)
                        {
                            //dp[v] = 2;
                            visited[v] = false;
                            return true;
                        }
                        bestChildIndex = n;
                    }
                }
            }
            visited[v] = false;
            if (bestChildIndex==-1)
            {
                //dp[v] = 1;
                return false;
            }
            keys.Add(bestChildIndex);
            //dp[v] = 2;
            return true;
        }
    }
}
