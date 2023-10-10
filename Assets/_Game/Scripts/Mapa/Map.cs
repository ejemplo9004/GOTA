using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private NavMeshBaker navMeshBaker;
    [SerializeField] private List<HexList> hexLists;
    [SerializeField] private GameObject walkableParent, notWalkableParent;
    [SerializeField] private GameObject playerTower, enemyTower;
    [SerializeField] private GameObject seaHex;

    [Header("Map shape")]
    public int height;
    public int width;

    [Header("Expand Map")]
    public int extendedHeight;
    public int extendedWidth;

    [Header("Offsets")]
    public float xOffset;
    public float zOffset;
    public float oddOffset;

    [Header("Initializers")]
    [Min(1)]
    public int upperCornerInitializer;
    public Vector2 playerTowerPos1, playerTowerPos2, enemyTowerPos1, enemyTowerPos2;

    [Header("Config")]
    public float newLandProb;
    public bool fillRandomly;
    public int repeats;
    public float verticalProb;

    private Hexagon[,] _hexagons;
    private int[,] _logicalHexagons;
    private int[,] _expandedMap;
    private PathFinder _pathFinder;

    private void Start()
    {
        _hexagons = new Hexagon[height, width];
        _logicalHexagons = new int[height, width];
        _expandedMap = new int[extendedHeight, extendedWidth];
        _pathFinder = new PathFinder(_logicalHexagons, verticalProb);
        if (fillRandomly) FillRandom();
        else
        {
            _logicalHexagons[0, 0] = upperCornerInitializer;
            for (int i = 0; i < repeats; i++)
            {
                FillLogicalMap();
            }
            FillMap();
        }
    }

    private void FillRandom()
    {
        for (int i = 0; i < _hexagons.GetLength(0); i++)
        {
            for (int j = 0; j < _hexagons.GetLength(1); j++)
            {
                _logicalHexagons[i, j] = Random.Range(0, hexLists.Count) + 1;
            }
        }
        for (int i = 0; i < repeats; i++)
        {
            FillLogicalMap();
        }
        FillMap();
    }

    private void FillExtendedMap()
    {
        for (var i = 0; i < _expandedMap.GetLength(0); i++)
        {
            for (var j = 0; j < _expandedMap.GetLength(1); j++)
            {
                _expandedMap[i, j] = -1;
            }
        }

        int xInit = (extendedHeight - height) / 2;
        int yInit = (extendedWidth - width) / 2;

        Debug.Log(xInit + ", " +  yInit);
        for(var i = xInit; i < xInit + height; i++)
        {
            for(var j = yInit; j < yInit + width; j++)
            {
                _expandedMap[i, j] = _logicalHexagons[i - xInit, j - yInit];
            }
        }
    }

    private void FillLogicalMap()
    {
        for (int i = 0; i < _hexagons.GetLength(0); i++)
        {
            for (int j = 0; j < _hexagons.GetLength(1); j++)
            {
                if (i == 0 && j == 0) continue;
                _logicalHexagons[i, j] = GetNextHex(i, j);
            }
        }
    }

    private void FillMap()
    {
        var path1 = _pathFinder.GetPaths(enemyTowerPos1, playerTowerPos1);
        var path2 = _pathFinder.GetPaths(enemyTowerPos2, playerTowerPos2);
        RemoveTowerPoints(path1);
        RemoveTowerPoints(path2);

        //Debug.Log(string.Join(", ", path1));
        //Debug.Log(string.Join(", ", path2));
        for (int i = 0; i < _hexagons.GetLength(0); i++)
        {
            for (int j = 0; j < _hexagons.GetLength(1); j++)
            {
                GameObject hexObj = null;
                if (path1.Contains((i, j)) || path2.Contains((i, j)))
                {
                    hexObj = hexLists[_logicalHexagons[i, j] - 1].HasWalkableHex ? Instantiate(hexLists[_logicalHexagons[i, j] - 1].GetWalkableHex(), gameObject.transform) :
                       Instantiate(hexLists[0].GetWalkableHex(), gameObject.transform);
                }
                else
                {
                    if(new Vector2(i, j).Equals(playerTowerPos1) || new Vector2(i, j).Equals(playerTowerPos2))
                    {
                        hexObj = Instantiate(playerTower);
                    }
                    else if(new Vector2(i, j).Equals(enemyTowerPos1) || new Vector2(i, j).Equals(enemyTowerPos2))
                    {
                        hexObj = Instantiate(enemyTower);
                    }
                    else
                    {
                        hexObj = Instantiate(hexLists[_logicalHexagons[i, j] - 1].GetHex(), gameObject.transform);
                    }                  
                }
                var posX = i % 2 == 0 ? j * xOffset : j * xOffset + oddOffset; 
                var posZ = i * zOffset;
                var pos = new Vector3(posX, 0, posZ);
                _hexagons[i, j] = hexObj.GetComponent<Hexagon>();
                hexObj.transform.position = pos;
                if (hexObj.GetComponent<Hexagon>().Bakeable) hexObj.transform.parent = walkableParent.transform;
                //else if (hexObj.GetComponent<Hexagon>().Jumpable) hexObj.transform.parent = walkableParent.transform;
                else hexObj.transform.parent = notWalkableParent.transform;
            }
        }

        navMeshBaker.BakeMap(walkableParent);
        FillExtendedMap();
        InstantiateExpandedMap();
    }

    private void InstantiateExpandedMap()
    {
        int xCorner = (extendedHeight - height) / 2;
        int yCorner = (extendedWidth - width) / 2;

        for (var i = 0; i < _expandedMap.GetLength(0); i++)
        {
            for (var j = 0; j < _expandedMap.GetLength(1); j++)
            {
                if (_expandedMap[i, j] == -1)
                {
                    var seaObj = Instantiate(seaHex, gameObject.transform);
                    var posX = (xCorner - i) % 2 == 0 ? -(yCorner - j) * xOffset : -(yCorner - j) * xOffset + oddOffset;
                    var posZ = -(xCorner - i) * zOffset;
                    var pos = new Vector3(posX, 0, posZ);
                    seaObj.transform.position = pos;
                }
            }
        }
    }

    private int GetNextHex(int row, int col)
    {
        var neighbors = GetNeighbors(row, col);
        var neighborsDict = GetNeighborDictionary(neighbors);
        var value = Random.Range(0f, 1f + newLandProb);
        foreach (var item in neighborsDict)
        {
            if(value < item.Value)
            {
                return item.Key;
            }
        }
        return Random.Range(1, hexLists.Count + 1);
    }

    private List<int> GetNeighbors(int row, int col)
    {
        var neighbors = new List<int>();
        for (int i = col - 1; i < col + 2; i++)
        {
            if (i < 0 || i >= width || i == col || _logicalHexagons[row, i] == 0) continue;
            neighbors.Add(_logicalHexagons[row, i]);
        }
        for (int i = row - 1; i < row + 2; i++)
        {
            if (i < 0 || i >= height || i == row || _logicalHexagons[i, col] == 0) continue;
            neighbors.Add(_logicalHexagons[i, col]);
        }
        return neighbors;
    }

    private Dictionary<int, float> GetNeighborDictionary(List<int> neighbors)
    {
        var dict = new Dictionary<int, float>();
        var probDict = new Dictionary<int, float>();
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (dict.ContainsKey(neighbors[i])) dict[neighbors[i]]++;
            else dict.Add(neighbors[i], 1);
        }

        for(int i = 0; i < dict.Keys.Count; i++)
        {
            probDict.Add(dict.Keys.ElementAt(i), dict[dict.Keys.ElementAt(i)] / neighbors.Count);
        }
        return probDict;
    }

    private void RemoveTowerPoints(List<(int, int)> path)
    {
        path.RemoveAll(item => item.Item1 == playerTowerPos1.x && item.Item2 == playerTowerPos1.y);
        path.RemoveAll(item => item.Item1 == playerTowerPos2.x && item.Item2 == playerTowerPos2.y);
        path.RemoveAll(item => item.Item1 == enemyTowerPos1.x && item.Item2 == enemyTowerPos1.y);
        path.RemoveAll(item => item.Item1 == enemyTowerPos2.x && item.Item2 == enemyTowerPos2.y);
    }
}
