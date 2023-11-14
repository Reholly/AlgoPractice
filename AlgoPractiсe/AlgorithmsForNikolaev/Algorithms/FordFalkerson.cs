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
            /* Это своеобразные ребра, по которым можно вернуть жидкость обратно из одной точки в другую.
             * Этто нужно для оптимального решения, так как иногда путь, который мы нашли, может быть не оптимальным и
             * просто перекроет другие пути. Для этого нужны обратные ребра, по которым можно "вернуться".
             *
             * Пример: граф в форме ромба с одной диагональю, А-исток, Д-сток, ВС-ребро в середине, АВ=7, ВС=4, СД=5,
             * АС=3, ВД=4. Предположим, первым найден путь АВСД, вычитаем максимум (4),
             * остаточная сеть остается АВ=3, ВС=0, СД=1, СВ=4 (обратное ребро). Дальше находим пути АВД и АСД,
             * остается сеть АВ=0, ВД=1, АС=2, СД=0, и при отсутствии обратного ребра СВ решение неоптимально из-за
             * перегрузки пути АВСД. Но оно есть и мы находим ещё один путь АСВД на 1 и получаем ответ 4+3+1+1=9.
             */
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