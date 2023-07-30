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
    }
}
