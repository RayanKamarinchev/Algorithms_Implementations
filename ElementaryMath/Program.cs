List<List<int>> graph = new List<List<int>>();

int n = int.Parse(Console.ReadLine());

List<int> startNodes = new List<int>(n);
for (int i = 1; i <= n; i++)
{
    startNodes.Add(i);
}
graph.Add(startNodes);

List<Edge> results = new List<Edge>();

for (int i = 1; i <= n; i++)
{
    graph.Add(new List<int>());
    int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
    int a = numbers[0];
    int b = numbers[1];
    if (!resultsContainsValue(a+b, i))
    {
        results.Add(new Edge(a+b));
    }
    if (!resultsContainsValue(a - b, i))
    {
        results.Add(new Edge(a - b));
    }
    if (!resultsContainsValue(a * b, i))
    {
        results.Add(new Edge(a * b));
    }
}

var sinkPointer = new List<int>();
sinkPointer.Add(results.Count + n + 1);
for (int i = 0; i < results.Count; i++)
{
    foreach (var node in results[i].nodes)
    {
        graph[node].Add(i+n+1);
    }
    graph[i + n + 1] = sinkPointer;
}

int nodes = graph.Count;

bool resultsContainsValue(int value, int i)
{
    for (int j = 0; j < results.Count; j++)
    {
        if (results[j].value == value)
        {
            results[j].nodes.Add(i);
            return true;
        }
    }

    return false;
}

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

class Edge
{
    public int value;
    public List<int> nodes;

    public Edge(int value)
    {
        value = this.value;
        nodes = new List<int>();
    }
}