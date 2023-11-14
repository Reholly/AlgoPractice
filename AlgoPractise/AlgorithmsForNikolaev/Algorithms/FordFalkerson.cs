using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class FordFalkerson
{
    public int FordFulkerson(Graph graph, int vertices, int source, int sink)
    {
        //Копируем исходный граф во временную матрицу смежности. Ее мы и будет изменять
        int[][] tempGraph = new int[vertices][];
        for (int i = 0; i < vertices; i++)
        {
            tempGraph[i] = new int[vertices];
            for (int v = 0; v < vertices; v++)
            {
                tempGraph[i][v] = graph.AdjacencyMatrix[i][v];
            }
        }
        //Создаем массив "родителей" вершин.
        int[] parents = new int[vertices]; //в парентс лежат последние вершины-родители при поиске в глубину, который нашли.
        int maxFlow = 0;

        //Пока путь ЕСТЬ, мы выполняем алгоритм. (путь ищется BFS-ом)
        while (Bfs(tempGraph,  vertices, source, sink, parents))
        {
            int pathFlow = int.MaxValue;
            //Ищем минимальный поток в пути образовавшемся. Путь мы достаем из parents,
            //который изменился после проходки BFS.
            for (int i = sink; i != source; i = parents[i])
            {
                int iVertexParent = parents[i];
                pathFlow = Math.Min(pathFlow, tempGraph[iVertexParent][i]);
            }
            //Меняем исходный граф. Мы делаем что-то вроде ребра "обратного потока" (на хабре есть статья).
            //
            for (int i = sink; i != source; i = parents[i])
            {
                int currentVertexParent = parents[i];
                tempGraph[currentVertexParent][i] -= pathFlow;
                tempGraph[i][currentVertexParent] += pathFlow;
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