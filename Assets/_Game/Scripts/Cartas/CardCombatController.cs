using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCombatController : MonoBehaviour
{
    public Barajas[] barajas;

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
    [Header("Deck")]
    public ScriptableDeck deck;
    public DeckController deckController;
    public UICardsController cardsController;
    public UINextCardController nextCardController;

    [Header("Cost")]
    public float energyPerSecond = 1;
    public float energy = 5;
    public Material disabledMaterial21;
    public Material disabledMaterial22;

    private ScriptableCard nextCard;

    public Map map;

    private void Start()
    {
        string aliada = PlayerPrefs.GetString("mazo", "MUISCAS");
        for (int i = 0; i < barajas.Length; i++)
        {
            if (barajas[i].nombre == aliada)
            {
                deck = barajas[i].deck;
            }
        }
        cardsController = new UICardsController();
        deckController = new DeckController(deck);
        int spaces = cardsController.EmptyCards();

        for (int i = 0; i < spaces; i++)
        {
            cardsController.AddCard(deckController.DrawToHand());
            deckController.PrepareNextCard();
            nextCardController.SetNextCard(deckController.NextCard);
        }
        StartCoroutine(RegenEnergyCoroutine());
    }

    public void OnCardPlayed(ScriptableCard card)
    {
        deckController.DiscardCard(card);
        cardsController.AddCard(deckController.DrawToHand());
        nextCardController.SetNextCard(deckController.NextCard);
    }

    IEnumerator RegenEnergyCoroutine()
    {
        while (true)
        {
            energy = Mathf.Clamp( energy + Time.deltaTime * energyPerSecond, 0, 10);
            yield return null;
        }
    }
}
