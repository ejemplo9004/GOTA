using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList
{
    public List<ScriptableCard> cards;
    public int CardCount
    {
        get { return cards.Count; }
    }

    public ScriptableCard DrawCard(bool random = false)
    {
        if (cards == null) { cards = new List<ScriptableCard>(); }

        ScriptableCard card = null;

        if (CardCount > 0)
        {
            int r = (random) ? Random.Range(0, CardCount) : 0;
            card = cards[r];
            cards.RemoveAt(0);
        }

        return card;
    }

    public bool AddCard(ScriptableCard card)
    {
        if (cards == null)
        {
            cards.Add(new ScriptableCard());
        }

        cards.Add(card);
        return true;
    }
}
