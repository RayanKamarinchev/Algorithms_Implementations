﻿using System;
using System.Runtime.InteropServices;

public class EagerPrimsAdjacencyList
{
    int n;
    List<List<(int, int, int)>> graph;

    // Internal
    bool solved;
    bool mstExists;
    bool[] visited;
    MinIndexedDHeap<(int, int, int)> ipq;

    // Outputs
    long minCostSum;
    (int, int, int)[] mstEdges;

    public EagerPrimsAdjacencyList(List<List<(int, int, int)>> graph)
    {
        if (graph == null) throw new InvalidOperationException();
        this.n = graph.Count;
        this.graph = graph;
    }

    public (int, int, int)[] getMst()
    {
        solve();
        return mstExists ? mstEdges : null;
    }

    public long? getMstCost()
    {
        solve();
        return mstExists ? minCostSum : null;
    }

    private void relaxEdgesAtNode(int currentNodeIndex)
    {
        visited[currentNodeIndex] = true;

        // edges will never be null if the createEmptyGraph method was used to build the graph.
        List<(int, int, int)> edges = graph[currentNodeIndex];

        foreach ((int, int, int) edge in edges)
        {
            int destNodeIndex = edge.Item2;

            // Skip edges pointing to already visited nodes.
            if (visited[destNodeIndex]) continue;

            if (ipq.contains(destNodeIndex))
            {
                // Try and improve the cheapest edge at destNodeIndex with the current edge in the IPQ.
                ipq.decrease(destNodeIndex, edge);
            }
            else
            {
                // Insert edge for the first time.
                ipq.insert(destNodeIndex, edge);
            }
        }
    }

    // Computes the minimum spanning tree and minimum spanning tree cost.
    private void solve()
    {
        if (solved) return;
        solved = true;

        int m = n - 1, edgeCount = 0;
        visited = new bool[n];
        mstEdges = new (int, int, int)[m];

        // The degree of the d-ary heap supporting the IPQ can greatly impact performance, especially
        // on dense graphs. The base 2 logarithm of n is a decent value based on my quick experiments
        // (even better than E/V in many cases).
        int degree = (int)Math.Ceiling(Math.Log(n) / Math.Log(2));
        ipq = new MinIndexedDHeap<(int,int,int)>(Math.Max(2, degree), n);

        // Add initial set of edges to the priority queue starting at node 0.
        relaxEdgesAtNode(0);

        while (!ipq.isEmpty() && edgeCount != m)
        {
            int destNodeIndex = ipq.peekMinKeyIndex(); // equivalently: edge.to
            (int, int, int) edge = ipq.pollMinValue();

            mstEdges[edgeCount++] = edge;
            minCostSum += edge.Item3;

            relaxEdgesAtNode(destNodeIndex);
        }

        // Verify MST spans entire graph.
        mstExists = (edgeCount == m);
    }

    /* Graph construction helpers. */

    // Creates an empty adjacency list graph with n nodes.
    static List<List<(int, int, int)>> createEmptyGraph(int n)
    {
        List<List<(int, int, int)>> g = new List<List<(int, int, int)>>();
        for (int i = 0; i < n; i++) g.Add(new List<(int, int, int)>());
        return g;
    }

    static void addDirectedEdge(List<List<(int, int, int)>> g, int from, int to, int cost)
    {
        g[from].Add((from, to, cost));
    }

    static void addUndirectedEdge(List<List<(int, int, int)>> g, int from, int to, int cost)
    {
        addDirectedEdge(g, from, to, cost);
        addDirectedEdge(g, to, from, cost);
    }

    /* Example usage. */

    public static void main(String[] args)
    {
        eagerPrimsExampleFromSlides();
    }

