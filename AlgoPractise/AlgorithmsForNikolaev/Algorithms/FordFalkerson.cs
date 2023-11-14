using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class FordFalkerson
{
    public int FordFulkerson(Graph graph, int vertices, int source, int sink)
    {
        //Копируем исходный граф во временную матрицу смежности. Ее мы и будет изменять
        int[][] tempGraph = new int[vertices][];
        for (int u = 0; u < vertices; u++)
        {
            tempGraph[u] = new int[vertices];
            for (int v = 0; v < vertices; v++)
            {
                tempGraph[u][v] = graph.AdjacencyMatrix[u][v];
            }
        }
        //Создаем массив "родителей" вершин.
        int[] parent = new int[vertices];
        int maxFlow = 0;

        while (Bfs(tempGraph,  vertices, source, sink, parent))
        {
            int pathFlow = int.MaxValue;
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, tempGraph[u][v]);
            }

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                tempGraph[u][v] -= pathFlow;
                tempGraph[v][u] += pathFlow;
            }

            maxFlow += pathFlow;
        }

        return maxFlow;
    }
    
    private bool Bfs(int[][] residualGraph, int vertices, int source, int sink, int[] parents)
    {
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(source);
        visited[source] = true;
        parents[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < vertices; v++)
            {
                if (!visited[v] && residualGraph[u][v] > 0)
                {
                    queue.Enqueue(v);
                    parents[v] = u;
                    visited[v] = true;
                }
            }
        }

        return visited[sink];
    }

}