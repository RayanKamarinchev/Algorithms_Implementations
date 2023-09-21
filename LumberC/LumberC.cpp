#include <iostream>
#include <vector>
#include <algorithm>

class Rect {
public:
    int Left, Top, right, bottom;
    Rect(){}
    Rect(int left, int top, int right, int bottom) : Left(left), Top(top), right(right), bottom(bottom) {}
};

class Graph {
private:
    int V;
    std::vector<std::vector<int>> adjListArray;
    std::vector<std::vector<int>> res;
    std::vector<int> list;

public:
    Graph(int V) : V(V) {
        adjListArray.resize(V);
    }

    void addEdge(int src, int dest) {
        adjListArray[src].push_back(dest);
        adjListArray[dest].push_back(src);
    }

    void DFSUtil(int v, std::vector<bool>& visited) {
        visited[v] = true;
        list.push_back(v);
        for (int x : adjListArray[v]) {
            if (!visited[x]) {
                DFSUtil(x, visited);
            }
        }
    }

    std::vector<std::vector<int>> connectedComponents() {
        std::vector<bool> visited(V, false);
        for (int v = 0; v < V; ++v) {
            if (!visited[v]) {
                DFSUtil(v, visited);
                res.push_back(list);
                list.clear();
            }
        }
        return res;
    }
};

int main() {
    int n, m;
    std::cin >> n >> m;

    Graph g(n);
    std::vector<Rect> list(n);

    for (int i = 0; i < n; i++) {
        int left, top, right, bottom;
        std::cin >> left >> top >> right >> bottom;
        list[i] = Rect(left, top, right, bottom);

        for (int j = 0; j < i; j++) {
            Rect r1 = list[i];
            Rect r2 = list[j];
            if (!(r1.Left > r2.right) && !(r1.right < r2.Left) &&
                !(r1.Top < r2.bottom) && !(r1.bottom > r2.Top)) {
                g.addEdge(i, j);
            }
        }
    }

    std::vector<std::vector<int>> res = g.connectedComponents();
    std::vector<int> groups(n);

    for (int i = 0; i < res.size(); i++) {
        for (int item : res[i]) {
            groups[item] = i;
        }
    }

    for (int i = 0; i < m; i++) {
        int comp1, comp2;
        std::cin >> comp1 >> comp2;
        std::cout << (groups[comp1 - 1] == groups[comp2 - 1] ? "YES" : "NO") << std::endl;
    }

    return 0;
}