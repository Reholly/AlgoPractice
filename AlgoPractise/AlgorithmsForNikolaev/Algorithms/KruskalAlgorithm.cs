using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class KruskalAlgorithm
{
    public List<Edge> Kruskal(Graph graph)
    {
        var edges = graph.Edges;
        var result = new List<Edge>();

        edges = edges.OrderBy(x => x.Weight).ToArray();

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
            int setX = Find(parentsSets, nextEdge.StartVertex);
            int setY = Find(parentsSets, nextEdge.EndVertex);
           
            //если вершины не из одного множества, то добавляем их в МОД и объединяем их множества.
            if (setX != setY)
            {
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
        if (parentsSets[i] != i)
        {
            parentsSets[i] = Find(parentsSets, parentsSets[i]);
        }
        
        return parentsSets[i];
    }
    
    //Объединение множеств. просто обычный поиск множеств и присваивание первому номер последнего.
    private void Union(int[] parentsSets, int x, int y)
    {
        int setX = Find(parentsSets, x);
        int setY = Find(parentsSets, y);
        parentsSets[setX] = setY;
    }
}