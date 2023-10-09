using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCombatController : MonoBehaviour
{
    #region Singleton
    private static CardCombatController instance;

    public static CardCombatController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardCombatController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonExample");
                    instance = singletonObject.AddComponent<CardCombatController>();
                }
            }
            return instance;
        }
    }
    #endregion
    public ScriptableDeck deck;
    public DeckController deckController;
    public UICardsController cardsController;

    private void Start()
    {
        cardsController = new UICardsController();
        deckController = new DeckController(deck);
        int spaces = cardsController.EmptyCards();

        for (int i = 0; i < spaces; i++)
        {
            cardsController.AddCard(deckController.DrawToHand());
        }
    }

    public void OnCardPlayed(ScriptableCard card)
    {
        deckController.DiscardCard(card);
        cardsController.AddCard(deckController.DrawToHand());
    }
}
