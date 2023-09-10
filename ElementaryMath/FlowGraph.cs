using Index = ElementaryMath.Index;

namespace DinicsAlgorithm
{
    public class FlowGraph
    {
        private List<List<Index>> graph;
        private List<List<(Index, int)>> flow;
        private int[] level;
        private int[] parent;
        private int nodes;

        public FlowGraph(int nodes)
        {
            this.nodes = nodes;
            graph = new List<List<Index>>(nodes);
            level = new int[nodes];
            parent = new int[nodes];
        }

        public void AddEdge(Index from, Index to)
        {
            if (graph.Count <= from.index)
            {
                graph.Add(new List<Index>());
            }
            graph[from.index].Add(to);
        }

        private int getCap(int v, int i)
        {
            return flow[v].First(n => n.Item1.index == i).Item2;
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
                    if (level[i] < 0 && getCap(v, i) < 1)
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
                if (level[i] == level[current] + 1 && getCap(current, i) < 1)
                {
                    int curr_flow = Math.Min(flowTillNow, 1 - getCap(current, i));
                    int temp_flow = DFS(i, curr_flow, sink);

                    if (temp_flow > 0)
                    {
                        int index1 = flow[current].FindIndex(n => n.Item1.index == i);
                        flow[current][index1] = (flow[current][index1].Item1, temp_flow + flow[current][index1].Item2);

                        int index2 = flow[i].FindIndex(n => n.Item1.index == current);
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
