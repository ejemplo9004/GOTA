using UnityEngine;

public class EnemyDeckController
{
    public CardList hand;
    public CardList drawDeck;
    public CardList discardDeck;

    public EnemyDeckController(ScriptableDeck deck)
    {
        drawDeck = new CardList(deck.cards);
        hand = new CardList();
        discardDeck = new CardList();
    }

    public ScriptableCard DrawToHand()
    {
        ScriptableCard card = null;


        card = drawDeck.DrawCard(true);
        
        {
            drawDeck = discardDeck;
            discardDeck = new CardList();
            card = drawDeck.DrawCard(true);
        }

        hand.AddCard(card);

        Debug.Log(hand);
        Debug.Log(drawDeck);
        Debug.Log(discardDeck);
        return card;
    }


    public void DiscardCard(ScriptableCard card)
    {
        if (hand.RemoveCard(card))
        {
            discardDeck.AddCard(card);
        }
    }
}