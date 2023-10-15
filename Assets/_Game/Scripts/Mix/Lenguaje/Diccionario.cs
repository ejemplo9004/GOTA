using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Morion/Diccionario")]
public class Diccionario : ScriptableObject
{
	public Palabra[] palabras;
	public MenuIdioma idiomaSeleccionado;
	public string GetPalabra(string llave)
	{
		for (int i = 0; i < palabras.Length; i++)
		{
			if (palabras[i].llave.Equals(llave))
			{
				return palabras[i].GetPalabra(PlayerPrefs.GetInt("idioma"));
			}
		}
		return llave;
	}

	public void GuardarIdioma()
	{
		PlayerPrefs.SetInt("idioma", (int)idiomaSeleccionado);
	}
}

[System.Serializable]
public class Palabra
{
    public string llave;

    public string[] idiomas;

    public string GetPalabra(int idioma)
	{
		if (idioma < 0 || idioma >= idiomas.Length)
		{
			return llave;
		}

		return idiomas[idioma].Replace("/n","\n");
	}
}


public enum MenuIdioma
{
	español = 0,
	ingles = 1
}