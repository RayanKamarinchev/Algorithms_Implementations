namespace Graphs
{
    //Unweighted graph
    public class UGraph
    {
        public List<List<int>> AdjacencyList { get; set; }

        public Tuple<int, int[]> FindConnectedComponents()
        {
            int n = AdjacencyList.Count;
            int[] components = new int[n];
            bool[] visited = new bool[n];
            int count = 0;

            //Depth first search
            void Dfs(int index)
            {
                visited[index] = true;
                components[index] = count;
                foreach (var next in AdjacencyList[index])
                {
                    if (!visited[next])
                    {
                        Dfs(next);
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    count++;
                    Dfs(i);
                }
            }

            return new Tuple<int, int[]>(count, components);
        }

        public List<int> Bfs()
        {
            int n = AdjacencyList.Count;
            List<int> res = new List<int>();
            Queue<int> q = new Queue<int>();
            bool[] visited = new bool[n];

            q.Enqueue(0);
            while (q.Count > 0)
            {
                int current = q.Dequeue();
                visited[current] = true;
                foreach (var child in AdjacencyList[current])
                {
                    if (!visited[child] && !q.Contains(child))
                    {
                        q.Enqueue(child);
                    }
                }
                res.Add(current);
            }
            return res;
        }

        //Tarjans strongly connected components algorithm
        public int[] GetLowLinkValues()
        {
            int n = AdjacencyList.Count;
            int id = 0;
            int sccCount = 0;

            int[] ids = new int[n];
            int[] low = new int[n];
            bool[] onStack = new bool[n];
            Stack<int> stack = new Stack<int>();

            int[] findSccs()
            {
                for (int i = 0; i < n; i++)
                {
                    ids[i] = -1;
                }

                for (int i = 0; i < n; i++)
                {
                    if (ids[i] == -1)
                    {
                        Dfs(i);
                    }
                }

                return low;
            }

            void Dfs(int at)
            {
                stack.Push(at);
                onStack[at] = true;
                ids[at] = id;
                low[at] = id;
                id++;

                //visit all neighbours
                foreach (var next in AdjacencyList[at])
                {
                    if (ids[next] == -1)
                    {
                        Dfs(next);
                    }

                    if (onStack[next])
                    {
                        low[at] = Math.Min(low[next], low[at]);
                    }
                }

                //at start
                if (ids[at] == low[at])
                {
                    while (true)
                    {
                        var node = stack.Pop();
                        onStack[node] = false;
                        low[node] = ids[at];
                        if (node == at)
                        {
                            break;
                        }
                    }

                    sccCount++;
                }
            }

            return findSccs();
        }
        public List<int> FindBridges()
        {
            int id = 0;
            int nodes = AdjacencyList.Count;
            int[] ids = new int[nodes];
            int[] low = new int[nodes];
            bool[] visited = new bool[nodes];

            void Dfs(int at, int parent, List<int> bridges)
            {
                visited[at] = true;
                id++;
                ids[at] = id;
                low[at] = id;

                foreach (var next in AdjacencyList[at])
                {
                    if (next == parent)
                        continue;
                    if (!visited[next])
                    {
                        Dfs(next, at, bridges);
                        low[at] = Math.Min(low[at], low[next]);
                        if (low[at] < low[next])
                        {
                            bridges.Add(at);
                            bridges.Add(next);
                        }
                    }
                    else
                        low[at] = Math.Max(low[at], ids[next]);
                }
            }

            var bridges = new List<int>();
            for (int i = 0; i < nodes; i++)
            {
                if (!visited[i])
                {
                    Dfs(i, -1, bridges);
                }
            }

            return bridges;
        }

        public List<int> FindEulerianPath()
        {
            int nodes = AdjacencyList.Count;
            int edges = AdjacencyList.Sum(n => n.Count());

            int[] inEdges = new int[nodes];
            int[] outEdges = new int[nodes];
            List<int> path = new List<int>();

            countInOutEdges();
            if (!EulerianPathExists())
            {
                return null;
            }

            Dfs(getStartNode());
            if (path.Count == edges + 1)
            {
                return path;
            }

            return null;

            void countInOutEdges()
            {
                foreach (var edges in AdjacencyList)
                {
                    for (int i = 0; i < edges.Count; i++)
                    {
                        outEdges[i]++;
                        inEdges[edges[i]]++;
                    }
                }
            }

            bool EulerianPathExists()
            {
                int startNodes = 0;
                int endNodes = 0;
                for (int i = 0; i < nodes; i++)
                {
                    if (Math.Abs(inEdges[i] - outEdges[i]) > 1)
                    {
                        return false;
                    }
                    else if (inEdges[i] - outEdges[i] == 1)
                    {
                        endNodes++;
                    }
                    else if (outEdges[i] - inEdges[i] == 1)
                    {
                        startNodes++;
                    }
                }
                return (endNodes==0 && startNodes==0) || (endNodes == 1 && startNodes == 1);
            }

            int getStartNode()
            {
                int start = 0;
                for (int i = 0; i < nodes; i++)
                {
                    if (outEdges[i] - inEdges[i] == 1)
                        return i;
                    if (outEdges[i] > 0)
                        start = i;
                }
                return start;
            }


            void Dfs(int at)
            {
                while (outEdges[at] != 0)
                {
                    var nextEdge = AdjacencyList[at][--outEdges[at]];
                    Dfs(nextEdge);
                }
                path.Add(at);
            }
        }
    } 
}
