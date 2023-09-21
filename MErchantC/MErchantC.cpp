#include <iostream>
#include <vector>
#include <list>
#include <stack>
#include <algorithm>

class Graph {
private:
    int V;
    std::vector<std::list<int>> adj;
    int Time;
    std::vector<int> cycles;

public:
    Graph(int V) {
        this->V = V;
        adj.resize(V);
        Time = 0;
    }

    void AddEdge(int v, int w) {
        adj[v].push_back(w);
    }

    void SCCUtil(int u, std::vector<int>& low, std::vector<int>& disc, std::vector<bool>& stackMember, std::stack<int>& st) {
        disc[u] = Time;
        low[u] = Time;
        Time += 1;
        stackMember[u] = true;
        st.push(u);

        int n;

        for (int i : adj[u]) {
            n = i;

            if (disc[n] == -1) {
                SCCUtil(n, low, disc, stackMember, st);
                low[u] = std::min(low[u], low[n]);
            }
            else if (stackMember[n] == true) {
                low[u] = std::min(low[u], disc[n]);
            }
        }

        int w = -1;
        if (low[u] == disc[u]) {
            std::vector<int> cycle;
            while (w != u) {
                w = st.top();
                st.pop();
                cycle.push_back(w);
                stackMember[w] = false;
            }

            if (cycle.size() > 1) {
                cycles.insert(cycles.end(), cycle.begin(), cycle.end());
            }
        }
    }

    void SCC() {
        std::vector<int> disc(V, -1);
        std::vector<int> low(V, -1);
        std::vector<bool> stackMember(V, false);
        std::stack<int> st;

        for (int i = 0; i < V; i++) {
            if (disc[i] == -1) {
                SCCUtil(i, low, disc, stackMember, st);
            }
        }
    }

    std::pair<bool, long long> BFS(int s) {
        std::stack<int> queue;
        bool cycleExists = false;
        int cycleNode = -1;
        long long count = 0;
        queue.push(s);

        while (!queue.empty()) {
            s = queue.top();
            queue.pop();

            if (s == V - 1) {
                if (cycleNode != -1) {
                    return { true, -1 };
                }
                count++;
            }

            if (s == cycleNode) {
                cycleNode = -1;
                continue;
            }

            if (std::find(cycles.begin(), cycles.end(), s) != cycles.end() && cycleNode == -1) {
                cycleNode = s;
                cycleExists = true;
            }

            for (int val : adj[s]) {
                queue.push(val);
            }
        }

        return { cycleExists, count };
    }
};

int main() {
    int V, E;
    std::cin >> V >> E;

    Graph g(V);

    for (int i = 0; i < E; i++) {
        int v, w;
        std::cin >> v >> w;
        g.AddEdge(v - 1, w - 1);
    }

    g.SCC();

    std::pair<bool, long long> result = g.BFS(0);

    if (result.second == -1) {
        std::cout << "infinite" << std::endl;
    }
    else {
        std::cout << result.second << " " << (result.first ? "yes" : "no") << std::endl;
    }

    return 0;
}