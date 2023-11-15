using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class FordFalkerson
{
    private List<string> _steps = new List<string>();
    
    //первое лист всех путей вершин, второе лист всех шагов

    public (List<List<string>>, List<string>) GetSteps(Graph graph, int vertices, int source, int sink)
    {
        _steps.Clear();
        var result = FordFulkerson(graph, vertices, source, sink);
        _steps.Add($"Максимальный поток найден: {result.Item2}. ");
        return (result.Item1, _steps);
    }
    public (List<List<string>>,int) FordFulkerson(Graph graph, int vertices, int source, int sink)
    {
        //Копируем исходный граф во временную матрицу смежности. Ее мы и будет изменять
        List<List<string>> allPaths = new List<List<string>>();
        
        int[][] tempGraph = new int[vertices][];
        for (int i = 0; i < vertices; i++)
        {
            tempGraph[i] = new int[vertices];
            for (int v = 0; v < vertices; v++)
            {
                tempGraph[i][v] = graph.AdjacencyMatrix[i][v];
            }
        }
        _steps.Add($"Копируем исходный граф во временную матрицу смежности. Она будет служить сетью для изменений. ");
        //Создаем массив "родителей" вершин.
        int[] parents = new int[vertices]; //в парентс лежат последние вершины-родители при поиске в глубину, который нашли.
        int maxFlow = 0;

        //Пока путь ЕСТЬ, мы выполняем алгоритм. (путь ищется BFS-ом)
        while (Bfs(tempGraph,  vertices, source, sink, parents))
        {
            _steps.Add($"Путь был найден, продолжаем алгоритм. ");
            int pathFlow = int.MaxValue;
            //Ищем минимальный поток в пути образовавшемся. Путь мы достаем из parents,
            //который изменился после проходки BFS.
            List<string> currentPath = new List<string>();
            for (int i = sink; i != source; i = parents[i])
            {
                int iVertexParent = parents[i];
                currentPath.Add(graph.VerticesNames[iVertexParent]);
                pathFlow = Math.Min(pathFlow, tempGraph[iVertexParent][i]);
            }
            allPaths.Add(currentPath);
            _steps.Add($"Минимальный поток в найденном пути: {pathFlow}");
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
                _steps.Add($"Устанавливаем следующее: во временном графе по индексу {currentVertexParent},{i} к этому элементу и " +
                           $" {tempGraph[currentVertexParent][i]} - {pathFlow}. Уменьшаем на мин.поток текущий поток по пути " +
                           $"найденному. ");
                tempGraph[currentVertexParent][i] -= pathFlow;
                _steps.Add($"Устанавливаем следующее: во временном графе по индексу {i},{currentVertexParent} к этому элементу и " +
                           $" {tempGraph[i][currentVertexParent]} + {pathFlow}. Увеличиваем на мин.поток текущий поток по пути " +
                           $"найденному. ");
                tempGraph[i][currentVertexParent] += pathFlow;
            }
            _steps.Add($"Прибавляем к счетчику максимального потока наш минимальный поток: {maxFlow} += {pathFlow}");
            maxFlow += pathFlow;
        }

        return (allPaths, maxFlow);
    }
    
    private bool Bfs(int[][] residualGraph, int vertices, int source, int sink, int[] parents)
    {
        _steps.Add($"Начинаем обход графа в ширину, чтобы найти путь от истока до стока. ");
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();
        _steps.Add($"Добавили в очередь вершину по номеру {source}, пометили ее как пройденную. ");
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
                    _steps.Add($"Добавили в очередь вершину по номеру {v}, пометили ее как пройденную. ");
                    queue.Enqueue(v);
                    parents[v] = u;
                    visited[v] = true;
                }
            }
        }

        return visited[sink];
    }

}