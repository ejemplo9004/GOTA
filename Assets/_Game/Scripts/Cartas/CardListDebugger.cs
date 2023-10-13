using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardListDebugger : MonoBehaviour
{
    public CardList list;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if(list != null)
        {
            string s = name;
            foreach(ScriptableCard card in list.cards)
            {
                s += $"\n{card.prefab.name}";
            }
            text.text = s;
        }
    }
}
