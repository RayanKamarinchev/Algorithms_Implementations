namespace DinicsAlgorithm
{
    public class FlowGraph
    {
        private List<List<(int,int)>> graph;
        private List<List<(int, int)>> flow;
        private int[] level;
        private int[] parent;
        private int nodes;

        public FlowGraph(int nodes)
        {
            this.nodes = nodes;
            graph = new List<List<(int,int)>>(nodes);
            level = new int[nodes];
            parent = new int[nodes];
        }

        public void AddEdge(int from, int to, int cap)
        {
            while (graph.Count <= from)
            {
                graph.Add(new List<(int, int)>());
            }
            graph[from].Add((to, cap));
        }

        private int getCap(List<List<(int, int)>> graph, int v, int i)
        {
            return graph[v].First(n => n.Item1 == i).Item2;
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
                        getCap(flow, v, i) < getCap(graph, v, i))
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
                if (level[i] == level[current] + 1 && getCap(flow, current, i) < getCap(graph, current, i))
                {
                    int curr_flow = Math.Min(flowTillNow, getCap(graph, current, i) - getCap(flow, current, i));
                    int temp_flow = DFS(i, curr_flow, sink);

                    if (temp_flow > 0)
                    {
                        int index1 = flow[current].FindIndex(n => n.Item1 == i);
                        flow[current][index1] = (flow[current][index1].Item1, temp_flow + flow[current][index1].Item2);

                        int index2 = flow[i].FindIndex(n => n.Item1 == current);
                        flow[i][index2] = (flow[i][index2].Item1, flow[i][index2].Item2 - temp_flow);
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
