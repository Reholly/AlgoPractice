using AlgoPractise.AlgorithmsForNikolaev.Algorithms;
using AlgoPractise.AlgorithmsForNikolaev.Models;


//В матрицах смежности всегда будут на i =j индексах стоять единцы, так как это "путь" до самих себя у вершин.
//Матрица смежности для неориентированного графа
int[][] matrix = new int[][]
{
    //          A  B  C  D  E  F  G
    new int[] { 1, 1, 1, 1, 1, 0, 0 },
    new int[] { 1, 1, 0, 1, 1, 0, 0 },
    new int[] { 1, 0, 1, 0, 0, 1, 1 },
    new int[] { 1, 1, 0, 1, 1, 0, 0 },
    new int[] { 1, 1, 0, 1, 1, 0, 0 },
    new int[] { 0, 0, 1, 0, 0, 1, 1 },
    new int[] { 0, 0, 1, 0, 0, 1, 1 }
};
//Матрица смежности для неориентированного графа
int[][] matrix2 =
{
    //          A  B  C  D  E  F  G
    new int[] { 1, 2, 5, 0, 0, 0, 0 },
    new int[] { 2, 1, 0, 0, 0, 1, 0 },
    new int[] { 5, 0, 1, 1, 1, 0, 0 },
    new int[] { 0, 0, 1, 1, 1, 0, 1 },
    new int[] { 0, 0, 1, 1, 1, 0, 1 },
    new int[] { 0, 1, 0, 0, 0, 1, 1 },
    new int[] { 0, 0, 0, 1, 1, 1, 1 }
};
//Матрица смежности для ориентированного графа
int[][] matrix3 =
{
    //          A  B  C  D  E  F 
    new int[] { 1, 7, 4, 0, 0, 0 },
    new int[] { 0, 1, 4, 0, 2, 0 },
    new int[] { 0, 0, 1, 4, 8, 0 },
    new int[] { 0, 0, 0, 1, 0, 12 },
    new int[] { 0, 0, 0, 4, 1, 5 },
    new int[] { 0, 0, 0, 0, 0, 1 },
};

//Пример создания ориентированного графа. ВСЕГДА НУЖНО УКАЗЫВАТЬ ОРИЕНТИРОВАННОСТЬ. Иначе алгоритмы будут некорректные.
Graph graph = new Graph("Oriented", 6, new []{"A", "B", "C", "D", "E", "F"},matrix3, true);
//Пример создания неориентированного графа.
Graph unorientedGraph = new Graph("Unoriented", 7, new []{"A", "B", "C", "D", "E", "F", "G"},matrix, false);

//подсчет всех граней в графе. Нужен в основном для алгоритма дейкстры.

Console.WriteLine($"Oriented graph edges count: {graph.Edges.Length}");
foreach (var i in graph.Edges)
    Console.WriteLine($"Edge : {i.StartVertex} - {i.EndVertex} - {i.Weight} ");


//Пример применения алгоритма Краскала для СВЯЗНОГО НЕОРИЕНТИРОВАННОГО графа (только для них предназначен).
KruskalAlgorithm kruskalAlgorithm = new KruskalAlgorithm();
var spanningTreeEdges = kruskalAlgorithm.Kruskal(unorientedGraph);

Console.WriteLine($"Spanning Tree Edges count in {unorientedGraph.Title}: {spanningTreeEdges.Count}");
foreach (var i in spanningTreeEdges)
    Console.WriteLine($"Spanning Tree Edge : {i.StartVertex} - {i.EndVertex} - {i.Weight} ");
Console.Write("\n");


//Работает только для графов с неотрицательными ребрами, для ориентированных и неориентированных графов.
var dijkstra = new DijkstraAlgorithm();
var shortestPath = dijkstra.GetShortestPath(graph, 0, 5);

Console.WriteLine($"The shortes path between 0 and 4 in {graph.Title} is : {shortestPath}");

var shortestPathUnoriented = dijkstra.GetShortestPath(unorientedGraph, 0, 5);

Console.WriteLine($"The shortes path between 0 and 5 in {graph.Title} is : {shortestPathUnoriented}");


//Алгоритм Форда-Фалкерсона для максимального потока используется только в ориентированных неотрицательных связных графах.
Console.WriteLine($"Max flow between 0 and 5 in {graph.Title}: ");
var maxFlow = new FordFalkerson();
Console.WriteLine(maxFlow.FordFulkerson(graph, graph.Vertices, 0, 5));
  


Console.WriteLine($"Depth First Search in {unorientedGraph.Title} graph:");
DfsAlgorithm dfs = new DfsAlgorithm();
var stepsDfs = dfs.Dfs(unorientedGraph, 0);
foreach (var i in stepsDfs)
    Console.WriteLine($"Current Vertex: {i}");
    
Console.WriteLine($"Breadth First Search in {graph.Title} graph:");
BfsAlgorithm bfs = new BfsAlgorithm();
var stepsBfs = bfs.Bfs(graph, 0);
foreach (var i in stepsBfs)
    Console.WriteLine($"Current Vertex: {i}");