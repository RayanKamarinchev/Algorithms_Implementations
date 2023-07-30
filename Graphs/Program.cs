﻿using Graphs;

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