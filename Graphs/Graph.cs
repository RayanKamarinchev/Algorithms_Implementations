namespace Graphs
{
    public class Graph
    {
        private List<Tuple<int, int>>[] adj { get; set; }
        private int nodes;

        public Graph(int nodes)
        {
            nodes = nodes;
            adj = new List<Tuple<int, int>>[nodes];
            for (int i = 0; i < nodes; ++i)
                adj[i] = new List<Tuple<int, int>>();
        }
        
        public void addEdge(int u, int v, int w)
        {
            adj[u].Add(Tuple.Create(v, w));
            adj[v].Add(Tuple.Create(u, w));
        }

        public int[] shortedPath(int start)
        {
            var queue = new PriorityQueue<Tuple<int, int>>();
            var dist = Enumerable.Repeat(int.MaxValue, nodes).ToArray();
            
            queue.Enqueue(Tuple.Create(0, start));
            dist[start] = 0;

            while (queue.Count != 0)
            {
                int i = queue.Dequeue().Item2;
                foreach (var edge in adj[i])
                {
                    int node = edge.Item1;
                    int pathCost = edge.Item2;

                    if (dist[node] > dist[i] + pathCost)
                    {
                        // Updating distance of v
                        dist[node] = dist[i] + pathCost;
                        queue.Enqueue(Tuple.Create(dist[node], node));
                    }
                }
            }

            return dist;
        }
    }
}
