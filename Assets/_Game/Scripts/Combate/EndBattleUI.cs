using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class EndBattleUI : MonoBehaviour
{
    [SerializeField] private GameObject winBattlePanel;
    [SerializeField] private GameObject loseBattlePanel;
    [SerializeField] private float timeToShowPanel;

    private void OnEnable()
    {
        EndBattle.onWinBattle += EnableWinPanel;
        EndBattle.onLoseBattle += EnableLosePanel;
    }

    private void OnDisable()
    {
        EndBattle.onWinBattle -= EnableWinPanel;
        EndBattle.onLoseBattle -= EnableLosePanel;
    }

    private IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(timeToShowPanel);
        winBattlePanel.SetActive(true);
    }

    private IEnumerator ShowLosePanel()
    {
        yield return new WaitForSeconds(timeToShowPanel);
        loseBattlePanel.SetActive(true);
    }

    private void EnableWinPanel()
    {
        StartCoroutine(ShowWinPanel());
    }

    public void DisableWinPanel()
    {
        winBattlePanel.SetActive(false);
    }

    private void EnableLosePanel()
    {
        StartCoroutine(ShowLosePanel());
    }

    public void DisableLosePanel()
    {
        loseBattlePanel.SetActive(false);
    }
}
