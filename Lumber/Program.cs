using System;
using System.Collections.Generic;
using System.Linq;

namespace Lumber
{
    class Graph
    {
        int V;
        int i = 1;
        List<List<int>> adjListArray;
        public int[] groups;

        // constructor
        public Graph(int V)
        {
            this.V = V;
            groups = new int[V];

            // define the size of array as
            // number of vertices
            adjListArray = new List<List<int>>();

            // Create a new list for each vertex
            // such that adjacent nodes can be stored
            for (int i = 0; i < V; i++)
            {
                adjListArray.Add(new List<int>());
            }
        }

        // Adds an edge to an undirected graph
        public void addEdge(int src, int dest)
        {
            // Add an edge from src to dest.
            adjListArray[src].Add(dest);

            // Since graph is undirected, add an edge from dest
            // to src also
            adjListArray[dest].Add(src);
        }

        void DFSUtil(int v)
        {
            // Mark the current node as visited and print it
            groups[v] = i;
            // Recur for all the vertices
            // adjacent to this vertex
            foreach (int x in adjListArray[v])
            {
                if (groups[x] == 0)
                    DFSUtil(x);
            }
        }

        public void connectedComponents()
        {
            // Mark all the vertices as not visited
            for (int v = 0; v < V; ++v)
            {
                if (groups[v] == 0)
                {
                    // print all reachable vertices
                    // from v
                    DFSUtil(v);
                    i++;
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //30min and 25min real
            var fin = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = fin[0];
            int m = fin[1];
            Graph g = new Graph(n);

            Rect[] list = new Rect[n];
            int[] input;
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                list[i] = new Rect(input[0], input[1], input[2], input[3]);
                Rect r1;
                Rect r2;
                for (int j = 0; j < i; j++)
                {
                    r1 = list[i];
                    r2 = list[j];
                    if (!(r1.Left > r2.right) &&
                        !(r1.right < r2.Left) &&
                        !(r1.Top < r2.bottom) &&
                        !(r1.bottom > r2.Top))
                    {
                        g.addEdge(i, j);
                    }
                }
            }

            GC.Collect();
            g.connectedComponents();
            int[] groups = g.groups;
            GC.Collect();
            int[] comp;
            for (int i = 0; i < m; i++)
            {
                comp = Console.ReadLine().Split().Select(int.Parse).ToArray();
                Console.WriteLine(groups[comp[0] - 1] == groups[comp[1] - 1] ? "YES" : "NO");
            }
        }
    }

    class Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }

        public Rect(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }
}
