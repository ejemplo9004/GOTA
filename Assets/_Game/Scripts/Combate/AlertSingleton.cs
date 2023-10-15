using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSingleton : MonoBehaviour
{

    #region Singleton
    private static AlertSingleton instance;

    public static AlertSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AlertSingleton>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonExample");
                    instance = singletonObject.AddComponent<AlertSingleton>();
                }
            }
            return instance;
        }
    }
    #endregion

    public delegate void OnAlertTriggered(AlertEmition alert);
    public event OnAlertTriggered triggerAlert;

    public float[] LastTimer;
    public AlertEmition[] LastEmition;

    private void OnEnable()
    {
        triggerAlert += HandleAlert;

        int alerts = Enum.GetNames(typeof(AlertType)).Length;
        LastTimer = new float[alerts];
        StartCoroutine(TimerClearance());
    }

    public void HandleAlert(AlertEmition alert)
    {
        LastTimer[(int) alert.alertType] = 10f;
        LastEmition[(int) alert.alertType] = alert;
    }

    IEnumerator TimerClearance()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            for (int i = 0; i < LastTimer.Length; i++)
            {
                if (LastTimer[i] > 0)
                {
                    LastTimer[i] += -1;
                }else if (LastTimer[i] > -1)
                {
                    LastEmition[i] = null;
                }
            }
        }
    }

    public AlertEmition GetHighestPriorityAlert()
    {
        for (int i = 0;i < LastTimer.Length; i++)
        {
            if (LastTimer[i] > 0)
            {
                return LastEmition[i];
            }
        }
        return null;
    }
}

public class AlertEmition
{
    public AlertType alertType;
    public Vector3 position;
}

public enum AlertType
{
    EnemyTowerAttacked,
    PlayerTowerAttacked,
    EnemyUnitAttacked,
    PlayerUnitAttacked,
    UnitInEnemyTerritory,
    UnitInPlayerTerritory
}
