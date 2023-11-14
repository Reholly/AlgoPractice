namespace AlgoPractise.AlgorithmsForNikolaev.Models;

public struct Edge
{
    public Edge(int startVertex, int endVertex, int weight)
    {
        EndVertex = endVertex;
        StartVertex = startVertex;
        Weight = weight;
    }

    public int StartVertex { get; set; }
    public int EndVertex { get; set; }
    public int Weight { get; set; }
}