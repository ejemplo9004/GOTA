using System;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    public static Action onWinBattle;
    public static Action onLoseBattle;

    private void OnEnable()
    {
        ListasObjetivos.onTowerDestroy += CheckBattleState;
    }

    private void OnDisable()
    {
        ListasObjetivos.onTowerDestroy -= CheckBattleState;
    }
    public void CheckBattleState()
    {
        if(GestionCombate.singleton.listaUnidades.torresPrincipalesAliadas.Count == 0)
        {
            onLoseBattle.Invoke();
        }
        else if(GestionCombate.singleton.listaUnidades.torresPrincipalesEnemigas.Count == 0)
        {
            onWinBattle.Invoke();
        }
    }
}
