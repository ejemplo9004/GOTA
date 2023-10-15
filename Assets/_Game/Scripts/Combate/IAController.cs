using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class IAController : MonoBehaviour
{
    #region Singleton
    private static IAController instance;

    public static IAController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<IAController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonExample");
                    instance = singletonObject.AddComponent<IAController>();
                }
            }
            return instance;
        }
    }
    #endregion

    public float energyMultiplier = 1;
    public ScriptableDeck deck;
    public int handSize;

    public ListasObjetivos listas;
    private EnemyDeckController enemyDeckController;
    public float energy;
    public float[] probabilities;
    public int[] order;
    public Hexagon position;
    public Map map;



    public void Start()
    {
        listas = GestionCombate.singleton.listaUnidades;
        enemyDeckController = new EnemyDeckController(deck);
        probabilities = new float[handSize];
        for (int i = 0; i < handSize; i++)
        {
            enemyDeckController.DrawToHand();
        }
        StartCoroutine(RegenEnergyCoroutine());
        StartCoroutine(PlayGame());
    }

    IEnumerator RegenEnergyCoroutine()
    {
        while (true)
        {
            energy = Mathf.Clamp(
                energy + Time.deltaTime * CardCombatController.Instance.energyPerSecond * energyMultiplier
                , 0, 10);
            yield return null;
        }
    }

    IEnumerator PlayGame()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            CheckProbabilities();
            PlayAccordingProbable();
        }
    }

    private void PlayAccordingProbable()
    {
        int[] order = FindOrder();
        int cardToPlay = -1;


        for (int i = 0; i < order.Length; i++)
        {
            bool toPlay = Random.Range(0, 1f) < probabilities[order[i]];
            if (toPlay)
            {
                if (enemyDeckController.hand.cards[i].cost < energy)
                {
                    cardToPlay = i;
                    break;
                }
            }
        }
        if (cardToPlay >= 0)
        {
            ScriptableCard card = enemyDeckController.hand.cards[cardToPlay];
            //Debug.Log($"About to play {card}");

            GameObject unit = Instantiate(card.prefab,
                ChoosePosition(),
                Quaternion.identity);

            unit.GetComponent<Equipador>().Inicializar(Equipo.enemigo);
            energy -= card.cost;
            enemyDeckController.DiscardCard(card);
            enemyDeckController.DrawToHand();
        }
        else
        {
            //Debug.Log("WAiting");
        }
    }

    private void CheckProbabilities()
    {
        for (int i = 0; i < handSize; i++)
        {
            probabilities[i] = CalculateProbabilityOnCard(i);
        }
    }

    private float CalculateProbabilityOnCard(int i)
    {
        return Random.Range(0, 1f);
    }

    private int[] FindOrder()
    {
        int n = probabilities.Length;
        int[] orderOfPositions = new int[n];
        Array.Copy(Enumerable.Range(0, n).ToArray(), orderOfPositions, n);

        Array.Sort(orderOfPositions, (a, b) => probabilities[b].CompareTo(probabilities[a]));
        order = orderOfPositions;
        return orderOfPositions;
    }

    private Vector3 ChoosePosition()
    {
        int favoriteRow = 13;
        int favoriteColumn = 5;
        int row = favoriteRow;
        int column = favoriteColumn;

        

        row += Random.Range(-2, 2);
        column += Random.Range(-2, 2);

        if(row > map.height || row < 0)
        {
            row = favoriteRow;
        }

        if (column > map.width || column < 0)
        {
            column = favoriteColumn;
        }

        Vector3 hex = map.Hexagons[row, column].transform.position;
        Vector3 pos = new Vector3(hex.x, 0.4166315f, hex.z);
        return pos;
    }

    private enum EstadoIA
    {
        Atacando,
        Defendiendo
    }
}
