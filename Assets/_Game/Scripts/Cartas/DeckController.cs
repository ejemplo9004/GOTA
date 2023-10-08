using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController
{
    public CardList hand;
    public CardList drawDeck;
    public CardList discardDeck;

    public ScriptableCard DrawToHand()
    {
        if (hand == null) hand = new();
        if (drawDeck == null) drawDeck = new();
        if (discardDeck == null) discardDeck = new();

        ScriptableCard card = null;
        card = drawDeck.DrawCard();
        if (card == null)
        {
            drawDeck = discardDeck;
            discardDeck = null;
            card = drawDeck.DrawCard(true);
        }
        hand.AddCard(card);
        return card;
    }

}
