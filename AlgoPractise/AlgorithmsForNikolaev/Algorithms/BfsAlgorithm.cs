using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class BfsAlgorithm
{
    private List<string> _steps = new List<string>();
    
    public List<string> Bfs(Graph graph, int startVertex)
    {
        _steps.Clear();
        
        bool[] visitedVertices = new bool[graph.Vertices];

        Queue<int> queue = new Queue<int>();

        queue.Enqueue(startVertex);

        while (queue.Count != 0)
        {
            int currentVertex = queue.Dequeue();
            for (int i = 0; i < graph.AdjacencyMatrix[currentVertex].Length; i++)
            {
                if (!visitedVertices[i] && graph.AdjacencyMatrix[currentVertex][i] >= 1)
                {
                    visitedVertices[i] = true;
                    _steps.Add(graph.VerticesNames[i]);
                    queue.Enqueue(i);
                }
            }
        }

        return _steps;
    }
}