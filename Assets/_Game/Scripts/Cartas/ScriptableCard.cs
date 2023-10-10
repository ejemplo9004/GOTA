using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObject/Card")]
public class ScriptableCard : ScriptableObject
{
    public string cardName;
    public string Description;
    public int cost;
    public Sprite cardSprite;
    public GameObject prefab;
}
