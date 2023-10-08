using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    private List<(int, int)> pathPlayerToEnemy1, pathEnemyToPlayer1;
    private int[,] _map;
    private float _verticalProb;

    public List<(int, int)> GetPaths(Vector2 origin1, Vector2 origin2)
    {
        var connected = false;
        var path = new List<(int, int)>();
        pathPlayerToEnemy1.Add(((int)origin1.x, (int)origin1.y));
        pathEnemyToPlayer1.Add(((int)origin2.x, (int)origin2.y));
        var counter = 0;

        while (!connected && counter < 1000)
        {
            var horizontalNeighbors = GetHorizontalNeighborsPosition(pathEnemyToPlayer1[pathEnemyToPlayer1.Count - 1].Item1,
                pathEnemyToPlayer1[pathEnemyToPlayer1.Count - 1].Item2);
            var verticalNeighbors = GetVerticalNeighborsPosition(pathEnemyToPlayer1[pathEnemyToPlayer1.Count - 1].Item1,
                pathEnemyToPlayer1[pathEnemyToPlayer1.Count - 1].Item2);

            var rowOrCol = UnityEngine.Random.Range(0, 1f);

            if (rowOrCol < _verticalProb)
            {
                var nextPath = UnityEngine.Random.Range(0, verticalNeighbors.Count);
                
                if (pathPlayerToEnemy1.Contains(verticalNeighbors[nextPath]))
                {
                    path = pathPlayerToEnemy1.Concat(pathEnemyToPlayer1).ToList();
                    return ReducedPath(path);
                }
                else
                {
                    pathEnemyToPlayer1.Add(verticalNeighbors[nextPath]);
                }
            }
            
            else
            {
                var nextPath = UnityEngine.Random.Range(0, horizontalNeighbors.Count);
                if (pathPlayerToEnemy1.Contains(horizontalNeighbors[nextPath]))
                {
                    path = pathPlayerToEnemy1.Concat(pathEnemyToPlayer1).ToList();
                    return ReducedPath(path);
                }
                else
                {
                    pathEnemyToPlayer1.Add(horizontalNeighbors[nextPath]);
                }
            }

            var horizontalNeighbors2 = GetHorizontalNeighborsPosition(pathPlayerToEnemy1[pathPlayerToEnemy1.Count - 1].Item1, 
                pathPlayerToEnemy1[pathPlayerToEnemy1.Count - 1].Item2);
            var verticalNeighbors2 = GetVerticalNeighborsPosition(pathPlayerToEnemy1[pathPlayerToEnemy1.Count - 1].Item1,
                pathPlayerToEnemy1[pathPlayerToEnemy1.Count - 1].Item2);

            rowOrCol = UnityEngine.Random.Range(0, 1f);

            if (rowOrCol < _verticalProb)
            {
                var nextPath = UnityEngine.Random.Range(0, verticalNeighbors2.Count);
                if (pathEnemyToPlayer1.Contains(verticalNeighbors2[nextPath]))
                {
                    path = pathEnemyToPlayer1.Concat(pathPlayerToEnemy1).ToList();
                    return ReducedPath(path);
                }
                else
                {
                    pathPlayerToEnemy1.Add(verticalNeighbors2[nextPath]);
                }
            }
            else
            {
                var nextPath = UnityEngine.Random.Range(0, horizontalNeighbors2.Count);
                if (pathEnemyToPlayer1.Contains(horizontalNeighbors2[nextPath]))
                {
                    path = pathEnemyToPlayer1.Concat(pathPlayerToEnemy1).ToList();
                    return ReducedPath(path);
                }
                else
                {
                    pathPlayerToEnemy1.Add(horizontalNeighbors2[nextPath]);
                }
            }
            
            counter++;
        }
        Debug.Log(counter);
        pathEnemyToPlayer1.Clear();
        pathPlayerToEnemy1.Clear();
        return path;
    }

    private List<(int, int)> GetHorizontalNeighborsPosition(int row, int col)
    {
        var neighbors = new List<(int, int)>();
        for (int i = col - 1; i < col + 2; i++)
        {
            if (i < 0 || i >= _map.GetLength(1) || i == col) continue;
            neighbors.Add((row, i));
        }
        return neighbors;
    }

    private List<(int, int)> GetVerticalNeighborsPosition(int row, int col)
    {
        var neighbors = new List<(int, int)>();
        for (int i = row - 1; i < row + 2; i++)
        {
            if (i < 0 || i >= _map.GetLength(0) || i == row) continue;
            neighbors.Add((i, col));
        }
        return neighbors;
    }

    private List<(int, int)> ReducedPath(List<(int, int)> path)
    {
        var reducedPath = new List<(int, int)>();
        foreach (var item in path)
        {
            if (!reducedPath.Contains(item))
            {
                reducedPath.Add(item);
            }
        }
        return reducedPath;
    }

    public PathFinder(int[,] map, float verticalProb)
    {
        pathEnemyToPlayer1 = new List<(int, int)> ();
        pathPlayerToEnemy1 = new List<(int, int)> ();   
        _map = map;
        _verticalProb = verticalProb;
    }
}
