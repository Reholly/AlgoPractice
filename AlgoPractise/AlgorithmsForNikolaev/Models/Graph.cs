namespace AlgoPractise.AlgorithmsForNikolaev.Models;

public class Graph
{
    public string Title => _title;
    public string[] VerticesNames => _verticesNames;
    public int Vertices => _vertices;
    public int[][] AdjacencyMatrix => _adjacencyMatrix;
    public Edge[] Edges 
    {   
        get =>  _edges;
        set => _edges = value;
    }
    public bool IsOriented => _isOriented;

    private readonly string _title = "Default";
    private readonly string[] _verticesNames;
    private readonly int[][] _adjacencyMatrix;
    private readonly int _vertices;
    private Edge[] _edges;
    private readonly bool _isOriented;

    //если ориентированный, то там тупо не будет значений где не надо, а если взвешенный
    // то будут просто значения >=1

    public Graph(string title, int vertices, string[] verticesNames, int[][] adjacencyMatrix, bool orientedGraph)
    {
        _title = title;
        _vertices = vertices;
        _verticesNames = verticesNames;
        _adjacencyMatrix = adjacencyMatrix;
        _isOriented = orientedGraph;
        _edges = GetEdges(adjacencyMatrix, vertices, orientedGraph).ToArray();
    }

    private List<Edge> GetEdges(int[][] adjacencyMatrix, int vertices, bool orientedGraph)
    {
        //Метод для получения граней.
        List<Edge> edges = new List<Edge>();
        bool[] visitedVertices = new bool[vertices];
        
        for (int y = 0; y < adjacencyMatrix.Length; y++)
        {
            for (int x = 0; x < adjacencyMatrix[y].Length; x++)
            {
                if (x == y || (!orientedGraph && visitedVertices[x])) continue;
                if (adjacencyMatrix[y][x] <= 0) continue;
                edges.Add(new Edge(y, x, adjacencyMatrix[y][x]));
            }

            visitedVertices[y] = true;
        }

        return edges;
    }
}