    private static void eagerPrimsExampleFromSlides()
    {
        int n = 7;
        List<List<(int, int, int)>> g = createEmptyGraph(n);

        addDirectedEdge(g, 0, 2, 0);
        addDirectedEdge(g, 0, 5, 7);
        addDirectedEdge(g, 0, 3, 5);
        addDirectedEdge(g, 0, 1, 9);

        addDirectedEdge(g, 2, 0, 0);
        addDirectedEdge(g, 2, 5, 6);

        addDirectedEdge(g, 3, 0, 5);
        addDirectedEdge(g, 3, 1, -2);
        addDirectedEdge(g, 3, 6, 3);
        addDirectedEdge(g, 3, 5, 2);

        addDirectedEdge(g, 1, 0, 9);
        addDirectedEdge(g, 1, 3, -2);
        addDirectedEdge(g, 1, 6, 4);
        addDirectedEdge(g, 1, 4, 3);

        addDirectedEdge(g, 5, 2, 6);
        addDirectedEdge(g, 5, 0, 7);
        addDirectedEdge(g, 5, 3, 2);
        addDirectedEdge(g, 5, 6, 1);

        addDirectedEdge(g, 6, 5, 1);
        addDirectedEdge(g, 6, 3, 3);
        addDirectedEdge(g, 6, 1, 4);
        addDirectedEdge(g, 6, 4, 6);

        addDirectedEdge(g, 4, 1, 3);
        addDirectedEdge(g, 4, 6, 6);

        EagerPrimsAdjacencyList solver = new EagerPrimsAdjacencyList(g);
        long? cost = solver.getMstCost();

        if (cost == null)
        {
            Console.WriteLine("No MST does not exists");
        }
        else
        {
            Console.WriteLine("MST cost: " + cost);
            foreach (var e in solver.getMst())
            {
                Console.WriteLine($"from: {e.Item1}, to: {e.Item2}, cost: {e.Item3}");
            }
        }
    }

