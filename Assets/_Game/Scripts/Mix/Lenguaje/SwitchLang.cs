using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchLang : MonoBehaviour
{
    public Text txtIdioma;
    // Start is called before the first frame update
    void Start()
    {
        txtIdioma.text = (PlayerPrefs.GetInt("idioma", 0) == 0) ? "ENGLISH" : "ESPAÑOL";
    }

    public void CambiarIdioma()
	{
		if (PlayerPrefs.GetInt("idioma", 0) == 0)
        {
            PlayerPrefs.SetInt("idioma", 1);
		}
		else
		{
            PlayerPrefs.SetInt("idioma", 0);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
