using System.Text;
using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class KruskalAlgorithm
{
    private List<string> _steps = new List<string>();
    public (List<Edge>,List<string>) GetSteps(Graph graph)
    {
        _steps.Clear();

        var edges = Kruskal(graph);

        return (edges, _steps);
    }
    public List<Edge> Kruskal(Graph graph)
    {
        var edges = graph.Edges;
        var result = new List<Edge>();
        
        _steps.Add("Сортируем в порядке возрастания грани графа.");
        edges = edges.OrderBy(x => x.Weight).ToArray();
        StringBuilder builder = new StringBuilder();
        foreach (var edge in edges)
            builder.Append($"{edge.StartVertex} -> {edge.EndVertex} : Вес - {edge.Weight} ");

        builder.Append("\n");
        _steps.Add(builder.ToString());
        
        int[] parentsSets = new int[graph.Vertices];
        
        for (int i = 0; i < graph.Vertices; i++)
        {
            parentsSets[i] = i;
        }

        int edgeCount = 0;
        int edgeIndex = 0;

        while (edgeCount < graph.Vertices - 1)
        {
            Edge nextEdge = edges[edgeIndex++];
            //Ищем множества, к которым принадлежат вершины начальная и конечная.
            _steps.Add("Проверяем к каким множества относятся начальная и конечная точка ребра: ");
            int setX = Find(parentsSets, nextEdge.StartVertex);
            
            int setY = Find(parentsSets, nextEdge.EndVertex);
            _steps.Add($"Начальная вершина, множество: {setX}. Конечная вершина, множество {setY}. ");
            //если вершины не из одного множества, то добавляем их в МОД и объединяем их множества.
            if (setX != setY)
            {
                _steps.Add($"Вершины не из одного множества, объединяем множества {setX} и {setY}. ");
                result.Add(nextEdge);
                Union(parentsSets, setX, setY);
                edgeCount++;
              
            }
        }
        
        return result;
    }
    //К какому множеству относится вершина i. (вплоть до vertices - 1 множеств)
    private int Find(int[] parentsSets, int i)
    {
        _steps.Add($"Ищем к какому множетсву относится {i} вершина. ");
        if (parentsSets[i] != i)
        {
            _steps.Add($"В массиве родителей по индексу самой {i} вершины не находится равное {i} значение. Рекурсивно погружаемся" +
                       $" в этот поиск снова, толкьо вместо {i} передаем элемент из массива родителей по {i} индексу ");
            parentsSets[i] = Find(parentsSets, parentsSets[i]);
        }
        
        return parentsSets[i];
    }
    
    //Объединение множеств. просто обычный поиск множеств и присваивание первому номер последнего.
    private void Union(int[] parentsSets, int x, int y)
    {
        _steps.Add($"Объединение: Находим множества X и Y. После присваиваем множеству X тот же номер, что и у Y. ");
        int setX = Find(parentsSets, x);
        int setY = Find(parentsSets, y);
        parentsSets[setX] = setY;
    }
}