    public class MinIndexedDHeap<T>
        where T : IComparable
    {
        // Current number of elements in the heap.
        private int sz;

        // Maximum number of elements in the heap.
        private int N;

        // The degree of every node in the heap.
        private int D;

        // Lookup arrays to track the child/parent indexes of each node.
        private int[] child, parent;

        // The Position Map (pm) maps Key Indexes (ki) to where the position of that
        // key is represented in the priority queue in the domain [0, sz).
        public int[] pm;

        // The Inverse Map (im) stores the indexes of the keys in the range
        // [0, sz) which make up the priority queue. It should be noted that
        // 'im' and 'pm' are inverses of each other, so: pm[im[i]] = im[pm[i]] = i
        public int[] im;

        // The values associated with the keys. It is very important  to note
        // that this array is indexed by the key indexes (aka 'ki').
        public T?[] values;

        // Initializes a D-ary heap with a maximum capacity of maxSize.
        public MinIndexedDHeap(int degree, int maxSize)
        {
            if (maxSize <= 0) throw new InvalidOperationException("maxSize <= 0");

            D = Math.Max(2, degree);
            N = Math.Max(D + 1, maxSize);

            im = new int[N];
            pm = new int[N];
            child = new int[N];
            parent = new int[N];
            values = new T?[N];

            for (int i = 0; i < N; i++)
            {
                parent[i] = (i - 1) / D;
                child[i] = i * D + 1;
                pm[i] = im[i] = -1;
            }
        }

        public int size()
        {
            return sz;
        }

        public bool isEmpty()
        {
            return sz == 0;
        }

        public bool contains(int ki)
        {
            keyInBoundsOrThrow(ki);
            return pm[ki] != -1;
        }

        public int peekMinKeyIndex()
        {
            isNotEmptyOrThrow();
            return im[0];
        }

        public int pollMinKeyIndex()
        {
            int minki = peekMinKeyIndex();
            delete(minki);
            return minki;
        }
        

        public T peekMinValue()
        {
            isNotEmptyOrThrow();
            return (T)values[im[0]];
        }

        public T pollMinValue()
        {
            T minValue = peekMinValue();
            delete(peekMinKeyIndex());
            return minValue;
        }

        public void insert(int ki, T value)
        {
            if (contains(ki)) throw new InvalidOperationException("index already exists; received: " + ki);
            valueNotNullOrThrow(value);
            pm[ki] = sz;
            im[sz] = ki;
            values[ki] = value;
            swim(sz++);
        }
        

        public T valueOf(int ki)
        {
            keyExistsOrThrow(ki);
            return (T)values[ki];
        }
        

        public T delete(int ki)
        {
            keyExistsOrThrow(ki);
            int i = pm[ki];
            swap(i, --sz);
            sink(i);
            swim(i);
            T value = (T)values[ki];
            values[ki] = default(T);
            pm[ki] = -1;
            im[sz] = -1;
            return value;
        }
        

        public T update(int ki, T value)
        {
            keyExistsAndValueNotNullOrThrow(ki, value);
            int i = pm[ki];
            T oldValue = (T)values[ki];
            values[ki] = value;
            sink(i);
            swim(i);
            return oldValue;
        }

        // Strictly decreases the value associated with 'ki' to 'value'
        public void decrease(int ki, T value)
        {
            keyExistsAndValueNotNullOrThrow(ki, value);
            if (less(value, values[ki]))
            {
                values[ki] = value;
                swim(pm[ki]);
            }
        }

        // Strictly increases the value associated with 'ki' to 'value'
        public void increase(int ki, T value)
        {
            keyExistsAndValueNotNullOrThrow(ki, value);
            if (less(values[ki], value))
            {
                values[ki] = value;
                sink(pm[ki]);
            }
        }

        /* Helper functions */

        private void sink(int i)
        {
            for (int j = minChild(i); j != -1;)
            {
                swap(i, j);
                i = j;
                j = minChild(i);
            }
        }

        private void swim(int i)
        {
            while (less(i, parent[i]))
            {
                swap(i, parent[i]);
                i = parent[i];
            }
        }

        // From the parent node at index i find the minimum child below it
        private int minChild(int i)
        {
            int index = -1, from = child[i], to = Math.Min(sz, from + D);
            for (int j = from; j < to; j++)
                if (less(j, i))
                    index = i = j;
            return index;
        }

        private void swap(int i, int j)
        {
            pm[im[j]] = i;
            pm[im[i]] = j;
            int tmp = im[i];
            im[i] = im[j];
            im[j] = tmp;
        }

        // Tests if the value of node i < node j

        private bool less(int i, int j)
        {
            return values[im[i]].CompareTo(values[im[j]]) < 0;
        }
        
        private bool less(T obj1, T obj2)
        {
            return obj1.CompareTo(obj2) < 0;
        }

        public override string ToString()
        {
            List<int> lst = new List<int>(sz);
            for (int i = 0; i < sz; i++) lst.Add(im[i]);
            return lst.ToString();
        }
        /* Helper functions to make the code more readable. */

        private void isNotEmptyOrThrow()
        {
            if (isEmpty()) throw new Exception("Priority queue underflow");
        }

        private void keyExistsAndValueNotNullOrThrow(int ki, Object value)
        {
            keyExistsOrThrow(ki);
            valueNotNullOrThrow(value);
        }

        private void keyExistsOrThrow(int ki)
        {
            if (!contains(ki)) throw new Exception("Index does not exist; received: " + ki);
        }

        private void valueNotNullOrThrow(Object value)
        {
            if (value == null) throw new InvalidOperationException("value cannot be null");
        }

        private void keyInBoundsOrThrow(int ki)
        {
            if (ki < 0 || ki >= N)
                throw new InvalidOperationException("Key index out of bounds; received: " + ki);
        }

        /* Test functions */

        // Recursively checks if this heap is a min heap. This method is used
        // for testing purposes to validate the heap invariant.
        public bool isMinHeap()
        {
            return isMinHeap(0);
        }

        private bool isMinHeap(int i)
        {
            int from = child[i], to = Math.Min(sz, from + D);
            for (int j = from; j < to; j++)
            {
                if (!less(i, j)) return false;
                if (!isMinHeap(j)) return false;
            }

            return true;
        }
    }
}