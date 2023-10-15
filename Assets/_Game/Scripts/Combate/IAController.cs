using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float energyRegen;
    public EnemyDeckController enemyDeckController;

    public ListasObjetivos listas;
    private float energy;



    public void Start()
    {
        listas = GestionCombate.singleton.listaUnidades;

    }

    

    private enum EstadoIA
    {
        Atacando,
        Defendiendo
    }
}
