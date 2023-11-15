using System.Text;
using AlgoPractise.AlgorithmsForNikolaev.Models;

namespace AlgoPractise.AlgorithmsForNikolaev.Algorithms;

public class DijkstraAlgorithm
{
    public List<(string[], string)> _steps = new List<(string[], string)>();

    //Первое путь, второе список из текущего пути и описания шага
    public (int, List<(string[], string)>) GetSteps(Graph graph, int startVertex, int endVertex)
    {
        _steps.Clear();

        int path = GetShortestPath(graph, startVertex, endVertex);
        var lasVertices = _steps[_steps.Count - 1];
        var newList = new List<string>();

        foreach (var i in lasVertices.Item1)
        {
            newList.Add(i);
        }
        newList.Add(graph.VerticesNames[graph.Vertices - 1]);
        _steps.Add((newList.ToArray(), $"Финальный шаг: нашли нужную вершину {graph.VerticesNames[endVertex]}" +
                                       $" и кратчайший путь равен: {path}"));
        return (path, _steps);
    }
    
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
            List<string> vertices = new List<string>();
            for (int flag = 0; flag < usedVertices.Length; flag++)
            {
                if (usedVertices[flag])
                {
                    vertices.Add(graph.VerticesNames[flag]);
                }
            }

            if (usedVertices[endVertex])
            {
                return distances[endVertex];
            }
            
            //Обновляем массив длин на этом шаге.
            //Проверяем есть ли вообще дорога в узел, не прошли ли мы уже его, не равен ли он "бесконечности" (int.MaxValue)
            //И условие новой стоимости перемещения к ребру: новое расстояние до ребра равно минимуму из 
            //текущей дистанции до j-ой вершины  до ребра и
            // (расстояние до minDistanceVertex  + расстояние до j-ой вершины от minDistanceVertex)
            
            _steps.Add((vertices.ToArray(),"Обновляем дистанции по условию  (условие описано выше)."));
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
            
            StringBuilder currentDistances = new StringBuilder();
            for (var x =0; x < distances.Length; x++)
            {
                currentDistances.Append($"Кр. путь до {graph.VerticesNames[x]} : {distances[x]}, ");
            }
            _steps.Add((vertices.ToArray(), $"Текущие дистанции {currentDistances.ToString()}. Проходим дальше по вершинам и выбираем следующую вершину по следующим условиям:" +
                                            $"1) мы НЕ прошли эту вершин. 2) расстояние не равно бесконечности. 3) есть ли дорога к узлу." +
                                            $"4) новое расстояние до ребра равно минимуму из текущей дистанции до j-ой вершины  до ребра и " +
                                            $"(расстояние до minDistanceVertex  + расстояние до j-ой вершины от minDistanceVertex)" +
                                            $"  Получили, что по условиям идем в вершину: {graph.VerticesNames[i]} = {distances[i]} и получаем путь: "));

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