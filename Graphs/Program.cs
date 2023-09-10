using Graphs;

UGraph unweightedGraph = new UGraph();
unweightedGraph.AdjacencyList = new List<List<int>>()
{
    new() {9, 7, 11},
    new() {10, 8},
    new() {12, 3},
    new() {2, 4, 7},
    new() {3},
    new() {6},
    new() {7, 5},
    new () {0, 11, 6, 3},
    new() {9, 1, 12},
    new() {0, 10, 8},
    new() {9, 1},
    new() {0, 7},
    new() {8, 2},
};
var res = unweightedGraph.Bfs();
Console.WriteLine(string.Join(", ", res));

UGraph unweightedGraph2 = new UGraph();
unweightedGraph2.AdjacencyList = new List<List<int>>()
{
    new() {1},
    new() {2},
    new() {0},
    new() {4, 7},
    new() {5},
    new() {6, 0},
    new() {4,0,2},
    new () {5,3}
};
var res2 = unweightedGraph2.GetLowLinkValues();
Console.WriteLine(string.Join(", ", res2));

Graph graphForPrim = new Graph(5);
graphForPrim.addEdge(0, 1, 2);
graphForPrim.addEdge(0, 3, 6);
graphForPrim.addEdge(1, 2, 3);
graphForPrim.addEdge(1, 3, 8);
graphForPrim.addEdge(1, 4, 5);
graphForPrim.addEdge(2, 4, 7);
graphForPrim.addEdge(3, 4, 9);

List<(int, int)> minimumSpanningTree = graphForPrim.FindMST();

Console.WriteLine("Minimum Spanning Tree Edges:");
foreach (var edge in minimumSpanningTree)
{
    Console.WriteLine($"{edge.Item1} - {edge.Item2}");
}