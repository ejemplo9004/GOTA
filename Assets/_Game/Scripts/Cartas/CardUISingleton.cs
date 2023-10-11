using UnityEngine;

public class CardUISingleton : MonoBehaviour
{
    #region Singleton
    private static CardUISingleton instance;

    public static CardUISingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardUISingleton>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonExample");
                    instance = singletonObject.AddComponent<CardUISingleton>();
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