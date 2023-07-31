int[,] graph = new int[,] {
    { 0, 16, 13, 0, 0, 0 },
    { 0, 0, 10, 12, 0, 0 },
    { 0, 4, 0, 0, 14, 0 },
    { 0, 0, 9, 0, 0, 20 },
    { 0, 0, 0, 7, 0, 4 },
    { 0, 0, 0, 0, 0, 0 }
};
int nodes = 6;

Console.WriteLine(findMaxFlow(graph, 0, 5));

int findMaxFlow(int[,] graph, int source, int sink)
{
    //create graph with capacities
    int[,] rGraph = new int[nodes, nodes];
    for (int i = 0; i < nodes; i++)
    {
        for (int j = 0; j < nodes; j++)
        {
            rGraph[i, j] = graph[i, j];
        }
    }

    //Store path
    int[] parent = new int[nodes];
    int maxFlow = 0;

    while (Bfs(rGraph, source, sink, parent))
    {
        //backwards, find the bottleneck (the lowest value)
        int pathFlow = int.MaxValue;
        for (int i = sink; i != source; i = parent[i])
        {
            int j = parent[i];
            pathFlow = Math.Min(pathFlow, rGraph[j, i]);
        }

        for (int i = sink; i != source; i = parent[i])
        {
            int j = parent[i];
            rGraph[j, i] -= pathFlow;
            rGraph[i, j] += pathFlow;
        }

        maxFlow += pathFlow;
    }
    return maxFlow;
}

bool Bfs(int[,] graph, int source, int sink, int[] parent)
{
    bool[] visited = new bool[nodes];
    Queue<int> queue = new Queue<int>();
    queue.Enqueue(source);
    visited[source] = true;
    parent[source] = -1;

    while (queue.Count > 0)
    {
        int current = queue.Dequeue();
        for (int j = 0; j < nodes; j++)
        {
            if (graph[current, j] > 0 && !visited[j])
            {
                if (j == sink)
                {
                    parent[j] = current;
                    return true;
                }
                queue.Enqueue(j);
                parent[j] = current;
                visited[j] = true;
            }
        }
    }

    return false;
}