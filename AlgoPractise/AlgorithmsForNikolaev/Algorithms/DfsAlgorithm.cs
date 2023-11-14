using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class DfsAlgorithm
{
    private List<string> _steps = new List<string>();
    
    public List<string> Dfs(Graph graph, int startVertex)
    {
        _steps.Clear();
        
        bool[] visitedVertices = new bool[graph.Vertices];
        DfsUtil(startVertex, graph, visitedVertices);

        return _steps;
    }

    private void DfsUtil(int currentVertex, Graph graph, bool[] visitedVertices)
    {
        visitedVertices[currentVertex] = true;
        _steps.Add(graph.VerticesNames[currentVertex]);
        for (int i = 0; i < graph.AdjacencyMatrix[currentVertex].Length; i++)
        {
            if (!visitedVertices[i] && graph.AdjacencyMatrix[currentVertex][i] >= 1)
            {
                DfsUtil(i, graph, visitedVertices);
            }
        }
    }
}