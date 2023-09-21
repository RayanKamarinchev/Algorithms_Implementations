using System;
using System.Collections.Generic;
using System.Linq;

namespace Merchants
{
    class Graph
    {

        // No. of vertices
        private int V;

        // Adjacency Lists
        LinkedList<int>[] _adj;
        private int Time;
        private List<int> cycles = new List<int>();
        private Dictionary<int, long> dp;
        int cycleNode = -1;
        long count = 0;
        public bool cycleExists = false;

        public Graph(int V)
        {
            _adj = new LinkedList<int>[V];
            for (int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new LinkedList<int>();
            }
            dp = new Dictionary<int, long>
            {
                { V - 1, 1 }
            };

            this.V = V;
            Time = 0;
        }

        // Function to add an edge into the graph
        public void AddEdge(int v, int w)
        {
            _adj[v].AddLast(w);
        }
        private void SCCUtil(int u, int[] low, int[] disc,
                 bool[] stackMember, Stack<int> st)
        {

            // Initialize discovery time and low value
            disc[u] = Time;
            low[u] = Time;
            Time += 1;
            stackMember[u] = true;
            st.Push(u);

            int n;

            // Go through all vertices adjacent to this
            foreach (int i in _adj[u])
            {
                n = i;

                if (disc[n] == -1)
                {
                    SCCUtil(n, low, disc, stackMember, st);

                    // Check if the subtree rooted with v
                    // has a connection to one of the
                    // ancestors of u
                    // Case 1 (per above discussion on
                    // Disc and Low value)
                    low[u] = Math.Min(low[u], low[n]);
                }
                else if (stackMember[n] == true)
                {
                    // Update low value of 'u' only if 'v' is
                    // still in stack (i.e. it's a back edge,
                    // not cross edge).
                    // Case 2 (per above discussion on Disc
                    // and Low value)
                    low[u] = Math.Min(low[u], disc[n]);
                }
            }

            // head node found, pop the stack and print an SCC
            // To store stack extracted vertices
            int w = -1;
            if (low[u] == disc[u])
            {
                List<int> cycle = new List<int>();
                while (w != u)
                {
                    w = st.Pop();

                    cycle.Add(w);
                    stackMember[w] = false;
                }

                if (cycle.Count > 1)
                {
                    cycles.AddRange(cycle);
                }
            }
        }

        // The function to do DFS traversal.
        // It uses SCCUtil()
        public void SCC()
        {
            // Mark all the vertices as not visited
            // and Initialize parent and visited,
            // and ap(articulation point) arrays
            int[] disc = new int[V];
            int[] low = new int[V];
            for (int i = 0; i < V; i++)
            {
                disc[i] = -1;
                low[i] = -1;
            }

            bool[] stackMember = new bool[V];
            Stack<int> st = new Stack<int>();
            // Call the recursive helper function
            // to find strongly connected components
            for (int i = 0; i < V; i++)
                if (disc[i] == -1)
                    SCCUtil(i, low, disc, stackMember, st);
        }

        // Prints BFS traversal from a given source s
        public long BFS(int s)
        {
            if (dp.TryGetValue(s, out var value))
            {
                if (cycleNode != -1)
                {
                    return -1;
                }

                return value;
            }

            if (s == cycleNode)
            {
                cycleNode = -1;
                return 0;
            }
            if (cycles.Contains(s) && cycleNode == -1)
            {
                cycleNode = s;
                cycleExists = true;
            }

            long res = 0;
            foreach (var val in _adj[s].OrderByDescending(x => cycles.Contains(x)))
            {
                long v = BFS(val);
                if (v == -1)
                {
                    return -1;
                }
                res += v;
            }
            dp.Add(s, res);
            return res;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //9:28 45min 11:02 resume
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Graph g = new Graph(input[0]);
            for (int i = 0; i < input[1]; i++)
            {
                var path = Console.ReadLine().Split().Select(int.Parse).ToArray();
                g.AddEdge(path[0]-1, path[1]-1);
            }

            g.SCC();
            long count = g.BFS(0);
            if (count == -1)
            {
                Console.WriteLine("infinite");
            }
            else
            {
                Console.WriteLine($"{count%9000000000} " + (g.cycleExists ? "yes" : "no"));
            }
        }
    }
}
