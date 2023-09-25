namespace _10DurinsLabyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static int timer = 1;
        private static int nodesCount;
        private static int[] depth;
        private static int[] lowpoint;
        private static int[] graphParents;
        private static int[] blockCutTreeParents;
        private static int[] graphIndexesToBlockCutTreeIndexes;
        private static bool[] articulationPoints;
        private static List<int> blockCutTreeArticulationPointIndexesToGraphIndexes;
        private static Stack<int> dfsStack;

        private static List<List<int>> biconnectedComponents;
        private static List<List<int>> blockCutTree;
        private static List<int>[] graph;

        // Steps of the solution:
        // 1. Find biconnected components and articulation points of the graph (this can be done as a single step)
        // 2. Create a new block-cut tree - compressed version of the graph where each node is either a biconnected component 
        // or an articulation point - read more here https://en.wikipedia.org/wiki/Biconnected_component#Block-cut_tree
        // 3. Run Bfs on the block-cut tree to find a route from start to end
        // 4. All articulation points that are part of that route are the result
        // Note that the graph may contain articulation points that are NOT part of that path
        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            graph = new List<int>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] parameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var parent = parameters[0];
                var child = parameters[1];

                graph[parent].Add(child);
                graph[child].Add(parent);
            }

            depth = new int[nodesCount];
            lowpoint = new int[nodesCount];
            graphParents = new int[nodesCount];
            articulationPoints = new bool[nodesCount];
            graphIndexesToBlockCutTreeIndexes = new int[nodesCount];
            dfsStack = new Stack<int>();
            biconnectedComponents = new List<List<int>>();
            blockCutTree = new List<List<int>>();
            blockCutTreeArticulationPointIndexesToGraphIndexes = new List<int>();

            FindBiconnectedComponents(0);
            CreateBlockCutTree();
            FindPathInBlockCutTree();

            List<int> path = new List<int>();
            var blockCutTreeEndingNode = graphIndexesToBlockCutTreeIndexes[nodesCount - 1];
            var currentNode = blockCutTreeEndingNode;
            while (blockCutTreeParents[currentNode] != -2)
            {
                // If the blockCutTree index is an articulation point -> put the original index
                // of the articulation point (in the original graph) in the path
                // Since the first nodes we constructed in the block-cut tree were the articulation points, we know
                // that they hold the indexes 0...N (the lowest indexes) in the block-cut tree, so if a node in the path has an 
                // index X < blockCutTreeArticulationPointIndexesToGraphIndexes.Count, then it must be an articulation point
                if (blockCutTreeArticulationPointIndexesToGraphIndexes.Count > blockCutTreeParents[currentNode])
                {
                    var articulationPointOriginalIndex =
                        blockCutTreeArticulationPointIndexesToGraphIndexes[blockCutTreeParents[currentNode]];

                    if (articulationPointOriginalIndex != 0 && articulationPointOriginalIndex != nodesCount - 1)
                    {
                        path.Add(blockCutTreeArticulationPointIndexesToGraphIndexes[blockCutTreeParents[currentNode]]);
                    }
                }

                currentNode = blockCutTreeParents[currentNode];
            }

            path.Reverse();
            Console.WriteLine(path.Sum());
            Console.WriteLine(string.Join("->", path));
        }

        static void FindBiconnectedComponents(int node)
        {
            depth[node] = timer;
            lowpoint[node] = timer;

            //Permanently increasing the depth allows us to differentiate 2 children of a node based on traversal order
            timer++;
            dfsStack.Push(node);

            foreach (var child in graph[node])
            {
                if (graphParents[node] != child)
                {
                    //Child is not visited
                    if (depth[child] == 0)
                    {
                        graphParents[child] = node;
                        FindBiconnectedComponents(child);
                        lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);

                        if (lowpoint[child] >= depth[node])
                        {
                            // The current node is an articulation point if :
                            // 1. (depth[node] > 1) => it's not the starting node
                            // 2. (depth[child] > 2) => it is the starting node and has at least 2 children in different components of the graph
                            // Note that root can have multiple children with depth > 2, but this is not a problem as this point in the code
                            // will never be hit with node == root unless at least one of those children is NOT reachable from the others
                            articulationPoints[node] = (depth[node] > 1) || (depth[child] > 2);
                            biconnectedComponents.Add(new List<int>() { node });
                            while (biconnectedComponents.Last().Last() != child)
                            {
                                biconnectedComponents.Last().Add(dfsStack.Pop());
                            }
                        }
                    }
                    else
                    {
                        lowpoint[node] = Math.Min(lowpoint[node], depth[child]);
                    }
                }
            }
        }

        // Create a new graph where all biconnected components are compressed into single nodes so the new graph is 
        // a smaller version of the original graph where nodes are either biconnected components or articulation points
        static void CreateBlockCutTree()
        {
            graphIndexesToBlockCutTreeIndexes = new int[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                if (articulationPoints[i])
                {
                    graphIndexesToBlockCutTreeIndexes[i] = blockCutTree.Count;
                    blockCutTreeArticulationPointIndexesToGraphIndexes.Add(i);
                    blockCutTree.Add(new List<int>());
                }
            }

            foreach (var biconnectedComponent in biconnectedComponents)
            {
                int blockCutTreeNode = blockCutTree.Count;
                blockCutTree.Add(new List<int>());
                foreach (var node in biconnectedComponent)
                {
                    if (!articulationPoints[node])
                    {
                        graphIndexesToBlockCutTreeIndexes[node] = blockCutTreeNode;
                    }
                    else
                    {
                        var articulationPointBlockCutTreeNode = graphIndexesToBlockCutTreeIndexes[node];
                        blockCutTree[articulationPointBlockCutTreeNode].Add(blockCutTreeNode);
                        blockCutTree[blockCutTreeNode].Add(articulationPointBlockCutTreeNode);
                    }
                }
            }
        }

        // The constraints specify that there will always be a path from antechamber to treasury room
        static void FindPathInBlockCutTree()
        {
            // Run Bfs over block Cut Tree to find the shortest path between starting node and ending node
            blockCutTreeParents = new int[blockCutTree.Count];
            for (int i = 0; i < blockCutTreeParents.Length; i++)
            {
                blockCutTreeParents[i] = -1;
            }
            var queue = new Queue<int>();
            var blockCutTreeStartingNode = graphIndexesToBlockCutTreeIndexes[0];
            var blockCutTreeEndingNode = graphIndexesToBlockCutTreeIndexes[nodesCount - 1];
            queue.Enqueue(blockCutTreeStartingNode);
            blockCutTreeParents[blockCutTreeStartingNode] = -2;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == blockCutTreeEndingNode)
                {
                    return;
                }

                foreach (var child in blockCutTree[node])
                {
                    if (blockCutTreeParents[child] == -1)
                    {
                        blockCutTreeParents[child] = node;
                        queue.Enqueue(child);
                    }
                }
            }
        }
    }
}
