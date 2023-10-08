using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hex List", menuName = "Latinidad/Hex List")]
public class HexList : ScriptableObject
{
    [SerializeField] private HexType hexType;
    [SerializeField] private bool hasWalkableHex;
    [SerializeField] private List<GameObject> hexList;
    [SerializeField] private List <GameObject> walkableHexList;
    [SerializeField] private List<float> probs;
    [SerializeField] private List<float> walkProbs;

    public GameObject GetHex()
    {
        var index = Random.Range(0f, 1f);
        for (int i = 0; i < hexList.Count; i++)
        {
            if(index < probs[i])
            {
                return hexList[i];
            }
        }
        return hexList[Random.Range(0, hexList.Count)];
    }

    public GameObject GetWalkableHex()
    {
        var index = Random.Range(0f, 1f);
        for (int i = 0; i < walkableHexList.Count; i++)
        {
            if (index < walkProbs[i])
            {
                return walkableHexList[i];
            }
        }
        return walkableHexList[Random.Range(0, walkableHexList.Count)];
    }

    public bool HasWalkableHex { get => hasWalkableHex; }
}
