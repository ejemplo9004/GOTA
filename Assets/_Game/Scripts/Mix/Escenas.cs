using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour
{
	private void Awake()
	{
		int i = PlayerPrefs.GetInt("idioma", -1);
		if (i == -1)
		{
			PlayerPrefs.SetInt("idioma", 1);
		}
	}
	public void CambiarEscena(string escena)
	{
		SceneManager.LoadScene(escena);
	}
}
