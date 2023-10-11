using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController
{
    public CardList hand;
    public CardList drawDeck;
    public CardList discardDeck;
    CardListDebugger[] debuggers;

    public DeckController(ScriptableDeck deck)
    {
        drawDeck = new CardList(deck.cards);
        hand = new CardList();
        discardDeck = new CardList();
        debuggers = Object.FindObjectsOfType<CardListDebugger>();
        debuggers[0].list = hand;
        debuggers[1].list = drawDeck;
        debuggers[2].list = discardDeck;        
        debuggers[0].name = "hand";
        debuggers[1].name = "drawDeck";
        debuggers[2].name = "discardDeck";
    }

    public ScriptableCard DrawToHand()
    {
        ScriptableCard card = null;
        card = drawDeck.DrawCard(true);
        //Debug.Log($"Hand {hand.CardCount}\nDeck {drawDeck.CardCount}\nDiscard {discardDeck.CardCount}");
        if (card == null)
        {
            //Debug.Log($"BEFORE: DrawDeck {drawDeck.CardCount} DiscardDeck {discardDeck.CardCount}");
            drawDeck = discardDeck;
            discardDeck = new CardList();
            debuggers[2].list = discardDeck;
            debuggers[1].list = drawDeck;
            card = drawDeck.DrawCard(true);
            //.Log($"AFTER: DrawDeck {drawDeck.CardCount} DiscardDeck {discardDeck.CardCount}");
        }
        hand.AddCard(card);
        return card;
    }

    public void DiscardCard(ScriptableCard card)
    {
        if(hand.RemoveCard(card))
        {
            discardDeck.AddCard(card);
        }
    }

}
