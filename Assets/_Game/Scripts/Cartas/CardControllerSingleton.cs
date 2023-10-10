using UnityEngine;

public class CardControllerSingleton : MonoBehaviour
{
    #region Singleton
    private static CardControllerSingleton instance;

    public static CardControllerSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardControllerSingleton>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonExample");
                    instance = singletonObject.AddComponent<CardControllerSingleton>();
                }
            }
            return instance;
        }
    }
    #endregion

    [Header("UI Card Settings")]
    public float screenPorcentajeLimitToPlayCard;

    [Header("Line Settings")]
    public LineController lineController;
    public LayerMask worldMask;
    [HideInInspector] public GameObject arrowInstance;
    public GameObject arrowPrefab;

}