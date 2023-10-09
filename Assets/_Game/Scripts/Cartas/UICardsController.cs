using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICardsController
{
    public List<UICard> cardContainers;

    public UICardsController()
    {
        cardContainers = Object
            .FindObjectsByType<UICard>(FindObjectsSortMode.None)
            .ToList();
    }

    public int EmptyCards()
    {
        return cardContainers.Count(card => !card.hasCard);
    }

    public bool AddCard(ScriptableCard card)
    {
        for (int i = 0; i < cardContainers.Count; i++)
        {
            if (!cardContainers[i].hasCard)
            {
                cardContainers[i].FillUICard(card);
                return true;
            }
        }
        return false;
    }
}