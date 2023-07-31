namespace DinicsAlgorithm
{
    public class FlowGraph
    {
        private int[,] graph;
        private int[,] flow;
        private int[] level;
        private int[] parent;
        private int nodes;

        public FlowGraph(int nodes)
        {
            this.nodes = nodes;
            graph = new int[nodes, nodes];
            level = new int[nodes];
            parent = new int[nodes];
        }

        public void AddEdge(int from, int to, int cap)
        {
            graph[from, to] += cap;
        }

        private bool BFS(int source, int sink)
        {
            Array.Fill(level, -1);
            level[source] = 0;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count != 0)
            {
                int v = queue.Dequeue();
                for (int i = 0; i < nodes; i++)
                {
                    //checks if unvisited and if flow can go through
                    if (level[i] < 0 && 
                        flow[v, i] < graph[v, i])
                    {
                        queue.Enqueue(i);
                        level[i] = level[v] + 1;
                    }
                }
            }

            return level[sink] >= 0;
        }

        private int DFS(int current, int flowTillNow, int sink)
        {
            if (current == sink)
                return flowTillNow;

            while (parent[current] < nodes)
            {
                int i = parent[current]++;
                if (level[i] == level[current] + 1 && flow[current, i] < graph[current, i])
                {
                    int curr_flow = Math.Min(flowTillNow, graph[current, i] - flow[current, i]);
                    int temp_flow = DFS(i, curr_flow, sink);

                    if (temp_flow > 0)
                    {
                        flow[current, i] += temp_flow;
                        flow[i, current] -= temp_flow;
                        return temp_flow;
                    }
                }
            }

            return 0;
        }

        public int MaxFlow(int source, int sink)
        {
            if (source == sink)
                return -1;

            int total = 0;

            while (BFS(source, sink))
            {
                Array.Fill(parent, 0);
                while (true)
                {
                    int flow = DFS(source, int.MaxValue, sink);
                    if (flow == 0)
                        break;
                    total += flow;
                }
            }

            return total;
        }
    }
}
