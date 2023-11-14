using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class DijkstraAlgorithm
{
    public int GetShortestPath(Graph graph, int startVertex, int endVertex)
    {
        int[] distances = new int[graph.Vertices];
        bool[] usedVertices = new bool[graph.Vertices];

        //В алгоритме Дейкстры изначально все расстояния равны бесконечности.
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = int.MaxValue;
        }
        //Расстояние от началльной вершины до нее самой равно нулю
        distances[startVertex] = 0;
        //Ищем все кратчайшие пути до вершин.
        for (int i = 0; i < graph.Vertices - 1; i++)
        {
            //Находим вершину с минимальным расстоянием до нее.
            int minDistanceVertex = MinDistanceVertex(distances, usedVertices);
            //Помечаем вершину как пройденную 
            usedVertices[minDistanceVertex] = true;

            //Обновляем массив длин на этом шаге.
            //Проверяем есть ли вообще дорога в узел, не прошли ли мы уже его, не равен ли он "бесконечности" (int.MaxValue)
            //И условие новой стоимости перемещения к ребру: новое расстояние до ребра равно минимуму из 
            //текущей дистанции до j-ой вершины  до ребра и
            // (расстояние до minDistanceVertex  + расстояние до j-ой вершины от minDistanceVertex)
            
            for (int j = 0; j < graph.Vertices; j++)
            {
                if (!usedVertices[j] &&
                    graph.AdjacencyMatrix[minDistanceVertex][j] >= 1 &&
                    distances[minDistanceVertex] != int.MaxValue &&
                    distances[minDistanceVertex] + graph.AdjacencyMatrix[minDistanceVertex][j] < distances[j])
                {
                    distances[j] = distances[minDistanceVertex] + graph.AdjacencyMatrix[minDistanceVertex][j];
                }
            }
        }

        return distances[endVertex];
    }
    
    
    private int MinDistanceVertex(int[] distances, bool[] usedVertices)
    {
        int min = int.MaxValue;
        int vertices = distances.Length;
        int minIndex = -1;
        

        for (int i = 0; i < vertices; i++)
        {
            if (!usedVertices[i] && distances[i] <= min)
            {
                min = distances[i];
                minIndex = i;
            }
        }

        return minIndex;
    }
}