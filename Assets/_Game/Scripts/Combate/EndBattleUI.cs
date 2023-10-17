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
    public Image imAliados;
    public Image imEnemigos;

	private IEnumerator Start()
	{
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        imAliados.sprite = GetImagen(PlayerPrefs.GetString("mazo"));
        imEnemigos.sprite = GetImagen(IAController.Instance.baraja.nombre);
    }

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
        GestionCombate.singleton.GameOver();
        ShowTowerImage(true);
        yield return new WaitForSeconds(timeToShowPanel);
        cardCanvas.SetActive(false);
        winBattlePanel.SetActive(true);
        yield return new WaitForSeconds(timeToEndBattle);
        SceneManager.LoadScene(0);

    }

    private IEnumerator ShowLosePanel()
    {
        GestionCombate.singleton.GameOver();
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
                    winImage.sprite = towerSprites[3];
                }
                else
                {
                    loseImage.sprite = towerSprites[3];
                }
                break;
        }
    }

    private Sprite GetImagen(string team)
    {
        switch (team)
        {
            case "MUISCAS":
                return towerSprites[0];
            case "PANCHES":
                return towerSprites[1];
            case "GUANES":
                return towerSprites[2];
            case "DEMONIOS":
                return towerSprites[3];
        }
        return towerSprites[0];
    }
}
