using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattleUI : MonoBehaviour
{
    [SerializeField] private GameObject winBattlePanel;
    [SerializeField] private GameObject loseBattlePanel;
    [SerializeField] private Image winImage, loseImage;
    [SerializeField] private List<Sprite> towerSprites;
    [SerializeField] private float timeToShowPanel;
    [SerializeField] private float timeToEndBattle;
    [SerializeField] private GameObject cardCanvas;

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
        ShowTowerImage(true);
        yield return new WaitForSeconds(timeToShowPanel);
        cardCanvas.SetActive(false);
        winBattlePanel.SetActive(true);
        yield return new WaitForSeconds(timeToEndBattle);
        SceneManager.LoadScene(0);

    }

    private IEnumerator ShowLosePanel()
    {
        ShowTowerImage(false);
        yield return new WaitForSeconds(timeToShowPanel);
        cardCanvas.SetActive(false);
        loseBattlePanel.SetActive(true);
        yield return new WaitForSeconds(timeToEndBattle);
        SceneManager.LoadScene(0);
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

    private void ShowTowerImage(bool win)
    {
        var team = PlayerPrefs.GetString("mazo");
        if (team == null) team = "GUANES";
        switch (team)
        {
            case "MUISCAS":
                if(win)
                {
                    winImage.sprite = towerSprites[0];
                }
                else
                {
                    loseImage.sprite = towerSprites[0];
                }
                break;
            case "PANCHES":
                if (win)
                {
                    winImage.sprite = towerSprites[1];
                }
                else
                {
                    loseImage.sprite = towerSprites[1];
                }
                break;
            case "GUANES":
                if (win)
                {
                    winImage.sprite = towerSprites[2];
                }
                else
                {
                    loseImage.sprite = towerSprites[2];
                }
                break;
            case "DEMONIOS":
                if (win)
                {
                    winImage.sprite = towerSprites[2];
                }
                else
                {
                    loseImage.sprite = towerSprites[2];
                }
                break;
        }
    }
}
